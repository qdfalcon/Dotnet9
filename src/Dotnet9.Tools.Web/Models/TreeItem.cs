﻿namespace Dotnet9.Tools.Web.Models;

public class TreeItem
{
    public TreeItem(TreeItemKind kind, string text, string url, object? tag = null, string? icon = null)
    {
        Id = Guid.NewGuid().ToString();
        Kind = kind;
        Text = text;
        Url = url;
        Tag = tag;
        Icon = icon;
    }

    public string Id { get; set; }
    public TreeItemKind Kind { get; set; }
    public string Text { get; set; }
    public string Url { get; set; }
    public string? Icon { get; set; }
    public object? Tag { get; set; }
    public List<TreeItem>? Children { get; set; }
}