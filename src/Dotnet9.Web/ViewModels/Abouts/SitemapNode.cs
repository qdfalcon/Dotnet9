﻿namespace Dotnet9.Web.ViewModels.Abouts;

public class SitemapNode
{
    public string Url { get; set; } = null!;
    public DateTime LastModified { get; set; }
    public SitemapFrequency Frequency { get; set; }
    public double Priority { get; set; }
}

public enum SitemapFrequency
{
    Monthly,
    Daily
}