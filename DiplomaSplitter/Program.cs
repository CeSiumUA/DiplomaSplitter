Console.WriteLine("Enter file path:");

var filePath = Console.ReadLine();

if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
{
    Console.WriteLine("File not found!");
    return;
}


int symbolsCountSplit;
Console.WriteLine("Enter symbols to split:");
var convertionSucceedded = int.TryParse(Console.ReadLine()!, out symbolsCountSplit);

if (!convertionSucceedded)
{
    Console.WriteLine("Invalid number!");
    return;
}

const string DefaultOutputFolderName = "output";

Console.WriteLine($"Output directory ({DefaultOutputFolderName} by default):");

var outputDirectory = Console.ReadLine();
if (string.IsNullOrEmpty(outputDirectory))
{
    outputDirectory = DefaultOutputFolderName;
}

var allText = await File.ReadAllTextAsync(filePath!);

if (Directory.Exists(outputDirectory))
{
    Directory.Delete(outputDirectory, true);
}

Directory.CreateDirectory(outputDirectory);

for(int x = 0; x < Math.Ceiling((double)allText.Length / symbolsCountSplit); x++)
{
    var textPartition = new string(allText.Skip(x * symbolsCountSplit).Take(symbolsCountSplit).ToArray());
    await File.WriteAllTextAsync(Path.Combine(DefaultOutputFolderName, $"text_{x + 1}.txt"), textPartition);
}