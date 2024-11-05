// -----------------------------------------------------------------------
// <copyright file="ImageManagerOperations.cs" company="Pixsys">
// Copyright (c) Pixsys. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Pixsys.Library.Common.Extensions;
using Pixsys.Library.Media.Common.Enums;
using Pixsys.Library.Media.Common.Extensions;
using Pixsys.Library.Media.Common.Helpers;
using Pixsys.Library.Media.Common.Models;
using Pixsys.Library.Media.ImageManager.Constants;
using Pixsys.Library.Media.ImageManager.Interfaces;
using Pixsys.Library.Media.ImageManager.Models;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System.Text;

namespace Pixsys.Library.Media.ImageManager
{
    /// <summary>
    /// The Image manager from image.
    /// </summary>
    /// <seealso cref="IImageManagerOperations" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:Keywords should be spaced correctly", Justification = "Reviewed.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:Opening square brackets should be spaced correctly", Justification = "Reviewed.")]
    public class ImageManagerOperations : IImageManagerOperations
    {
        private readonly ImageManagerSettings settings;
        private readonly DirectoryInfo sourceFolder;
        private readonly string sourceFileName;
        private readonly List<Action<IImageProcessingContext>> actions;
        private readonly StringBuilder imagesuffix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageManagerOperations"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="sourceFileName">The source file name.</param>
        /// <param name="actions">The list of actions.</param>
        /// <param name="imagesuffix">The image manipulations suffix.</param>
        public ImageManagerOperations(ImageManagerSettings settings, DirectoryInfo sourceFolder, string sourceFileName, List<Action<IImageProcessingContext>> actions, StringBuilder imagesuffix)
        {
            ArgumentNullException.ThrowIfNull(sourceFolder);
            ArgumentNullException.ThrowIfNull(sourceFileName);
            this.settings = settings;
            this.sourceFolder = sourceFolder;
            this.sourceFileName = sourceFileName;
            this.actions = actions;
            this.imagesuffix = imagesuffix;
        }

        /// <inheritdoc />
        public IImageManagerOperations Resize(ResizeOptions options)
        {
            return new ImageManagerOperations(
                settings,
                sourceFolder,
                sourceFileName,
                new List<Action<IImageProcessingContext>>(actions)
                {
                    x => x.Resize(options),
                },
                imagesuffix.Copy().Append('_').Append(options.Size.Width).Append('x').Append(options.Size.Height));
        }

        /// <inheritdoc />
        public IImageManagerOperations BlackWhite()
        {
            return AddAction(x => x.BlackWhite(), ImageManagerOperationSuffixes.BlackWhite);
        }

        /// <inheritdoc />
        public IImageManagerOperations GaussianBlur()
        {
            return AddAction(x => x.GaussianBlur(), ImageManagerOperationSuffixes.GaussianBlur);
        }

        /// <inheritdoc />
        public IImageManagerOperations Kodachrome()
        {
            return AddAction(x => x.Kodachrome(), ImageManagerOperationSuffixes.Kodachrome);
        }

        /// <inheritdoc />
        public IImageManagerOperations AutoOrient()
        {
            return AddAction(x => x.AutoOrient(), ImageManagerOperationSuffixes.AutoOrient);
        }

        /// <inheritdoc />
        public IImageManagerOperations BokehBlur()
        {
            return AddAction(x => x.BokehBlur(), ImageManagerOperationSuffixes.BokehBlur);
        }

        /// <inheritdoc />
        public IImageManagerOperations BoxBlur()
        {
            return AddAction(x => x.BoxBlur(), ImageManagerOperationSuffixes.BoxBlur);
        }

        /// <inheritdoc />
        public IImageManagerOperations Brightness(float amount)
        {
            return AddAction(x => x.Brightness(amount), $"{ImageManagerOperationSuffixes.Brightness}{amount}");
        }

        /// <inheritdoc />
        public IImageManagerOperations Crop(int width, int height)
        {
            return AddAction(x => x.Crop(width, height), $"{ImageManagerOperationSuffixes.Crop}{width}_{height}");
        }

        /// <inheritdoc />
        public IImageManagerOperations Flip(FlipMode mode)
        {
            return AddAction(x => x.Flip(mode), ImageManagerOperationSuffixes.Flip);
        }

        /// <inheritdoc />
        public IImageManagerOperations Glow()
        {
            return AddAction(x => x.Glow(), ImageManagerOperationSuffixes.Glow);
        }

        /// <inheritdoc />
        public IImageManagerOperations Grayscale()
        {
            return AddAction(x => x.Grayscale(), ImageManagerOperationSuffixes.Grayscale);
        }

        /// <inheritdoc />
        public IImageManagerOperations Hue(float degrees)
        {
            return AddAction(x => x.Hue(degrees), $"{ImageManagerOperationSuffixes.Hue}{degrees}");
        }

        /// <inheritdoc />
        public IImageManagerOperations Invert()
        {
            return AddAction(x => x.Invert(), ImageManagerOperationSuffixes.Invert);
        }

        /// <inheritdoc />
        public IImageManagerOperations Lightness(float amount)
        {
            return AddAction(x => x.Lightness(amount), $"{ImageManagerOperationSuffixes.Lightness}{amount}");
        }

        /// <inheritdoc />
        public IImageManagerOperations OilPaint()
        {
            return AddAction(x => x.OilPaint(), ImageManagerOperationSuffixes.OilPaint);
        }

        /// <inheritdoc />
        public IImageManagerOperations Opacity(float amount)
        {
            return AddAction(x => x.Opacity(amount), $"{ImageManagerOperationSuffixes.Opacity}{amount}");
        }

        /// <inheritdoc />
        public IImageManagerOperations Pixelate()
        {
            return AddAction(x => x.Pixelate(), ImageManagerOperationSuffixes.Pixelate);
        }

        /// <inheritdoc />
        public IImageManagerOperations Polaroid()
        {
            return AddAction(x => x.Polaroid(), ImageManagerOperationSuffixes.Polaroid);
        }

        /// <inheritdoc />
        public IImageManagerOperations Saturate(float amount)
        {
            return AddAction(x => x.Saturate(amount), $"{ImageManagerOperationSuffixes.Saturate}{amount}");
        }

        /// <inheritdoc />
        public IImageManagerOperations Sepia()
        {
            return AddAction(x => x.Sepia(), ImageManagerOperationSuffixes.Sepia);
        }

        /// <inheritdoc />
        public IImageManagerOperations Vignette()
        {
            return AddAction(x => x.Vignette(), ImageManagerOperationSuffixes.Vignette);
        }

        /// <inheritdoc />
        public async Task<FileContentResult> SaveStreamAsync()
        {
            string sourceImagePath = Path.Combine(sourceFolder.FullName, sourceFileName);
            using SixLabors.ImageSharp.Image sourceImage = await SixLabors.ImageSharp.Image.LoadAsync(sourceImagePath);
            (SixLabors.ImageSharp.Image image, _) = ApplyTransformations(sourceImage, false);
            await using MemoryStream outStream = new();
            await image.SaveAsync(outStream, new WebpEncoder());
            return new FileContentResult(outStream.ToArray(), "image/webp");
        }

        /// <inheritdoc />
        public async Task<ImageProperties> SaveImageAsync(DirectoryInfo folder, string fileName, ImageFormat format, bool addActionsSuffixes = false, string profileName = "")
        {
            string sourceImagePath = Path.Combine(sourceFolder.FullName, sourceFileName);
            using SixLabors.ImageSharp.Image sourceImage = await SixLabors.ImageSharp.Image.LoadAsync(sourceImagePath);
            (SixLabors.ImageSharp.Image image, string? suffix) = ApplyTransformations(sourceImage, addActionsSuffixes);
            ImageProperties imageOutput = await image.SaveImageAsync(folder, fileName, suffix ?? string.Empty, format);

            // Remove original or keep the more lightweight image
            if (actions.Count == 0 && sourceImagePath != imageOutput.Location.FullPath && string.Equals(Path.GetExtension(imageOutput.Location.FullPath), format.GetExtension(), StringComparison.OrdinalIgnoreCase))
            {
                FileInfo original = new(sourceImagePath);
                FileInfo converted = new(imageOutput.Location.FullPath);
                if (original.Length < converted.Length)
                {
                    File.Delete(imageOutput.Location.FullPath);
                    File.Move(sourceImagePath, imageOutput.Location.FullPath);
                }
            }

            // Thumbnails Creation
            if (!string.IsNullOrWhiteSpace(profileName))
            {
                ImageProfile profile = GetProfile(profileName);
                imageOutput.Thumbnails = [];
                using SixLabors.ImageSharp.Image image2 = SixLabors.ImageSharp.Image.Load(imageOutput.Location.FullPath);
                foreach (SixLabors.ImageSharp.Size size in profile.Sizes)
                {
                    DirectoryInfo destFolder = Directory.CreateDirectory(Path.Combine(folder.FullName, profile.Name, size.Width + "x" + size.Height));
                    image2.Mutate(x => x.Resize(new ResizeOptions()
                    {
                        Mode = ResizeMode.Max,
                        Size = size,
                    }));

                    imageOutput.Thumbnails.Add(await image2.SaveImageAsync(destFolder, fileName, string.Empty, format));
                }

                if (!profile.KeepOriginalWhenResizing)
                {
                    await Helpers.ImageManagerHelper.DeleteAsync(sourceFolder, sourceFileName);
                }
            }

            return imageOutput;
        }

        /// <inheritdoc />
        public ImageProfile GetProfile(string profileName)
        {
            if (settings.Profiles is null)
            {
                throw new InvalidOperationException("No image profile has been set. Please update the ImageManager section in your appsettings.json");
            }

            ImageProfile? profile = settings.Profiles.Find(x => x.Name == profileName);
            return profile ?? throw new InvalidOperationException($"No image profile with the name {profileName} has been found. Please update the ImageManager section in your appsettings.json");
        }

        /// <inheritdoc />
        public async Task<ImageLocation> SimulateImageLocationAsync(DirectoryInfo folder, string fileName, ImageFormat format)
        {
            return await ImageHelper.GetImageLocation(folder, fileName, imagesuffix.ToString(), format);
        }

        /// <summary>
        /// Adds action to the actions list.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="suffix">The action suffix.</param>
        /// <returns>The <see cref="ImageManagerOperations"/> object.</returns>
        private ImageManagerOperations AddAction(Action<IImageProcessingContext> action, string suffix)
        {
            return new ImageManagerOperations(settings, sourceFolder, sourceFileName, new List<Action<IImageProcessingContext>>(actions) { action }, imagesuffix.Copy().Append(suffix));
        }

        /// <summary>
        /// Applies image transformations.
        /// </summary>
        /// <param name="image">The source image.</param>
        /// <param name="addActionsSuffixes">A value indicating wether the action suffixes must be added to the file name or not.</param>
        /// <returns>The <see cref="SixLabors.ImageSharp.Image "/>.</returns>
        private (SixLabors.ImageSharp.Image Image, string? Suffix) ApplyTransformations(SixLabors.ImageSharp.Image image, bool addActionsSuffixes = false)
        {
            void CombinedAction(IImageProcessingContext context)
            {
                foreach (Action<IImageProcessingContext> action in actions)
                {
                    action(context);
                }
            }

            if (actions.Count != 0)
            {
                image.Mutate(CombinedAction);
            }

            string? suffix = null;
            if (addActionsSuffixes)
            {
                suffix = imagesuffix.ToString();
            }

            return (image, suffix);
        }
    }
}