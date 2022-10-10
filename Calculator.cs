using SkiaSharp;
using System.ComponentModel;
using System.Text;

namespace TheBrainChallenge1;

public class Calculator {
	
	public double YSize { get; set; } = 2.0;

	private const double EdgeWidth = 1;

	private const string grayscaleChars = " .:-=+*#%@";
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
	
	public string Circle(int size, int fillDarkness, int edgeDarkness, bool useBlocks) {

		double cx = (size) / 2.0;
		double cy = (size) / 2.0;

		string grays = useBlocks ? grayscaleBlocks : grayscaleChars;

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

	public string Picture(int size, bool useBlocks) {
		SKSizeI skSize = new SKSizeI(size, size);

		using(SKBitmap back = bitmap.Resize(skSize, SKFilterQuality.High)) {

			double cx = (size) / 2.0;
			double cy = (size) / 2.0;

			string grays = useBlocks ? grayscaleBlocks : grayscaleChars;

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

	private double CalcDist(double x1, double y1, double x2, double y2) {
		double dx = x1 - x2;
		double dy = y1 - y2;
		return Math.Sqrt(dx * dx + dy * dy);
	}
}
