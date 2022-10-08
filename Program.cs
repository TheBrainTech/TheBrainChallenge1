// See https://aka.ms/new-console-template for more information

using TheBrainChallenge1;

Console.WriteLine("TheBrain Challenge #1");

string defFill = "10";
string defEdge = "0";
string defSize = "50";
string defOut = "C";

while(true) {
	Console.WriteLine("======================");
	
	Console.WriteLine($"Chars or blocks (C or B) - default is {defOut}");
	string outString = Console.ReadLine().ToUpper();
	if(outString == "C" || outString == "B") {
		defOut = outString;
	}
	bool useBlocks = defOut == "B";
	
	Console.WriteLine($"Enter a size (or X to exit or enter for {defSize}):");
	string sizeString = Console.ReadLine();
	if(sizeString == "") {
		sizeString = defSize;
	}

	if(!int.TryParse(sizeString, out int size)) {
		break;
	}
	
	if(size <= 0) {
		break;
	}

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

	Calculator calc = new Calculator();

	Console.WriteLine("======================");
	Console.WriteLine("SQUARE:");
	Console.WriteLine("");
	string square = calc.Square(size, useBlocks);
	Console.Write(square);
	Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("CIRCLE:");
	Console.WriteLine("");
	string circle = calc.Circle(size, fill, edge, useBlocks);
	Console.Write(circle);
	Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("\n\n");

	defFill = fillString;
	defEdge = edgeString;
	defSize = sizeString;
}
