using System.Security.Cryptography.X509Certificates;
using Markdig;
using MarkdownParser;
using System.IO;

namespace MarkdownParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string markdownFilePath = @"C:\Users\RRai\Documents\Form.md.txt";

            if(File.Exists(markdownFilePath))
            {
                string markdownFile = File.ReadAllText(markdownFilePath);
                if(string.IsNullOrEmpty(markdownFile))
                {
                    System.Console.WriteLine("There is no content in File ");
                }
                else{
                    System.Console.WriteLine("File exist and content in not null or empty.");
                    // Store the HTML content in a file
                    string htmlFilePath = Path.ChangeExtension(markdownFilePath, ".html");

                    File.WriteAllText(htmlFilePath,Service.ConvertMarkdownToHtml(markdownFilePath));

                    Console.WriteLine($"HTML content has been saved to: {htmlFilePath}");
                }
            } 
            else
            {
              System.Console.WriteLine("File does not exist at specified path.");
            }

        }
    }

}