using Microsoft.AspNetCore.Mvc;
using AlaWiki.Core;
using AlaWiki.Models;

namespace AlaWiki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MindMapController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public MindMapController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public ActionResult<MindMapNode> GetCompleteMindMap()
    {
        var repoPath = _configuration["WikiRepository:Path"];
        if (string.IsNullOrEmpty(repoPath))
        {
            return BadRequest("Repository path not configured");
        }

        var repository = new GitWikiRepository(repoPath);
        var service = new MindMapService(repository);
        var mindMap = service.GenerateCompleteMindMap();
        
        return Ok(mindMap);
    }

    [HttpGet("{*filePath}")]
    public ActionResult<MindMapNode> GetMindMapForFile(string filePath)
    {
        var repoPath = _configuration["WikiRepository:Path"];
        if (string.IsNullOrEmpty(repoPath))
        {
            return BadRequest("Repository path not configured");
        }

        try
        {
            var repository = new GitWikiRepository(repoPath);
            var service = new MindMapService(repository);
            var mindMap = service.GenerateMindMap(filePath);
            
            return Ok(mindMap);
        }
        catch (FileNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
