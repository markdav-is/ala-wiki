using LibGit2Sharp;
using AlaWiki.Models;

namespace AlaWiki.Core;

public class GitWikiRepository
{
    private readonly string _repositoryPath;

    public GitWikiRepository(string repositoryPath)
    {
        _repositoryPath = repositoryPath;
    }

    public List<WikiFile> GetMarkdownFiles()
    {
        var files = new List<WikiFile>();

        if (!Repository.IsValid(_repositoryPath))
        {
            return files;
        }

        using var repo = new Repository(_repositoryPath);
        var commit = repo.Head.Tip;

        if (commit == null)
        {
            return files;
        }

        var tree = commit.Tree;
        foreach (var entry in tree)
        {
            if (entry.TargetType == TreeEntryTargetType.Blob && 
                (entry.Name.EndsWith(".md") || entry.Name.EndsWith(".markdown")))
            {
                var blob = (Blob)entry.Target;
                files.Add(new WikiFile
                {
                    Path = entry.Path,
                    Content = blob.GetContentText(),
                    CommitSha = commit.Sha
                });
            }
        }

        return files;
    }

    public WikiFile? GetFile(string filePath)
    {
        if (!Repository.IsValid(_repositoryPath))
        {
            return null;
        }

        using var repo = new Repository(_repositoryPath);
        var commit = repo.Head.Tip;

        if (commit == null)
        {
            return null;
        }

        var entry = commit[filePath];
        if (entry == null || entry.TargetType != TreeEntryTargetType.Blob)
        {
            return null;
        }

        var blob = (Blob)entry.Target;
        return new WikiFile
        {
            Path = filePath,
            Content = blob.GetContentText(),
            CommitSha = commit.Sha
        };
    }

    public static Repository? CloneOrOpen(string url, string localPath)
    {
        try
        {
            if (Repository.IsValid(localPath))
            {
                return new Repository(localPath);
            }

            if (Directory.Exists(localPath))
            {
                Directory.Delete(localPath, true);
            }

            Repository.Clone(url, localPath);
            return new Repository(localPath);
        }
        catch
        {
            return null;
        }
    }
}
