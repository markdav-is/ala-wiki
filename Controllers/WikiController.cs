using Microsoft.AspNetCore.Mvc;
using AlaWiki.Core;
using AlaWiki.Models;

namespace AlaWiki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WikiController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public WikiController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("files")]
    public ActionResult<List<WikiFile>> GetFiles()
    {
        var repoPath = _configuration["WikiRepository:Path"];
        if (string.IsNullOrEmpty(repoPath))
        {
            return BadRequest("Repository path not configured");
        }

        var repository = new GitWikiRepository(repoPath);
        var files = repository.GetMarkdownFiles();
        return Ok(files);
    }

    [HttpGet("files/{*filePath}")]
    public ActionResult<WikiFile> GetFile(string filePath)
    {
        var repoPath = _configuration["WikiRepository:Path"];
        if (string.IsNullOrEmpty(repoPath))
        {
            return BadRequest("Repository path not configured");
        }

        var repository = new GitWikiRepository(repoPath);
        var file = repository.GetFile(filePath);
        
        if (file == null)
        {
            return NotFound($"File not found: {filePath}");
        }

        return Ok(file);
    }
}
