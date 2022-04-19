﻿using AutoMapper;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Categories;
using Dotnet9.Web.ViewModels.Blogs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.ViewComponents.Blogs;

public class WordBlogPosts3 : ViewComponent
{
    private readonly ICacheService _cacheService;
    private readonly ICategoryAppService _categoryAppService;
    private readonly IMapper _mapper;

    public WordBlogPosts3(ICategoryAppService categoryAppService, ICacheService cacheService, IMapper mapper)
    {
        _categoryAppService = categoryAppService;
        _cacheService = cacheService;
        _mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        const string cacheKey = $"{nameof(WordBlogPosts3)}-{nameof(InvokeAsync)}";
        var cacheData = await _cacheService.GetAsync<WordBlogPostsViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        var categories = new[]
        {
            "Large-front-end-flutter", "Large-front-end-vue", "database-mysql", "database-sqlite"
        };

        cacheData = new WordBlogPostsViewModel
        {
            Items = new Dictionary<string, CategoryWordBlogPosts>()
        };
        foreach (var categorySlug in categories)
        {
            var categoryViewMode = await _categoryAppService.GetCategoryAsync(categorySlug);
            if (categoryViewMode != null)
                cacheData.Items[categoryViewMode.Name] = new CategoryWordBlogPosts
                {
                    Name = categoryViewMode.Name,
                    Slug = categorySlug,
                    BlogPosts = categoryViewMode.Items
                };
        }

        await _cacheService.ReplaceAsync(cacheKey, cacheData, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}