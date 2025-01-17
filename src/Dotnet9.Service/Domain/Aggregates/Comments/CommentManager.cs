﻿namespace Dotnet9.Service.Domain.Aggregates.Comments;

public class CommentManager : IScopedDependency
{
    private readonly ICommentRepository _commentRepository;

    public CommentManager(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment> CreateAsync(
        Guid? parentId,
        string url,
        string userName,
        string email,
        string content)
    {
        var id = Guid.NewGuid();

        if (parentId is not null)
        {
            var existComment = await _commentRepository.FindAsync(parentId.Value);
            if (existComment == null)
            {
                throw new Exception($"不存在的父级留言: {parentId}");
            }
        }

        var comment = new Comment(id, parentId, url, userName, email, content);
        comment.SetCreationTime(DateTime.Now);
        return comment;
    }
}