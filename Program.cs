// See https://aka.ms/new-console-template for more information

using TheBrainChallenge1;

Console.WriteLine("TheBrain Challenge #1");

string defFill = "10";
string defEdge = "0";
string defSize = "50";

while(true) {
	Console.WriteLine("======================");
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
	string square = calc.Square(size);
	Console.Write(square);
	Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("CIRCLE:");
	Console.WriteLine("");
	string circle = calc.Circle(size, fill, edge);
	Console.Write(circle);
	Console.WriteLine("");
	Console.WriteLine("======================");
	Console.WriteLine("\n\n");

	defFill = fillString;
	defEdge = edgeString;
	defSize = sizeString;
}
