using Xunit;
using AlaWiki.Core;
using AlaWiki.Models;

namespace AlaWiki.Tests;

public class MindMapServiceTests
{
    [Fact]
    public void GenerateCompleteMindMap_WithNoFiles_ReturnsRootNode()
    {
        // Arrange
        var tempRepo = CreateTempGitRepo();
        var service = new MindMapService(new GitWikiRepository(tempRepo));

        // Act
        var result = service.GenerateCompleteMindMap();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Wiki Root", result.Title);
        Assert.Equal(0, result.Level);
        Assert.Empty(result.Children);

        // Cleanup
        Directory.Delete(tempRepo, true);
    }

    [Fact]
    public void GenerateMindMap_WithNonExistentFile_ThrowsException()
    {
        // Arrange
        var tempRepo = CreateTempGitRepo();
        var service = new MindMapService(new GitWikiRepository(tempRepo));

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => 
            service.GenerateMindMap("nonexistent.md"));

        // Cleanup
        Directory.Delete(tempRepo, true);
    }

    private string CreateTempGitRepo()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);
        
        var processInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "git",
            Arguments = "init",
            WorkingDirectory = tempPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        var process = System.Diagnostics.Process.Start(processInfo);
        process?.WaitForExit();
        
        // Create initial commit
        File.WriteAllText(Path.Combine(tempPath, ".gitkeep"), "");
        
        var configEmailInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "git",
            Arguments = "config user.email \"test@test.com\"",
            WorkingDirectory = tempPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        System.Diagnostics.Process.Start(configEmailInfo)?.WaitForExit();
        
        var configNameInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "git",
            Arguments = "config user.name \"Test User\"",
            WorkingDirectory = tempPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        System.Diagnostics.Process.Start(configNameInfo)?.WaitForExit();
        
        var addInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "git",
            Arguments = "add .",
            WorkingDirectory = tempPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        System.Diagnostics.Process.Start(addInfo)?.WaitForExit();
        
        var commitInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = "git",
            Arguments = "commit -m \"Initial commit\"",
            WorkingDirectory = tempPath,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        System.Diagnostics.Process.Start(commitInfo)?.WaitForExit();
        
        return tempPath;
    }
}
