namespace Noite.Models;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public class Node
{
    public string? Id { get; set; }
    public string? data { get; set; }
    public Position Position { get; set; } = new Position(0, 0);
    public string? Type { get; set; }
}
