string path = Path.Combine(Environment.CurrentDirectory, "input.txt");
var inputData = File.ReadAllLines(path);

var listOne = new List<int>();
var listTwo = new List<int>();

foreach (var line in inputData)
{
    var numbers = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    var num1 = int.Parse(numbers[0]);
    InsertNumberInAscendingOrder(listOne, num1);

    var num2 = int.Parse(numbers[1]);
    InsertNumberInAscendingOrder(listTwo, num2);
}

// Part one
long partOneResult = 0;

for (int i = 0; i < listOne.Count; i++)
{
    partOneResult += Math.Abs(listOne[i] - listTwo[i]);
}

Console.WriteLine($"Part one: {partOneResult}");

// Part two
var listTwoDuplicates = new Dictionary<int, int>();
foreach (var number in listTwo)
{
    if (!listTwoDuplicates.TryAdd(number, 1))
    {
        listTwoDuplicates[number]++;
    }
}

long partTwoResult = 0;

foreach (var number in listOne)
{
    var value = listTwoDuplicates.TryGetValue(number, out var count) ? count : 0;

    partTwoResult += number * value;
}

Console.WriteLine($"Part two: {partTwoResult}");

static void InsertNumberInAscendingOrder(List<int> list, int number)
{
    if (list.Count == 0)
    {
        list.Add(number);
        return;
    }

    for (int i = 0; i < list.Count; i++)
    {
        if (number < list[i])
        {
            list.Insert(i, number);
            return;
        }
    }

    list.Add(number);
}
