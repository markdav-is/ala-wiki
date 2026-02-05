using AlaWiki.Models;

namespace AlaWiki.Core;

public class MindMapService
{
    private readonly GitWikiRepository _repository;
    private readonly MarkdownParser _parser;

    public MindMapService(GitWikiRepository repository)
    {
        _repository = repository;
        _parser = new MarkdownParser();
    }

    public MindMapNode GenerateMindMap(string filePath)
    {
        var file = _repository.GetFile(filePath);
        if (file == null)
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        return _parser.ParseToMindMap(file.Content);
    }

    public MindMapNode GenerateCompleteMindMap()
    {
        var root = new MindMapNode
        {
            Title = "Wiki Root",
            Level = 0
        };

        var files = _repository.GetMarkdownFiles();
        foreach (var file in files)
        {
            var fileNode = new MindMapNode
            {
                Title = Path.GetFileNameWithoutExtension(file.Path),
                Level = 1,
                ParentId = root.Id
            };

            var contentMap = _parser.ParseToMindMap(file.Content);
            fileNode.Children = contentMap.Children;

            root.Children.Add(fileNode);
        }

        return root;
    }
}
