﻿using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Categories;

public class CategoryCountDto : EntityDto<Guid>
{
    public Guid? ParentId { get; set; }

    public string Name { get; set; }

    public string CoverImageUrl { get; set; }

    public string Description { get; set; }

    public int BlogPostCount { get; set; }
}