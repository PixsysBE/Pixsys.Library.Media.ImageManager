// -----------------------------------------------------------------------
// <copyright file="ImageManagerSettings.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Pixsys.Library.Media.ImageManager.Models
{
    /// <summary>
    /// The Image Manager settings.
    /// </summary>
    public class ImageManagerSettings
    {
        /// <summary>
        /// Gets or sets the temporary folder.
        /// </summary>
        /// <value>
        /// The temporary folder.
        /// </value>
        public string? TemporaryFolder { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>
        /// The profiles.
        /// </value>
        public List<ImageProfile>? Profiles { get; set; }
    }
}