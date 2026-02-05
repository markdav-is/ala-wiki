namespace AlaWiki.Models;

public class WikiFile
{
    public string Path { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string CommitSha { get; set; } = string.Empty;
}
