# Ala Wiki - Mind Maps for Git-Based Wikis

A web application that visualizes Git-based wiki content as interactive mind maps, making it easy to understand the structure and hierarchy of your documentation.

## Features

- **Git Integration**: Reads markdown files directly from Git repositories
- **Automatic Mind Map Generation**: Converts markdown heading hierarchy into visual mind maps
- **Interactive Visualization**: Beautiful, interactive mind maps using Mermaid.js
- **Multiple View Modes**: View individual files or all wiki content in one comprehensive map
- **RESTful API**: Clean API for programmatic access to wiki content and mind maps

## Quick Start

### Prerequisites

- .NET 10.0 SDK or later
- Git repository with markdown files

### Installation

1. Clone this repository
2. Configure your wiki repository path in `appsettings.json`:
   ```json
   {
     "WikiRepository": {
       "Path": "/path/to/your/wiki/repo"
     }
   }
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

4. Open your browser to `http://localhost:5000`

## Usage

### Web Interface

1. Select a markdown file from the dropdown menu
2. Click "Load Mind Map" to visualize that file's structure
3. Or click "Load All Files" to see all wiki pages in one view

### API Endpoints

- `GET /api/wiki/files` - List all markdown files in the repository
- `GET /api/wiki/files/{filePath}` - Get content of a specific file
- `GET /api/mindmap` - Generate mind map for all files
- `GET /api/mindmap/{filePath}` - Generate mind map for a specific file

## How It Works

The application:
1. Reads markdown files from your Git repository using LibGit2Sharp
2. Parses markdown headings (H1-H6) to extract document structure
3. Converts the hierarchy into a tree-based mind map model
4. Serves both raw data via API and visual representation via web UI

## Architecture

- **Core**: Business logic for Git operations and markdown parsing
- **Controllers**: REST API endpoints
- **Models**: Data models for wiki files and mind map nodes
- **wwwroot**: Static web frontend with Mermaid.js visualization

## License

See LICENSE file for details.