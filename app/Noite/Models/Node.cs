namespace Noite.Models;

public class Position
{
    public double X { get; set; }
    public double Y { get; set; }

    public Position(double x, double y)
    {
        this.X = x;
        this.Y = y;
    }
}

public class Node
{
    public string? Id { get; set; }
    public string? data { get; set; }
    public Position Position { get; set; } = new Position(0d, 0d);
    public string? Type { get; set; }
}
