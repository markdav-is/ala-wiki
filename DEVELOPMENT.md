# Development Guide

## Project Structure

```
ala-wiki/
├── Controllers/           # API endpoints
│   ├── WikiController.cs     # Wiki file operations
│   └── MindMapController.cs  # Mind map generation
├── Core/                 # Business logic
│   ├── GitWikiRepository.cs  # Git repository operations
│   ├── MarkdownParser.cs     # Markdown to mind map conversion
│   └── MindMapService.cs     # Mind map generation service
├── Models/               # Data models
│   ├── MindMapNode.cs        # Mind map node structure
│   └── WikiFile.cs           # Wiki file representation
├── Tests/                # Unit tests
│   ├── MarkdownParserTests.cs
│   └── MindMapServiceTests.cs
├── wwwroot/              # Static web files
│   └── index.html            # Main UI with Mermaid.js
└── Program.cs            # Application startup
```

## Running the Application

1. **Configure Repository Path**
   Edit `appsettings.json`:
   ```json
   {
     "WikiRepository": {
       "Path": "/path/to/your/git/wiki"
     }
   }
   ```

2. **Run the Application**
   ```bash
   dotnet run
   ```

3. **Access the Web UI**
   Open browser to `http://localhost:5000`

4. **Test API Endpoints**
   Use the provided `AlaWiki.http` file with REST Client extension

## Running Tests

```bash
cd Tests
dotnet test
```

## How Mind Maps Work

1. **Markdown Parsing**: The `MarkdownParser` uses Markdig to parse markdown files
2. **Hierarchy Extraction**: Headings (H1-H6) are extracted and organized into a tree structure
3. **Node Generation**: Each heading becomes a `MindMapNode` with title, level, and children
4. **Visualization**: Mermaid.js renders the tree as an interactive mind map

## API Usage Examples

### List All Files
```http
GET /api/wiki/files
```

### Get Mind Map for All Files
```http
GET /api/mindmap
```

### Get Mind Map for Specific File
```http
GET /api/mindmap/MyDocument.md
```

## Extending the Application

### Adding New Endpoints
1. Create a new controller in `Controllers/`
2. Inject dependencies in constructor
3. Add action methods with appropriate HTTP attributes

### Modifying Mind Map Generation
1. Update `MarkdownParser.cs` for custom parsing logic
2. Extend `MindMapNode.cs` for additional metadata
3. Update tests to cover new behavior

### Customizing the UI
1. Edit `wwwroot/index.html`
2. Modify Mermaid.js theme in JavaScript initialization
3. Add new visualization options or export formats

## Dependencies

- **LibGit2Sharp**: Git repository access
- **Markdig**: Markdown parsing
- **Mermaid.js**: Mind map visualization (CDN)
- **xUnit**: Testing framework

## Future Enhancements

Potential improvements for future development:

1. **Bidirectional Editing**: Allow editing mind maps and sync back to markdown
2. **Multi-Repository Support**: Manage multiple wiki repositories
3. **Search & Filter**: Advanced search across all wiki content
4. **Export Options**: Export mind maps as PNG, SVG, or PDF
5. **Real-time Collaboration**: Multiple users editing simultaneously
6. **Authentication**: User management and access control
7. **Caching**: Improve performance for large wikis
8. **Git Operations**: Commit, push, pull directly from UI
