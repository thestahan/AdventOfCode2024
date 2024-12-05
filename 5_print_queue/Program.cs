string path = Path.Combine(Environment.CurrentDirectory, "input.txt");
var inputData = File.ReadAllLines(path);

var orderData = inputData
    .Where(x => x.Contains('|'))
    .Select(x => x.Split('|'))
    .Select(x => new { FirstNumber = int.Parse(x[0]), SecondNumber = int.Parse(x[1]) });

Dictionary<int, NumberOrder> numbersOrder = new Dictionary<int, NumberOrder>();

foreach (var order in orderData)
{
    if (!numbersOrder.ContainsKey(order.FirstNumber))
    {
        numbersOrder.Add(order.FirstNumber, new NumberOrder { SmallerThen = new List<int> { order.SecondNumber } });
    }
    else
    {
        numbersOrder[order.FirstNumber].SmallerThen.Add(order.SecondNumber);
    }

    if (!numbersOrder.ContainsKey(order.SecondNumber))
    {
        numbersOrder.Add(order.SecondNumber, new NumberOrder { GreaterThen = new List<int> { order.FirstNumber } });
    }
    else
    {
        numbersOrder[order.SecondNumber].GreaterThen.Add(order.FirstNumber);
    }
}

var printQueueData = inputData.Where(x => x.Contains(',')).Select(x => x.Split(','));
var printQueue = new List<int[]>();
foreach (var print in printQueueData)
{
    printQueue.Add(print.Select(int.Parse).ToArray());
}

int correctQueuesCount = 0;
int incorrectQueuesCount = 0;

foreach (var queue in printQueue)
{
    bool isCorrectOrder = true;

    for (int i = 0; i < queue.Length - 1; i++)
    {
        var leftNumber = queue[i];

        for (int j = i + 1; j < queue.Length; j++)
        {
            var rightNubmer = queue[j];

            if (orderData.Any(x => x.SecondNumber == leftNumber && x.FirstNumber == rightNubmer))
            {
                isCorrectOrder = false;

                break;
            }
        }

        if (!isCorrectOrder)
        {
            break;
        }
    }

    if (isCorrectOrder)
    {
        correctQueuesCount += queue.ElementAt((queue.Length - 1) / 2);
    }

    if (!isCorrectOrder)
    {
        for (int i = 0; i < queue.Length - 1; i++)
        {
            for (int j = i + 1; j < queue.Length; j++)
            {
                var leftNumber = queue[i];
                var rightNubmer = queue[j];

                if (numbersOrder[leftNumber].GreaterThen.Contains(rightNubmer))
                {
                    var temp = queue[i];
                    queue[i] = queue[j];
                    queue[j] = temp;
                }
            }
        }

        incorrectQueuesCount += queue.ElementAt((queue.Length - 1) / 2);
    }
}

Console.WriteLine($"Correct order queues: {correctQueuesCount}");
Console.WriteLine($"Incorrect order queues: {incorrectQueuesCount}");

internal class NumberOrder
{
    public List<int> GreaterThen { get; set; } = new List<int>();
    public List<int> SmallerThen { get; set; } = new List<int>();
}
