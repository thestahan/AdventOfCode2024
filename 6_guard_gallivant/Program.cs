string path = Path.Combine(Environment.CurrentDirectory, "input.txt");
var inputData = File.ReadAllLines(path);

char[,] map = new char[inputData.Length, inputData[0].Length];
bool[,] visited = new bool[inputData.Length, inputData[0].Length];

int startingX = 0;
int startingY = 0;

int guardX = 0;
int guardY = 0;

for (int i = 0; i < inputData.Length; i++)
{
    for (int j = 0; j < inputData[i].Length; j++)
    {
        map[i, j] = inputData[i][j];

        if (map[i, j] == '^')
        {
            startingX = j;
            startingY = i;

            guardX = startingX;
            guardY = startingY;

            visited[guardY, guardX] = true;
        }
    }
}

var currentDirection = Direction.Up;

while (true)
{
    var nextPosition = GetNextPosition(currentDirection, guardY, guardX);

    bool isNextPositionValid = nextPosition.x >= 0 && nextPosition.x < map.GetLength(1) && nextPosition.y >= 0 && nextPosition.y < map.GetLength(0);
    if (!isNextPositionValid)
    {
        break;
    }

    bool isNextPositionBlocked = map[nextPosition.y, nextPosition.x] == '#';
    if (isNextPositionBlocked)
    {
        currentDirection = ChangeDirection90DegRight(currentDirection);
    }
    else
    {
        map[guardY, guardX] = 'X';

        guardX = nextPosition.x;
        guardY = nextPosition.y;

        visited[guardY, guardX] = true;
    }
}

int visitedPositions = 0;
for (int i = 0; i < visited.GetLength(0); i++)
{
    for (int j = 0; j < visited.GetLength(1); j++)
    {
        if (visited[i, j])
        {
            visitedPositions++;
        }
    }
}

int infiniteLoops = 0;
for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        if (map[i, j] == 'X')
        {
            map[i, j] = '#';

            if (IsInfiniteLoop(map, startingX, startingY))
            {
                infiniteLoops++;
            }

            map[i, j] = 'X';
        }
    }
}

Console.WriteLine($"Part one: {visitedPositions}");

Console.WriteLine($"Part two: {infiniteLoops}");

static bool IsInfiniteLoop(char[,] map, int startingX, int startingY)
{
    var visited = new Dictionary<(int x, int y), int>();

    int guardX = startingX;
    int guardY = startingY;

    var currentDirection = Direction.Up;

    while (true)
    {
        var nextPosition = GetNextPosition(currentDirection, guardY, guardX);

        bool isNextPositionValid = nextPosition.x >= 0 && nextPosition.x < map.GetLength(1) && nextPosition.y >= 0 && nextPosition.y < map.GetLength(0);
        if (!isNextPositionValid)
        {
            break;
        }

        bool isNextPositionBlocked = map[nextPosition.y, nextPosition.x] == '#';
        if (isNextPositionBlocked)
        {
            currentDirection = ChangeDirection90DegRight(currentDirection);
        }
        else
        {
            map[guardY, guardX] = 'X';

            guardX = nextPosition.x;
            guardY = nextPosition.y;

            int visitedCount = visited.GetValueOrDefault((guardX, guardY));

            if (visitedCount >= 4 && guardX != startingX && guardY != startingY)
            {
                return true;
            }

            visited[(guardX, guardY)] = visitedCount + 1;
        }
    }

    return false;
}

static (int y, int x) GetNextPosition(Direction direction, int y, int x)
{
    switch (direction)
    {
        case Direction.Up:
            y--;
            break;

        case Direction.Down:
            y++;
            break;

        case Direction.Left:
            x--;
            break;

        case Direction.Right:
            x++;
            break;
    }

    return (y, x);
}

static Direction ChangeDirection90DegRight(Direction direction)
{
    return direction switch
    {
        Direction.Up => Direction.Right,
        Direction.Down => Direction.Left,
        Direction.Left => Direction.Up,
        Direction.Right => Direction.Down,
        _ => direction,
    };
}

internal enum Direction
{
    Up,
    Down,
    Left,
    Right
}
