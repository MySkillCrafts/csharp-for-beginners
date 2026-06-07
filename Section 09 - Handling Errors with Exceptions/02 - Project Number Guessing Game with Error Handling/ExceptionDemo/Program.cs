

try
{
    int number = 0;
    int result = 10 / number;
}
catch (DivideByZeroException ex)
{
    Console.WriteLine("Cannot divide by zero: " + ex.Message);
}