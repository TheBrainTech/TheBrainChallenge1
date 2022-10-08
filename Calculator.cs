using Microsoft.VisualBasic.FileIO;
using System.Text;

namespace TheBrainChallenge1;

public class Calculator {
	
	private const double YSize = 2.65;

	private const double EdgeWidth = 2;
	
//	private const double FillColor = 0.2;
//	private const double EdgeColor = 1;
	
	private const double FillColor = 1;
	private const double EdgeColor = 0;
	

	private const string grayscale = " .:-=+*#%@";
	
	public Calculator() {
	}

	public string Square(int size) {
		StringBuilder line = new();
		for(int n = 0; n < size; n++) {
			line.Append('*');
		}

		StringBuilder output = new();
		for(double n = 0; n < size; n += YSize) {
			output.Append(line + "\n");
		}

		return output.ToString();
	}
	
	public string Circle(int size) {

		double cx = (size) / 2.0;
		double cy = (size) / 2.0;

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
					double fill = fillAmount * FillColor;
					double edge = edgeAmount * EdgeColor;
					
					int index = (int)Math.Min((fill + edge) * 10, 9);
					output.Append(grayscale[index]);
				} else {
					output.Append(" ");
				}
			}
			output.Append("\n");
		}
		return output.ToString();
	}

	private double CalcDist(double x, double y, double cx, double cy) {
		double dx = x - cx;
		double dy = y - cy;
		//dx *= AspectAdjust;
		return Math.Sqrt(dx * dx + dy * dy);
	}
}
