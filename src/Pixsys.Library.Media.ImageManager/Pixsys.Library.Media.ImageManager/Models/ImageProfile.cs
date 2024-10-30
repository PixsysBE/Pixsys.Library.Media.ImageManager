// -----------------------------------------------------------------------
// <copyright file="ImageProfile.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using SixLabors.ImageSharp;
using System.Diagnostics.CodeAnalysis;

namespace Pixsys.Library.Media.ImageManager.Models
{
    /// <summary>
    /// The Image profile model.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:DeclarationKeywordsMustFollowOrder", Justification = "Reviewed.")]
    public class ImageProfile
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the sizes.
        /// </summary>
        /// <value>
        /// The sizes.
        /// </value>
        public required List<Size> Sizes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the original must be kept when resizing.
        /// </summary>
        /// <value>
        ///   <c>true</c> or <c>false</c>.
        /// </value>
        public required bool KeepOriginalWhenResizing { get; set; }
    }
}
