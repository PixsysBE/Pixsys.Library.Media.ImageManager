# Image Manager

This manager will save and transform your images as a Fluent API, using SixLabors.ImageSharp library.

## 1. Installation

### 1.1 Register your settings in `appsettings.json`

Create a new section called `ImageManager` and add the following properties:

| Name | Optional? | Description
|----------|----------|------------|
| TemporaryFolder | Yes | Sets where the temporary image folder is located. Useful when you're working with uploaded images.
| Profiles | Yes | Sets all the profiles. Each profile contains a name, a array of sizes required for your needs, and a true/false property telling if you want to keep the original image when resizing.

```json
  "ImageManager": {
    "TemporaryFolder": "<TEMP_FOLDER>",
    "Profiles": [
      {
        "Name": "<PROFILE_NAME>",
        "Sizes": [
          {
            "Width": <WIDTH>,
            "Height": <HEIGHT>
          },
          {
            "Width": <WIDTH>,
            "Height": <HEIGHT>
          }
        ],
        "KeepOriginalWhenResizing": <KEEP_ORIGINAL_WHEN_RESIZING>
      },
      {
        "Name": "<PROFILE_NAME>",
        "Sizes": [
          {
            "Width": <WIDTH>,
            "Height": <HEIGHT>
          },
          {
            "Width": <WIDTH>,
            "Height": <HEIGHT>
          }
        ],
        "KeepOriginalWhenResizing": <KEEP_ORIGINAL_WHEN_RESIZING>
      }
    ]
  }
```

If profiles are set, the following image folder structure will be created:

```
├── <IMAGE_FOLDER>
│   ├── <PROFILE_1>
│   │   ├── <WIDTH_1xHEIGHT1> //ex: folder named '200x200'
│   │   │   ├── IMG
│   │   ├── <WIDTH_2xHEIGHT2>
│   │   │   ├── IMG
```

### 1.2 Register the service in `Program.cs`

```csharp

using Pixsys.Library.Media.ImageManager;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

_ = builder.AddImageManager();

```

### 2 Usage

#### 2.1 Inject the service into your controller

```csharp
private readonly IImageManager _imageManager;

public MyController(IImageManager imageManager)
{
    _imageManager = imageManager;;
}
```

#### 2.2 Methods

First, you set if the image manager must process a server-stored image (`ForImage`) or an uploaded one (`FromFormFile`).

Then you can apply multiple manipulations (such as `resize`,`crop`,`Sepia`,...) and/or save the output.

If you specify a profile name in the `SaveImageAsync` method, it will create resized images in the 'profile name' folder.

#### 2.2.1 Example

```csharp
await _imageManager
    .ForImage(<FILE_PATH>)
    .BlackWhite()
    .AutoOrient()
    .Resize(new SixLabors.ImageSharp.Processing.ResizeOptions
    {
        Mode = SixLabors.ImageSharp.Processing.ResizeMode.Max,
        Size = new Size(300, 400)
    })
    .Flip(SixLabors.ImageSharp.Processing.FlipMode.Horizontal)
    .Grayscale()
    .Polaroid()
    .Vignette()
    .Sepia()
    .Pixelate()
    .OilPaint()
    .SaveImageAsync(new DirectoryInfo(webRootPath), "output", Pixsys.Library.Media.Common.Enums.ImageFormat.Webp, false, "test");
```