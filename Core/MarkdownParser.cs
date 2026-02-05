using Markdig;
using Markdig.Syntax;
using AlaWiki.Models;

namespace AlaWiki.Core;

public class MarkdownParser
{
    public MindMapNode ParseToMindMap(string markdownContent)
    {
        var document = Markdown.Parse(markdownContent);
        var root = new MindMapNode
        {
            Title = "Root",
            Level = 0
        };

        var nodeStack = new Stack<MindMapNode>();
        nodeStack.Push(root);

        foreach (var block in document)
        {
            if (block is HeadingBlock heading)
            {
                var node = new MindMapNode
                {
                    Title = heading.Inline?.FirstChild?.ToString() ?? "Untitled",
                    Level = heading.Level,
                    Content = ExtractContent(heading)
                };

                // Pop nodes from stack until we find the parent
                while (nodeStack.Count > 0 && nodeStack.Peek().Level >= heading.Level)
                {
                    nodeStack.Pop();
                }

                if (nodeStack.Count > 0)
                {
                    var parent = nodeStack.Peek();
                    node.ParentId = parent.Id;
                    parent.Children.Add(node);
                }

                nodeStack.Push(node);
            }
        }

        return root;
    }

    private string ExtractContent(HeadingBlock heading)
    {
        if (heading.Inline == null) return string.Empty;
        
        var content = new System.Text.StringBuilder();
        foreach (var inline in heading.Inline)
        {
            content.Append(inline.ToString());
        }
        return content.ToString();
    }
}
