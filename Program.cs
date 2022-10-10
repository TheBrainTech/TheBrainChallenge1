// See https://aka.ms/new-console-template for more information

using TheBrainChallenge1;

Console.WriteLine("TheBrain Challenge #1");

string defFill = "10";
string defEdge = "0";
string defSize = "100";
string defVert = "2.0";
string defOut = "C";

while(true) {
	Console.WriteLine("======================");
	
	Console.WriteLine($"(C)hararters, (B)locks or e(X)it? - default is {defOut}");
	string outString = Console.ReadLine().ToUpper();
	if(outString == "X") {
		break;
	}
	if(outString == "C" || outString == "B") {
		defOut = outString;
	}
	bool useBlocks = defOut == "B";
	
	Console.WriteLine($"Enter a size (enter for {defSize}):");
	string sizeString = Console.ReadLine();
	if(sizeString == "") {
		sizeString = defSize;
	}
	int.TryParse(sizeString, out int size);

	Console.WriteLine($"Vertical multiplier (enter for {defVert}):");
	string vertString = Console.ReadLine();
	if(vertString == "") {
		vertString = defVert;
	}
	double.TryParse(vertString, out double vert);

	Console.WriteLine($"Fill darkness (1 to 10 or enter for {defFill}):");
	string fillString = Console.ReadLine();
	if(fillString == "") {
		fillString = defFill;
	}
	int.TryParse(fillString, out int fill);

	Console.WriteLine($"Edge darkness (1 to 10 or enter for {defEdge}):");
	string edgeString = Console.ReadLine();
	if(edgeString == "") {
		edgeString = defEdge;
	}
	int.TryParse(edgeString, out int edge);

	Calculator calc = new Calculator("../../../Background.png");
	calc.YSize = vert;

	//Console.WriteLine("======================");
	//Console.WriteLine("SQUARE:");
	//Console.WriteLine("");
	//string square = calc.Square(size, useBlocks);
	//Console.Write(square);
	//Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("CIRCLE:");
	Console.WriteLine("");
	string circle = calc.Circle(size, fill, edge, useBlocks);
	Console.Write(circle);
	Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("PICTURE:");
	Console.WriteLine("");
	string picture = calc.Picture(size, useBlocks);
	Console.Write(picture);
	Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("\n\n");

	defFill = fillString;
	defEdge = edgeString;
	defSize = sizeString;
}
