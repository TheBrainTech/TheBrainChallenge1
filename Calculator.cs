using SkiaSharp;
using System.ComponentModel;
using System.Text;
using static TheBrainChallenge1.Calculator;

namespace TheBrainChallenge1;

public class Calculator {
	
	public double YSize { get; set; } = 2.0;

	private const double EdgeWidth = 1;

	private const string grayscaleChars = " .:-=+*#%@";
	private const string binaryBlocks = " █";
	private const string grayscaleBlocks = " ░▒▓█";

	private SKBitmap bitmap;
	
	public Calculator(string path) {
		SKImage image;
		using(FileStream fs = new FileStream(path, FileMode.Open)) {
			bitmap = SKBitmap.Decode(fs);
		}
	}

	public string Square(int size, bool useBlocks) {
		string grays = useBlocks ? grayscaleBlocks : grayscaleChars;

		StringBuilder line = new();
		for(int n = 0; n < size; n++) {
			line.Append(grays[grays.Length - 1]);
		}

		StringBuilder output = new();
		for(double n = 0; n < size; n += YSize) {
			output.Append(line + "\n");
		}

		return output.ToString();
	}

	public enum OutputType {
		Characters,
		Grayscale,
		BlackAndWhite,
	}
	
	public string Circle(int size, int fillDarkness, int edgeDarkness, OutputType outputType) {

		double cx = (size) / 2.0;
		double cy = (size) / 2.0;

		string grays;
		switch(outputType) {
			case OutputType.Grayscale:
				grays = grayscaleBlocks;
				break;
			case OutputType.BlackAndWhite:
				grays = binaryBlocks;
				break;
			default:
				grays = grayscaleChars;
				break;

		}

		cx++;
		cy += 2;

		double rad = size / 2.0;

		StringBuilder output = new();
		for(double y = 0; y < size + 4; y += YSize) {
			for(double x = 0; x < size + 2; x++) {

				double dist = CalcDist(x, y, cx, cy);
				double brightness = 0;
				if(dist <= rad + 1) {

					double distFromEdge = Math.Abs(dist - rad);
					double edgeAmount = Math.Max(EdgeWidth - distFromEdge, 0);

					double fillAmount = Math.Min(rad + 1 - dist, 1);
					double fill = fillAmount * fillDarkness / 10.0;
					double edge = edgeAmount * edgeDarkness / 10.0;

					brightness = fill + edge;
				}

				int index = (int)Math.Min((brightness) * grays.Length, grays.Length - 1);
				output.Append(grays[index]);
			}
			output.Append("\n");
		}
		return output.ToString();
	}

	public string Picture(int size, OutputType outputType) {

		string grays;
		switch(outputType) {
			case OutputType.Grayscale:
				grays = grayscaleBlocks;
				break;
			case OutputType.BlackAndWhite:
				grays = binaryBlocks;
				break;
			default:
				grays = grayscaleChars;
				break;

		}

		SKSizeI skSize = new SKSizeI(size, size);

		using(SKBitmap back = bitmap.Resize(skSize, SKFilterQuality.High)) {

			double cx = (size) / 2.0;
			double cy = (size) / 2.0;

			double maxBrightness = 0.1;
			double totalBrightness = 0;
			for(double y = 0; y < size; y += YSize) {
				for(double x = 0; x < size; x++) {

					SKColor color = back.GetPixel((int)x, (int)y);
					double brightness = (color.Red / 255.0) * 0.2126 + (color.Green / 255.0) * 0.7152 + (color.Blue / 255.0) * 0.0722;
					maxBrightness = Math.Max(brightness, maxBrightness);
					totalBrightness += brightness;
				}
			}
			double averageBrightness = totalBrightness / (size * size);
			//maxBrightness = Math.Min(averageBrightness * 2, maxBrightness);

			StringBuilder output = new();
			for(double y = 0; y < size; y += YSize) {
				for(double x = 0; x < size; x++) {

					SKColor color = back.GetPixel((int)x, (int)y);
					double brightness = (color.Red / 255.0) * 0.2126 + (color.Green / 255.0) * 0.7152 + (color.Blue / 255.0) * 0.0722;
					brightness *= 1 / maxBrightness;

					int index = (int)Math.Min((brightness) * grays.Length, grays.Length - 1);
					output.Append(grays[index]);
				}
				output.Append("\n");
			}
			return output.ToString();
		}
	}



	public string PictureWithDiffusion(int size, OutputType outputType) {

		string grays;
		switch(outputType) {
			case OutputType.Grayscale:
				grays = grayscaleBlocks;
				break;
			case OutputType.BlackAndWhite:
				grays = binaryBlocks;
				break;
			default:
				grays = grayscaleChars;
				break;

		}

		SKSizeI skSize = new SKSizeI(size, size);

		using(SKBitmap back = bitmap.Resize(skSize, SKFilterQuality.High)) {

			double cx = (size) / 2.0;
			double cy = (size) / 2.0;

			double maxBrightness = 0.1;
			double totalBrightness = 0;
			for(double y = 0; y < size; y += YSize) {
				for(double x = 0; x < size; x++) {

					SKColor color = back.GetPixel((int)x, (int)y);
					double brightness = (color.Red / 255.0) * 0.2126 + (color.Green / 255.0) * 0.7152 + (color.Blue / 255.0) * 0.0722;
					maxBrightness = Math.Max(brightness, maxBrightness);
					totalBrightness += brightness;
				}
			}

			StringBuilder output = new();

			// conversion to a value will be inaccurate since we only have a limited set of greys. use Floyd/Steinberg diffusion to improve result
			// we need a buffer that will include the current line and the next line;
			double[] curLineError = new double[size];
			double[] nextLineError = new double[size];
			for(double y = 0; y < size; y += YSize) {
				for(int x = 0; x < size; x++) {

					SKColor color = back.GetPixel(x, (int)y);
					double brightness = (color.Red / 255.0) * 0.2126 + (color.Green / 255.0) * 0.7152 + (color.Blue / 255.0) * 0.0722;
					brightness *= 1 / maxBrightness;

					brightness += curLineError[x];

					int index = (int)Math.Min((brightness) * grays.Length, grays.Length - 1);
					index = Math.Max(index, 0);
					output.Append(grays[index]);

					// how different is the output from what was desired?
					double error = ((double)index / (grays.Length - 1)) - brightness;
					// distribute this over the nearby pixels as per Floyd/Steinberg
					double errorUnit = -error / 16;
					if(x < size - 1) {
						curLineError[x + 1] += errorUnit * 7.0;
						nextLineError[x + 1] += errorUnit * 1.0;
					}
					if(x > 0) {
						nextLineError[x - 1] += errorUnit * 3.0;
					}
					nextLineError[x + 0] += errorUnit * 5.0;
				}
				output.Append("\n");
				for(int x = 0; x < size; x++) {
					curLineError[x] = nextLineError[x];
					nextLineError[x] = 0;
				}
			}
			return output.ToString();
		}
	}

	private double CalcDist(double x1, double y1, double x2, double y2) {
		double dx = x1 - x2;
		double dy = y1 - y2;
		return Math.Sqrt(dx * dx + dy * dy);
	}
}
