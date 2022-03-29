﻿using Dotnet9.Domain;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Shared.Albums;
using Dotnet9.Domain.Shared.Blogs;
using Dotnet9.Domain.Shared.Categories;
using Dotnet9.Domain.Shared.Tags;
using Dotnet9.Domain.Shared.UrlLinks;
using Dotnet9.Domain.Tags;
using Dotnet9.Domain.UrlLinks;
using Dotnet9.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

public class Dotnet9DbContext : DbContext
{
    public Dotnet9DbContext(DbContextOptions<Dotnet9DbContext> options) : base(options)
    {
    }

    public DbSet<BlogPost>? BlogPosts { get; set; }
    public DbSet<Album>? Albums { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Tag>? Tags { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<UrlLink>? UrlLinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Album>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Albums", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(AlbumConsts.MaxNameLength);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(AlbumConsts.MaxSlugLength);
            b.Property(x => x.Cover).IsRequired().HasMaxLength(AlbumConsts.MaxCoverLength);
            b.Property(x => x.Description).HasMaxLength(AlbumConsts.MaxDescriptionLength);
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Slug);
        });

        modelBuilder.Entity<Category>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Categories", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(CategoryConsts.MaxNameLength);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(CategoryConsts.MaxSlugLength);
            b.Property(x => x.Cover).IsRequired().HasMaxLength(CategoryConsts.MaxCoverLength);
            b.Property(x => x.Description).HasMaxLength(CategoryConsts.MaxDescriptionLength);
            b.Property(x => x.ParentId);
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Slug);
        });

        modelBuilder.Entity<Tag>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}Tags", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength);
            b.HasIndex(x => x.Name);
        });

        modelBuilder.Entity<BlogPost>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPosts", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(BlogPostConsts.MaxTitleLength);
            b.Property(x => x.Slug).IsRequired().HasMaxLength(BlogPostConsts.MaxSlugLength);
            b.Property(x => x.BriefDescription).HasMaxLength(BlogPostConsts.MaxBriefDescriptionLength);
            b.Property(x => x.Content).IsRequired().HasMaxLength(BlogPostConsts.MaxContentLength);
            b.Property(x => x.Cover).HasMaxLength(BlogPostConsts.MaxCoverLength);
            b.Property(x => x.CopyrightType);
            b.Property(x => x.Original).HasMaxLength(BlogPostConsts.MaxOriginalLength);
            b.Property(x => x.OriginalTitle).HasMaxLength(BlogPostConsts.MaxOriginalTitleLength);
            b.Property(x => x.OriginalLink).HasMaxLength(BlogPostConsts.MaxOriginalLinkLength);
            b.HasIndex(x => x.Title);
            b.HasIndex(x => x.Slug);
            b.HasMany(x => x.Albums).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasMany(x => x.Categories).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasMany(x => x.Tags).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
        });

        modelBuilder.Entity<BlogPostAlbum>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostAlbums", Dotnet9Consts.DbSchema);
            b.HasKey(x => new {x.BlogPostId, x.AlbumId});
            b.HasOne<BlogPost>().WithMany(x => x.Albums).HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasOne<Album>().WithMany().HasForeignKey(x => x.AlbumId).IsRequired();
            b.HasIndex(x => new {x.BlogPostId, x.AlbumId});
        });

        modelBuilder.Entity<BlogPostCategory>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostCategories", Dotnet9Consts.DbSchema);
            b.HasKey(x => new {x.BlogPostId, x.CategoryId});
            b.HasOne<BlogPost>().WithMany(x => x.Categories).HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
            b.HasIndex(x => new {x.BlogPostId, x.CategoryId});
        });

        modelBuilder.Entity<BlogPostTag>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostTags", Dotnet9Consts.DbSchema);
            b.HasKey(x => new {x.BlogPostId, x.TagId});
            b.HasOne<BlogPost>().WithMany(x => x.Tags).HasForeignKey(x => x.BlogPostId).IsRequired();
            b.HasOne<Tag>().WithMany().HasForeignKey(x => x.TagId).IsRequired();
            b.HasIndex(x => new {x.BlogPostId, x.TagId});
        });

        modelBuilder.Entity<UrlLink>(b =>
        {
            b.ToTable($"{Dotnet9Consts.DbTablePrefix}UrlLinks", Dotnet9Consts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(UrlLinkConsts.MaxNameLength);
            b.Property(x => x.Url).IsRequired().HasMaxLength(UrlLinkConsts.MaxUrlLength);
            b.Property(x => x.Description).HasMaxLength(UrlLinkConsts.MaxDescriptionLength);
            b.Property(x => x.Kind);
            b.Property(x => x.Index).IsRequired();
            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Url);
        });
    }
}