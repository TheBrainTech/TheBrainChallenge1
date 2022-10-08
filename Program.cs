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

	Calculator calc = new Calculator();

	Console.WriteLine("======================");
	Console.WriteLine("SQUARE:");
	string square = calc.Square(size);
	Console.Write(square);
	Console.WriteLine("======================");
	Console.WriteLine("CIRCLE:");
	string circle = calc.Circle(size);
	Console.Write(circle);
	Console.WriteLine("======================");
	Console.WriteLine("\n\n");
}
