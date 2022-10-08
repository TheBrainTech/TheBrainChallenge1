using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace TheBrainChallenge1;

public class Calculator {
	
	private const double YSize = 2.65;

	private const double EdgeWidth = 1;

	private const string grayscaleChars = " .:-=+*#%@";
	private const string grayscaleBlocks = " ░▒▓█";
	
	public Calculator() {
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
		for(double y = 0; y < size + 5; y += YSize) {
			for(double x = 0; x < size + 2; x++) {
				double dist = CalcDist(x, y, cx, cy);
				if(dist <= rad + 1) {

					double distFromEdge = Math.Abs(dist - rad);
					double edgeAmount = Math.Max(EdgeWidth - distFromEdge, 0);

					double fillAmount = Math.Min(rad + 1 - dist, 1);
					double fill = fillAmount * fillDarkness / 10.0;
					double edge = edgeAmount * edgeDarkness / 10.0;
					
					int index = (int)Math.Min((fill + edge) * grayscaleBlocks.Length, grayscaleBlocks.Length - 1);
					output.Append(grays[index]);
				} else {
					output.Append(" ");
				}
			}
			output.Append("\n");
		}
		return output.ToString();
	}

	private double CalcDist(double x1, double y1, double x2, double y2) {
		double dx = x1 - x2;
		double dy = y1 - y2;
		return Math.Sqrt(dx * dx + dy * dy);
	}
}
