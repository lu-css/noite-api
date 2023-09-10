namespace Noite.Models;

public class TableNodeModel : GenericNodeModel
{
    public List<PropertieNode> Properties { get; set; } = new List<PropertieNode>();
    public Node? Node { get; set; }
}

