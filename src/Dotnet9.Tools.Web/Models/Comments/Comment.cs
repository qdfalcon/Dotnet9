﻿namespace Dotnet9.Tools.Web.Models.Comments;

public class Comment
{
    public string? Id { get; set; }

    public string? UrlMark { get; set; }

    public string? UserName { get; set; }

    public long Timestamp { get; set; }

    public string? Content { get; set; }

    public CommentReplyKind? ReplyKind { get; set; }

    public string? ReplyId { get; set; }
}