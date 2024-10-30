// -----------------------------------------------------------------------
// <copyright file="ImageManagerExtensions.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pixsys.Library.Media.ImageManager.Interfaces;
using Pixsys.Library.Media.ImageManager.Models;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace Pixsys.Library.Media.ImageManager
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// The Image Manager extensions.
    /// </summary>
    public static class ImageManagerExtensions
    {
        /// <summary>
        /// Adds the Image manager.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The updated builder.</returns>
        public static WebApplicationBuilder AddImageManager(this WebApplicationBuilder builder)
        {
            _ = builder.Services.Configure<ImageManagerSettings>(builder.Configuration.GetSection(nameof(ImageManager)));
            builder.Services.TryAddTransient<IImageManager, ImageManager>();
            return builder;
        }
    }
}