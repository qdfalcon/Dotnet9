﻿using Microsoft.EntityFrameworkCore;
using System.Net;
using static Google.Rpc.Context.AttributeContext.Types;

namespace Dotnet9.Service.Infrastructure.Repositories;

public class BlogRepository : Repository<Dotnet9DbContext, Blog, Guid>, IBlogRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public BlogRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork,
        IMultilevelCacheClient multilevelCacheClient) : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public Task<Blog?> FindByIdAsync(Guid id)
    {
        return Context.Blogs.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Blog?> FindByTitleAsync(string title)
    {
        return Context.Blogs.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Title == title);
    }

    public async Task<BlogDetails?> FindBySlugAsync(string slug)
    {
        var blog = await Context.Blogs.Include(blogPost => blogPost.Albums)
            .Include(blogPost => blogPost.Categories).Include(blogPost => blogPost.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
        if (blog == null)
        {
            return null;
        }

        return new BlogDetails(blog.Id, blog.Title, blog.Slug, blog.Description, blog.Cover, blog.Content,
            blog.CopyrightType.ToString(), blog.Original, blog.OriginalTitle, blog.OriginalLink,
            (from blogPostCategory in blog.Categories
                join category in Context.Categories! on blogPostCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Name, category.Slug, category.Cover, category.Description, 0))
            .ToList(),
            blog.ViewCount,
            blog.CreationTime);
    }

    public Task<List<BlogBrief>> GetBlogBriefListAsync()
    {
        return Context.Blogs.Where(blog => blog.Banner).Take(10)
            .Select(blog => new BlogBrief(blog.Title, blog.Slug, blog.Description, blog.Cover, blog.CreationTime))
            .ToListAsync();
    }

    public async Task<GetBlogListByKeywordsResponse> GetBlogBriefListByKeywordsAsync(SearchBlogsByKeywordsQuery request)
    {
        TimeSpan? timeSpan = null;
        var keywords = WebUtility.UrlDecode(request.Keywords)?.ToLower();
        var key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByAlbumSlugAsync)}_{keywords}_{request.Page}_{request.PageSize}";
        var blogList = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var page = request.Page;
            var pageSize = request.PageSize;

            var query = Context.Blogs.AsQueryable();
            if (!request.Keywords.IsNullOrWhiteSpace())
            {
                query = query.Where(blog => EF.Functions.Like(blog.Title.ToLower(), $"%{keywords}%")
                                            || EF.Functions.Like(blog.Description.ToLower(), $"%{keywords}%"));
            }

            var dataListFromDb = query.OrderByDescending(x => x.CreationTime);
            var total = await dataListFromDb.CountAsync();
            var dataList = await dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).Select(x => new BlogBrief(x.Title, x.Slug, x.Description, x.Cover, x.CreationTime))
                .ToListAsync();


            if (dataList.Any())
            {
                var data = new GetBlogListByKeywordsResponse(true, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);

                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByKeywordsResponse>(data, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByKeywordsResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return blogList ?? new GetBlogListByKeywordsResponse(false);
    }

    public async Task<GetBlogListByAlbumSlugResponse> GetBlogBriefListByAlbumSlugAsync(SearchBlogsByAlbumQuery request)
    {
        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByAlbumSlugAsync)}_{request.AlbumSlug}_{request.Page}_{request.PageSize}";
        var blogList = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var album = await Context.Albums.FirstOrDefaultAsync(x => x.Slug == request.AlbumSlug);
            if (album == null)
            {
                return new CacheEntry<GetBlogListByAlbumSlugResponse>(null);
            }

            var query = Context.Blogs.AsQueryable();
            var dataListFromDb = query.OrderBy(x => x.CreationTime)
                .Include(x => x.Albums)
                .Where(x => x.Albums != null && x.Albums.Any(y => y.AlbumId == album.Id));
            var total = await dataListFromDb.CountAsync();
            var dataList = await dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).Select(x => new BlogBrief(x.Title, x.Slug, x.Description, x.Cover, x.CreationTime))
                .ToListAsync();


            if (dataList.Any())
            {
                var data = new GetBlogListByAlbumSlugResponse(true, album.Name, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);

                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByAlbumSlugResponse>(data, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByAlbumSlugResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return blogList ?? new GetBlogListByAlbumSlugResponse(false);
    }

    public async Task<GetBlogListByCategorySlugResponse> GetBlogBriefListByCategorySlugAsync(
        SearchBlogsByCategoryQuery request)
    {
        TimeSpan? timeSpan = null;
        var key =
            $"{nameof(BlogRepository)}_{nameof(GetBlogBriefListByCategorySlugAsync)}_{request.CategorySlug}_{request.Page}_{request.PageSize}";
        var blogList = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var page = request.Page;
            var pageSize = request.PageSize;
            var category = await Context.Categories.FirstOrDefaultAsync(x => x.Slug == request.CategorySlug);
            if (category == null)
            {
                return new CacheEntry<GetBlogListByCategorySlugResponse>(null);
            }

            var query = Context.Blogs.AsQueryable();
            var dataListFromDb = query.OrderBy(x => x.CreationTime)
                .Include(x => x.Categories)
                .Where(x => x.Categories != null && x.Categories.Any(y => y.CategoryId == category.Id));
            var total = await dataListFromDb.CountAsync();
            var dataList = await dataListFromDb.Skip((page - 1) * pageSize)
                .Take(pageSize).Select(x => new BlogBrief(x.Title, x.Slug, x.Description, x.Cover, x.CreationTime))
                .ToListAsync();


            if (dataList.Any())
            {
                var data = new GetBlogListByCategorySlugResponse(true, category.Name, dataList, total,
                    (total + pageSize - 1) / pageSize,
                    request.PageSize, request.Page);

                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<GetBlogListByCategorySlugResponse>(data, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<GetBlogListByCategorySlugResponse>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return blogList ?? new GetBlogListByCategorySlugResponse(false);
    }
}