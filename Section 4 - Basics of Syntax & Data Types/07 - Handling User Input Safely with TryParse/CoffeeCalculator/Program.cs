int numberOfCoffees = 0;
double pricePerCoffee = 2.5;
const double salesTax = 0.07;

Console.WriteLine("Welcome to our Coffee Calculator!");

Console.Write("How many cups of coffee would you like? ");
string userInput = Console.ReadLine();

bool isValidNumber = int.TryParse(userInput, out numberOfCoffees);

if (!isValidNumber)
{
    Console.WriteLine("Oops! That doesn't look like a valid number. We'll set your order to 1 coffee by default.");
    numberOfCoffees = 1;
}

Console.Write("Would you like sugar in your coffee? (y/n): ");
char sugarResponse = char.Parse(Console.ReadLine());

bool wantsSugar;

if (sugarResponse == 'y')
{
    wantsSugar = true;
}
else
{
    wantsSugar = false;
}

Console.WriteLine($"Sugar added: {wantsSugar}");

if (wantsSugar)
{
    Console.WriteLine("Adding sugar to your coffee.");
}
else
{
    Console.WriteLine("No sugar added to your coffee.");
}

double totalPrice = numberOfCoffees * pricePerCoffee;
double totalWithTax = totalPrice + (totalPrice * salesTax);

Console.WriteLine($"Your total for {numberOfCoffees} cups of coffee is ${totalPrice}.");
Console.WriteLine($"Including tax, your total comes to ${totalWithTax}.");