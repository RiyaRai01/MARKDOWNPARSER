using Markdig;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using System.Text.RegularExpressions;
namespace MarkdownParser
{
public class GitHubUserProfileExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        if (!pipeline.InlineParsers.Contains<GitHubUserProfileParser>())
        {
            pipeline.InlineParsers.Insert(0, new GitHubUserProfileParser());
        }
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
         
    }
}

public partial class GitHubUserProfileParser : InlineParser
{
    public GitHubUserProfileParser()
    {
        OpeningCharacters = new[] { '[' };
    }

    public override bool Match(InlineProcessor processor, ref StringSlice slice)
    {
        var precedingCharacter = slice.PeekCharExtra(-1);
        if (!precedingCharacter.IsWhiteSpaceOrZero())
        {
            return false;
        }

        var regex = GithubTagRegex();
        var match = regex.Match(slice.ToString());

        if (!match.Success)
        {
            return false;
        }

        var username = match.Groups["username"].Value;
        var literal = $"<a href=\"https://github.com/{username}\">{username}</a>";

        processor.Inline = new HtmlInline(literal)
        {
            Span =
            {
                Start = processor.GetSourcePosition(slice.Start, out var line, out var column)
            },
            Line = line,
            Column = column,
            IsClosed = true
        };
        processor.Inline.Span.End = processor.Inline.Span.Start + match.Length - 1;
        slice.Start += match.Length;
        return true;
    }

    [GeneratedRegex(@"\[github:(?<username>\w+)]")]
    private static partial Regex GithubTagRegex();
}

public static class MarkdownConfigModel
{
    public static MarkdownPipeline Pipeline { get; }

    static MarkdownConfigModel()
    {
        Pipeline = new MarkdownPipelineBuilder()
            .Use<GitHubUserProfileExtension>()
            .Build();
    }
}
}