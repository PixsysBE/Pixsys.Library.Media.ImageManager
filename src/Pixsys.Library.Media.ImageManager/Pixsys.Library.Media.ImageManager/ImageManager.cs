// -----------------------------------------------------------------------
// <copyright file="ImageManager.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pixsys.Library.Common.Utilities;
using Pixsys.Library.Media.ImageManager.Interfaces;
using Pixsys.Library.Media.ImageManager.Models;
using System.Text;

namespace Pixsys.Library.Media.ImageManager
{
    /// <summary>
    /// The Image manager.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <seealso cref="IImageManager" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:Keywords should be spaced correctly", Justification = "Reviewed.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
    public class ImageManager(IOptions<ImageManagerSettings> settings) : IImageManager
    {
        private readonly ImageManagerSettings settings = settings.Value;

        /// <inheritdoc />
        public IImageManagerOperations ForImage(DirectoryInfo folder, string fileName)
        {
            return new ImageManagerOperations(settings, folder, fileName, [], new StringBuilder());
        }

        /// <inheritdoc />
        public IImageManagerOperations ForImage(string imagePath)
        {
            string? folderName = Path.GetDirectoryName(imagePath);
            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentNullException(nameof(imagePath));
            }

            DirectoryInfo folder = new(folderName);
            string fileName = Path.GetFileName(imagePath);
            return ForImage(folder, fileName);
        }

        /// <inheritdoc />
        public async Task<IImageManagerOperations> FromFormFile(IFormFile file)
        {
            if (settings.TemporaryFolder is null)
            {
                throw new InvalidOperationException("No temporary folder has been set. Please update the ImageManager section in your appsettings.json");
            }

            if (file is null || file.Length <= 0)
            {
                throw new ArgumentNullException(nameof(file));
            }

            string sourceFileName = FileUtilities.GetUniqueFileName(file.FileName);
            string temporaryImagePath = Path.Combine(settings.TemporaryFolder, sourceFileName);

            // Copy original image to temporary Folder
            await using (FileStream stream = new(temporaryImagePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new ImageManagerOperations(settings, new DirectoryInfo(settings.TemporaryFolder), sourceFileName, [], new StringBuilder());
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string imagePath, List<ImageProfile>? profiles = null)
        {
            string? folderName = Path.GetDirectoryName(imagePath);
            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentNullException(nameof(imagePath));
            }

            DirectoryInfo folder = new(folderName);
            string fileName = Path.GetFileName(imagePath);
            await DeleteAsync(folder, fileName, profiles);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(DirectoryInfo folder, string fileName, List<ImageProfile>? profiles = null)
        {
            await Helpers.ImageManagerHelper.DeleteAsync(folder, fileName, profiles);
        }
    }
}