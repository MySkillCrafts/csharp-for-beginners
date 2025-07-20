float floatExample = 1f / 3f;
double doubleExample = 1d / 3d;
decimal decimalExample = 1m / 3m;

Console.WriteLine("Precision Comparison:");
Console.WriteLine("-------------------------");
Console.WriteLine($"float: {floatExample}");
Console.WriteLine($"double: {doubleExample}");
Console.WriteLine($"decimal: {decimalExample}");

// Coordinates in 3D space
float posX = 10.5f;
float posY = -7.25f;
float posZ = 3.0f;

// Color in RGBA (0 to 1 range)
float red = 0.3f;
float green = 0.6f;
float blue = 0.9f;
float alpha = 1.0f;

double temperature = 36.6;
double distanceInKm = 12.75;
double averageScore = 87.5;
double discountRate = 0.15;

decimal pricePerItem = 19.99m;
decimal taxRate = 0.23m;
decimal totalPrice = pricePerItem * (1 + taxRate);

Console.WriteLine($"Total price: {totalPrice}");