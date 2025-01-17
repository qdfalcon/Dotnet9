﻿global using Dotnet9.Commons;
global using Dotnet9.Contracts.Dto;
global using Dotnet9.Contracts.Dto.Abouts;
global using Dotnet9.Contracts.Dto.Albums;
global using Dotnet9.Contracts.Dto.Blogs;
global using Dotnet9.Contracts.Dto.Categories;
global using Dotnet9.Contracts.Dto.Donations;
global using Dotnet9.Contracts.Dto.FriendlyLinks;
global using Dotnet9.Contracts.Dto.Privacies;
global using Dotnet9.Contracts.Dto.Timelines;
global using Dotnet9.Contracts.Requests;
global using Dotnet9.Contracts.Tags;
global using Dotnet9.Service.Application.Abouts.Queries;
global using Dotnet9.Service.Application.Albums.Queries;
global using Dotnet9.Service.Application.Blogs.Commands;
global using Dotnet9.Service.Application.Blogs.Queries;
global using Dotnet9.Service.Application.Categories.Queries;
global using Dotnet9.Service.Application.Donations.Queries;
global using Dotnet9.Service.Application.FriendlyLinks.Commands;
global using Dotnet9.Service.Application.FriendlyLinks.Queries;
global using Dotnet9.Service.Application.Privacies.Queries;
global using Dotnet9.Service.Application.Tags.Queries;
global using Dotnet9.Service.Application.Timelines.Queries;
global using Dotnet9.Service.Domain.Aggregates.Abouts;
global using Dotnet9.Service.Domain.Aggregates.ActionLogs;
global using Dotnet9.Service.Domain.Aggregates.Albums;
global using Dotnet9.Service.Domain.Aggregates.Blogs;
global using Dotnet9.Service.Domain.Aggregates.Categories;
global using Dotnet9.Service.Domain.Aggregates.Comments;
global using Dotnet9.Service.Domain.Aggregates.Donations;
global using Dotnet9.Service.Domain.Aggregates.FriendlyLinks;
global using Dotnet9.Service.Domain.Aggregates.Privacies;
global using Dotnet9.Service.Domain.Aggregates.Users;
global using Dotnet9.Service.Domain.Aggregates.Tags;
global using Dotnet9.Service.Domain.Aggregates.Timelines;
global using Dotnet9.Service.Domain.Repositories;
global using Dotnet9.Service.Infrastructure;
global using Dotnet9.Service.Infrastructure.EFCore;
global using Dotnet9.Service.Infrastructure.Middleware;
global using Dotnet9.Service.Infrastructure.Repositories;
global using Dotnet9.Service.Services;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Mapster;
global using Masa.BuildingBlocks.Caching;
global using Masa.BuildingBlocks.Data;
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Ddd.Domain;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.BuildingBlocks.Ddd.Domain.Entities.Full;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
global using Masa.Contrib.Ddd.Domain.Repository.EFCore;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
global using Masa.Utils.Models;
global using Masa.Utils.Security.Cryptography;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Options;
global using Microsoft.OpenApi.Models;
global using System.Diagnostics;
global using System.Net;
global using System.Reflection;
global using System.Text.Json;
global using Microsoft.AspNetCore.HttpOverrides;
global using Dotnet9.ASPNETCore;
global using Microsoft.AspNetCore.Authorization;
global using Dotnet9.Contracts.Dto.Users;
global using Dotnet9.Service.Application.Auth.Commands;
global using Dotnet9.Service.Infrastructure.Constants;
global using Dotnet9.Contracts.Auth;
global using System.Text;
global using Microsoft.IdentityModel.Tokens;
global using Dotnet9.Service.Infrastructure.Extensions;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Dotnet9.Service.Infrastructure.Helpers;