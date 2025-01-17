﻿namespace Dotnet9.WebShare.Helpers;

public static class MarkdownHelper
{
    public static string Convert2Html(this string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePipeTables()
            .Build();

        return Markdig.Markdown.ToHtml(markdown, pipeline);
    }
}