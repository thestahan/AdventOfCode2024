string path = Path.Combine(Environment.CurrentDirectory, "input.txt");
var inputData = File.ReadAllLines(path);

int xmasCount = 0;
int masCount = 0;

for (int i = 0; i < inputData.Length; i++)
{
    for (int j = 0; j < inputData[i].Length; j++)
    {
        char letter = inputData[i][j];

        if (letter == 'A' && IsMSInAnyDirection(inputData, j, i))
        {
            masCount++;
        }

        if (letter == 'X')
        {
            if (IsMASInDirection(letter, inputData, j, i, Direction.TopLeft))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.Top))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.TopRight))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.Left))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.Right))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.DownLeft))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.DownRight))
            {
                xmasCount++;
            }

            if (IsMASInDirection(letter, inputData, j, i, Direction.Down))
            {
                xmasCount++;
            }
        }
    }
}

Console.WriteLine($"Xmas count: {xmasCount}");
Console.WriteLine($"MAS count: {masCount}");

static bool IsMSInAnyDirection(string[] inputData, int currentX, int currentY)
{
    if (currentX - 1 < 0 || currentX + 1 >= inputData[0].Length || currentY - 1 < 0 || currentY + 1 >= inputData.Length)
    {
        return false;
    }

    int mCount = 0;
    int sCount = 0;

    char topLeftChar = inputData[currentY - 1][currentX - 1];
    char topRightChar = inputData[currentY - 1][currentX + 1];
    char bottomLeftChar = inputData[currentY + 1][currentX - 1];
    char bottomRightChar = inputData[currentY + 1][currentX + 1];

    if (topLeftChar == 'M' && bottomRightChar == 'M' ||
        topLeftChar == 'S' && bottomRightChar == 'S' ||
        topRightChar == 'M' && bottomLeftChar == 'M' ||
        topRightChar == 'S' && bottomLeftChar == 'S')
    {
        return false;
    }

    if (topLeftChar == 'M')
    {
        mCount++;
    }
    else if (topLeftChar == 'S')
    {
        sCount++;
    }

    if (topRightChar == 'M')
    {
        mCount++;
    }
    else if (topRightChar == 'S')
    {
        sCount++;
    }

    if (bottomLeftChar == 'M')
    {
        mCount++;
    }
    else if (bottomLeftChar == 'S')
    {
        sCount++;
    }

    if (bottomRightChar == 'M')
    {
        mCount++;
    }
    else if (bottomRightChar == 'S')
    {
        sCount++;
    }

    if (mCount == 2 && sCount == 2)
    {
        return true;
    }

    return false;
}

static bool IsMASInDirection(char letter, string[] inputData, int currentX, int currentY, Direction direction)
{
    switch (direction)
    {
        case Direction.Top:
            if (currentY - 3 < 0)
            {
                return false;
            }

            if (inputData[currentY - 1][currentX] == 'M' && inputData[currentY - 2][currentX] == 'A' && inputData[currentY - 3][currentX] == 'S')
            {
                return true;
            }

            break;

        case Direction.TopLeft:
            if (currentY - 3 < 0 || currentX - 3 < 0)
            {
                return false;
            }

            if (inputData[currentY - 1][currentX - 1] == 'M' && inputData[currentY - 2][currentX - 2] == 'A' && inputData[currentY - 3][currentX - 3] == 'S')
            {
                return true;
            }

            break;

        case Direction.TopRight:
            if (currentY - 3 < 0 || currentX + 3 >= inputData[0].Length)
            {
                return false;
            }

            if (inputData[currentY - 1][currentX + 1] == 'M' && inputData[currentY - 2][currentX + 2] == 'A' && inputData[currentY - 3][currentX + 3] == 'S')
            {
                return true;
            }

            break;

        case Direction.Down:
            if (currentY + 3 >= inputData.Length)
            {
                return false;
            }

            if (inputData[currentY + 1][currentX] == 'M' && inputData[currentY + 2][currentX] == 'A' && inputData[currentY + 3][currentX] == 'S')
            {
                return true;
            }

            break;

        case Direction.DownLeft:
            if (currentY + 3 >= inputData.Length || currentX - 3 < 0)
            {
                return false;
            }

            if (inputData[currentY + 1][currentX - 1] == 'M' && inputData[currentY + 2][currentX - 2] == 'A' && inputData[currentY + 3][currentX - 3] == 'S')
            {
                return true;
            }

            break;

        case Direction.DownRight:
            if (currentY + 3 >= inputData.Length || currentX + 3 >= inputData[0].Length)
            {
                return false;
            }

            if (inputData[currentY + 1][currentX + 1] == 'M' && inputData[currentY + 2][currentX + 2] == 'A' && inputData[currentY + 3][currentX + 3] == 'S')
            {
                return true;
            }

            break;

        case Direction.Left:
            if (currentX - 3 < 0)
            {
                return false;
            }

            if (inputData[currentY][currentX - 1] == 'M' && inputData[currentY][currentX - 2] == 'A' && inputData[currentY][currentX - 3] == 'S')
            {
                return true;
            }

            break;

        case Direction.Right:
            if (currentX + 3 >= inputData[0].Length)
            {
                return false;
            }

            if (inputData[currentY][currentX + 1] == 'M' && inputData[currentY][currentX + 2] == 'A' && inputData[currentY][currentX + 3] == 'S')
            {
                return true;
            }

            break;

        default:
            break;
    }

    return false;
}

internal enum Direction
{
    Top,
    TopLeft,
    TopRight,
    Down,
    DownLeft,
    DownRight,
    Left,
    Right
}
