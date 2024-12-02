string path = Path.Combine(Environment.CurrentDirectory, "input.txt");

var inputData = File.ReadAllLines(path);

var reports = inputData
    .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(level => int.Parse(level)))
    .ToList();

int safeReports = 0;

foreach (var report in reports)
{
    bool isSafe = IsReportSafe(report);

    if (!isSafe)
    {
        for (int i = 0; i < report.Count(); i++)
        {
            var reportAfterRemovingThisLevel = report.Where((level, index) => index != i);
            bool isSafeAfterRemovingThisLevel = IsReportSafe(reportAfterRemovingThisLevel);

            if (isSafeAfterRemovingThisLevel)
            {
                isSafe = true;
                break;
            }
        }
    }

    if (isSafe)
    {
        safeReports++;
    }
}

Console.WriteLine($"Safe reports: {safeReports}");

static bool IsReportSafe(IEnumerable<int> report)
{
    bool isSafe = true;
    bool increasing = false;

    for (int i = 0; i < report.Count() - 1; i++)
    {
        var currentLevel = report.ElementAt(i);
        var nextLevel = report.ElementAt(i + 1);
        if (i == 0)
        {
            increasing = currentLevel < nextLevel;
        }
        if (IsNextLevelUnsafe(increasing, currentLevel, nextLevel))
        {
            isSafe = false;
        }
    }
    return isSafe;
}

static bool IsNextLevelUnsafe(bool increasing, int currentLevel, int nextLevel)
{
    return currentLevel == nextLevel ||
                increasing && currentLevel > nextLevel ||
                !increasing && currentLevel < nextLevel ||
                Math.Abs(currentLevel - nextLevel) > 3;
}
