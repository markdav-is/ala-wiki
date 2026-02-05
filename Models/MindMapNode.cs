namespace AlaWiki.Models;

public class MindMapNode
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Content { get; set; } = string.Empty;
    public string? ParentId { get; set; }
    public List<MindMapNode> Children { get; set; } = new();
}
