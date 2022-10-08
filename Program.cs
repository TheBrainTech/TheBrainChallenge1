// See https://aka.ms/new-console-template for more information

using TheBrainChallenge1;

Console.WriteLine("TheBrain Challenge #1");

while(true) {
	Console.WriteLine("======================");
	Console.WriteLine("Enter a size (or zero to exit):");

	string sizeString = Console.ReadLine();
	int.TryParse(sizeString, out int size);

	if(size <= 0) {
		break;
	}

	Console.WriteLine("Fill darkness (1 to 10):");
	string fillString = Console.ReadLine();
	int.TryParse(fillString, out int fill);

	Console.WriteLine("Edge darkness (1 to 10):");
	string edgeString = Console.ReadLine();
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
}
