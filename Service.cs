using Markdig;

namespace MarkdownParser
{
    class Service
    {
        public static string ConvertMarkdownToHtml(string markdownFilePath)
        {
        // Read markdown content from file
        string markdownContent = File.ReadAllText(markdownFilePath);
        
        var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Use<GitHubUserProfileExtension>().Build();
        // Convert markdown to HTML
        string htmlContent = Markdown.ToHtml(markdownContent,pipeline);

        // Replace image paths to match HTML rendering
        htmlContent = ReplaceImagePaths(htmlContent, Path.GetDirectoryName(@"c:\Users\RRai\Documents\Form.md.txt"));
    
        return htmlContent;
        }

      public static string ReplaceImagePaths(string htmlContent, string markdownDirectory)
      {
            // Replace image paths from Markdown to HTML
            htmlContent = htmlContent.Replace("src=\"", $"src=\"file://{markdownDirectory.Replace('\\', '/')}/");

            return htmlContent;
      }

    }
}