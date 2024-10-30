// -----------------------------------------------------------------------
// <copyright file="IImageManagerOperations.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Pixsys.Library.Media.Common.Enums;
using Pixsys.Library.Media.Common.Models;
using Pixsys.Library.Media.ImageManager.Models;
using SixLabors.ImageSharp.Processing;

namespace Pixsys.Library.Media.ImageManager.Interfaces
{
    /// <summary>
    /// Interface for ImageManagerForImage.
    /// </summary>
    public interface IImageManagerOperations
    {
        /// <summary>
        /// Resizes an image in accordance to the given <see cref="ResizeOptions"/>.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Resize(ResizeOptions options);

        /// <summary>
        /// Applies black and white toning to the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations BlackWhite();

        /// <summary>
        /// Applies a Gaussian blur to the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations GaussianBlur();

        /// <summary>
        /// Alters the colors of the image recreating an old Kodachrome camera effect.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Kodachrome();

        /// <summary>
        /// Adjusts an image so that its orientation is suitable for viewing. Adjustments are based on EXIF metadata embedded in the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations AutoOrient();

        /// <summary>
        /// Applies a bokeh blur to the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations BokehBlur();

        /// <summary>
        /// Applies a box blur to the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations BoxBlur();

        /// <summary>
        /// Alters the brightness component of the image.
        /// </summary>
        /// <param name="amount">The amount of brightness.</param>
        /// <remarks>
        /// A value of 0 will create an image that is completely black. A value of 1 leaves the input unchanged.
        /// Other values are linear multipliers on the effect. Values of an amount over 1 are allowed, providing brighter results.
        /// </remarks>
        /// /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Brightness(float amount);

        /// <summary>
        /// Crops an image to the given width and height.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Crop(int width, int height);

        /// <summary>
        /// Flips an image by the given instructions.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Flip(FlipMode mode);

        /// <summary>
        /// Applies a radial glow effect to an image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Glow();

        /// <summary>
        /// Applies <see cref="GrayscaleMode.Bt709"/> grayscale toning to the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Grayscale();

        /// <summary>
        /// Alters the hue component of the image.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Hue(float degrees);

        /// <summary>
        /// Inverts the colors of the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Invert();

        /// <summary>
        /// Alters the lightness component of the image.
        /// </summary>
        /// <param name="amount">The amount of lightness.</param>
        /// <remarks>
        /// A value of 0 will create an image that is completely black. A value of 1 leaves the input unchanged.
        /// Other values are linear multipliers on the effect. Values of an amount over 1 are allowed, providing lighter results.
        /// </remarks>
        /// /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Lightness(float amount);

        /// <summary>
        /// Alters the colors of the image recreating an oil painting effect with levels and brushSize
        /// set to <value>10</value> and <value>15</value> respectively.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations OilPaint();

        /// <summary>
        /// Multiplies the alpha component of the image.
        /// </summary>
        /// <param name="amount">The amount of opacity.</param>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Opacity(float amount);

        /// <summary>
        /// Pixelates an image with the given pixel size.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Pixelate();

        /// <summary>
        /// Alters the colors of the image recreating an old Polaroid camera effect.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Polaroid();

        /// <summary>
        /// Alters the saturation component of the image.
        /// </summary>
        /// <param name="amount">The amount of saturation.</param>
        /// <remarks>
        /// A value of 0 is completely un-saturated. A value of 1 leaves the input unchanged.
        /// Other values are linear multipliers on the effect. Values of amount over 1 are allowed, providing super-saturated results.
        /// </remarks>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Saturate(float amount);

        /// <summary>
        /// Applies sepia toning to the image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Sepia();

        /// <summary>
        /// Applies a radial vignette effect to an image.
        /// </summary>
        /// <returns>The <see cref="IImageManagerOperations"/> object.</returns>
        IImageManagerOperations Vignette();

        /// <summary>
        /// Saves the image asynchronously.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <param name="addActionsSuffixes">Sets if action suffixes must be added to the file name (ex: <c>_sep_vig_200x200</c>).</param>
        /// <param name="profileName">The profile name.</param>
        /// <returns>The <see cref="ImageProperties"/>.</returns>
        Task<ImageProperties> SaveAsync(DirectoryInfo folder, string fileName, ImageFormat format, bool addActionsSuffixes = false, string profileName = "");

        /// <summary>
        /// Simulates what the image location will be when the image will be saved.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="format">The format.</param>
        /// <returns>The <see cref="ImageLocation"/>.</returns>
        Task<ImageLocation> SimulateImageLocationAsync(DirectoryInfo folder, string fileName, ImageFormat format);

        /// <summary>
        /// Gets the profile based on the profile name.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        /// <returns>The <see cref="ImageProfile"/>.</returns>
        ImageProfile GetProfile(string profileName);
    }
}