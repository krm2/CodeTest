using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using DataServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataServices.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ImageService> _logger;

        private const string JpegFolder = "\\JpegFile\\";
        private const string OriginalFolder = "\\OriginalImage\\";
        private const string ImageOutPathKey = "ImageOutPath";

        public ImageService(IConfiguration configuration, ILogger<ImageService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SaveOriginalImage(byte[] image, string fileName)
        {
            var imageOutPath = _configuration.GetSection(ImageOutPathKey).Value;

            var path = Path.Combine(imageOutPath + OriginalFolder, fileName);

            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await fileStream.WriteAsync(image);
            }

            _logger.LogInformation($"Image saved: {path}");
        }

        public void SaveAsJpeg(byte[] image, string fileName)
        {
            var imageOutPath = _configuration.GetSection(ImageOutPathKey).Value;

            var fileNameNoEx = Path.GetFileNameWithoutExtension(fileName);

            var path = Path.Combine(imageOutPath + OriginalFolder, fileName);

            using (var imageWorker = Image.FromFile(path))
            {
                imageWorker.Save(Path.Combine(imageOutPath + JpegFolder + $"{fileNameNoEx}.jpg"), ImageFormat.Jpeg);
            }
            _logger.LogInformation($"Image saved: {path}");
        }

        public byte[] LoadJpeg(string imageFileName)
        {
            var imageOutPath = _configuration.GetSection(ImageOutPathKey).Value;

            var fileNameNoEx = Path.GetFileNameWithoutExtension(imageFileName);

            var path = Path.Combine(imageOutPath + JpegFolder, $"{fileNameNoEx}.jpg");

            _logger.LogInformation($"Image Read: {path}");

            return File.ReadAllBytes(path);
        }
    }
}
