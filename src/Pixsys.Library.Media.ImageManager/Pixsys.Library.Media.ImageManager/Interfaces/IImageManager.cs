// -----------------------------------------------------------------------
// <copyright file="IImageManager.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Pixsys.Library.Media.ImageManager.Models;

namespace Pixsys.Library.Media.ImageManager.Interfaces
{
    /// <summary>
    /// Interface for Image Manager.
    /// </summary>
    public interface IImageManager
    {
        /// <summary>
        /// Deletes one or more images asynchronously.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="profiles">The profiles.</param>
        /// <remarks>
        /// If the profiles parameter is null, it will only delete the original image.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(DirectoryInfo folder, string fileName, List<ImageProfile>? profiles = null);

        /// <summary>
        /// Deletes one or more images asynchronously.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <param name="profiles">The profiles.</param>
        /// <remarks>
        /// If the profiles parameter is null, it will only delete the original image.
        /// </remarks>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteAsync(string imagePath, List<ImageProfile>? profiles = null);

        /// <summary>
        /// Sets the image parameters.
        /// </summary>
        /// <param name="folder">The image folder.</param>
        /// <param name="fileName">The image name with its extension.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations ForImage(DirectoryInfo folder, string fileName);

        /// <summary>
        /// Sets the image parameters.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations ForImage(string imagePath);

        /// <summary>
        /// Gets the <see cref="IFormFile"/> and saves the image in the temporary folder.
        /// </summary>
        /// <param name="file">The file sent with the HttpRequest.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        Task<IImageManagerOperations> FromFormFile(IFormFile file);
    }
}