// -----------------------------------------------------------------------
// <copyright file="ImageManagerHelper.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Media.ImageManager.Models;

namespace Pixsys.Library.Media.ImageManager.Helpers
{
    /// <summary>
    /// The image manager helper.
    /// </summary>
    internal static class ImageManagerHelper
    {
        /// <summary>
        /// Deletes one or more images asynchronously.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="profiles">The profiles.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task DeleteAsync(DirectoryInfo folder, string fileName, List<ImageProfile>? profiles = null)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                if (profiles != null)
                {
                    foreach (ImageProfile profile in profiles)
                    {
                        foreach (SixLabors.ImageSharp.Size size in profile.Sizes)
                        {
                            string dimensionSourceFolder = folder.FullName + size.Width + "x" + size.Height;
                            string deletePath = Path.Combine(dimensionSourceFolder, fileName);
                            if (File.Exists(deletePath))
                            {
                                await Task.Run(() => File.Delete(deletePath));
                            }
                        }
                    }
                }

                string fileDeletePath = Path.Combine(folder.FullName, fileName);
                if (File.Exists(fileDeletePath))
                {
                    await Task.Run(() => File.Delete(fileDeletePath));
                }
            }
        }
    }
}