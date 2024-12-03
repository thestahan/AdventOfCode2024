string path = Path.Combine(Environment.CurrentDirectory, "input.txt");
var inputData = File.ReadAllLines(path);

long mulsSum = 0;

bool enableMult = true;

foreach (var inputLine in inputData)
{
    var muls = inputLine.Split("mul(");

    foreach (var mul in muls.Skip(1))
    {
        var numbersPair = mul.Split(")");
        var numbers = numbersPair[0].Split(",");
        if (numbers.Length != 2)
        {
            continue;
        }

        if (int.TryParse(numbers[0], out var a) && int.TryParse(numbers[1], out var b))
        {
            if (enableMult)
            {
                mulsSum += a * b;
            }
        }

        if (mul.Contains("don't()"))
        {
            enableMult = false;
        }

        if (mul.Contains("do()"))
        {
            enableMult = true;
        }
    }
}

Console.WriteLine($"Part two: {mulsSum}");
