using Xunit;
using AlaWiki.Core;
using AlaWiki.Models;

namespace AlaWiki.Tests;

public class MarkdownParserTests
{
    private readonly MarkdownParser _parser;

    public MarkdownParserTests()
    {
        _parser = new MarkdownParser();
    }

    [Fact]
    public void ParseToMindMap_WithSimpleHeadings_CreatesCorrectHierarchy()
    {
        // Arrange
        var markdown = @"# Main Title

## Section One

### Subsection A

## Section Two";

        // Act
        var result = _parser.ParseToMindMap(markdown);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Root", result.Title);
        Assert.Single(result.Children); // Only main title is direct child of root
        
        var mainTitle = result.Children[0];
        Assert.Equal("Main Title", mainTitle.Title);
        Assert.Equal(1, mainTitle.Level);
        Assert.Equal(2, mainTitle.Children.Count); // Section One and Section Two
        
        var sectionOne = mainTitle.Children[0];
        Assert.Equal("Section One", sectionOne.Title);
        Assert.Equal(2, sectionOne.Level);
        
        var subsectionA = sectionOne.Children[0];
        Assert.Equal("Subsection A", subsectionA.Title);
        Assert.Equal(3, subsectionA.Level);
        
        var sectionTwo = mainTitle.Children[1];
        Assert.Equal("Section Two", sectionTwo.Title);
        Assert.Equal(2, sectionTwo.Level);
    }

    [Fact]
    public void ParseToMindMap_WithEmptyContent_ReturnsRootOnly()
    {
        // Arrange
        var markdown = "";

        // Act
        var result = _parser.ParseToMindMap(markdown);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Root", result.Title);
        Assert.Empty(result.Children);
    }

    [Fact]
    public void ParseToMindMap_WithNestedHeadings_CreatesDeepHierarchy()
    {
        // Arrange
        var markdown = @"# Level 1
## Level 2
### Level 3
#### Level 4
##### Level 5
###### Level 6";

        // Act
        var result = _parser.ParseToMindMap(markdown);

        // Assert
        Assert.NotNull(result);
        
        var level1 = result.Children[0];
        Assert.Equal(1, level1.Level);
        
        var level2 = level1.Children[0];
        Assert.Equal(2, level2.Level);
        
        var level3 = level2.Children[0];
        Assert.Equal(3, level3.Level);
        
        var level4 = level3.Children[0];
        Assert.Equal(4, level4.Level);
        
        var level5 = level4.Children[0];
        Assert.Equal(5, level5.Level);
        
        var level6 = level5.Children[0];
        Assert.Equal(6, level6.Level);
    }

    [Fact]
    public void ParseToMindMap_WithMultipleSiblings_GroupsCorrectly()
    {
        // Arrange
        var markdown = @"# Title

## First Sibling
## Second Sibling
## Third Sibling";

        // Act
        var result = _parser.ParseToMindMap(markdown);

        // Assert
        var title = result.Children[0];
        Assert.Equal("Title", title.Title);
        Assert.Equal(3, title.Children.Count);
        Assert.Equal("First Sibling", title.Children[0].Title);
        Assert.Equal("Second Sibling", title.Children[1].Title);
        Assert.Equal("Third Sibling", title.Children[2].Title);
    }
}
