using System.Threading.Tasks;

namespace DataServices.Interfaces
{
    public interface IImageService
    {
        public Task SaveOriginalImage(byte[] image, string fileName);
        public void SaveAsJpeg(byte[] image, string fileName);
        public byte[] LoadJpeg(string imageFileName);
    }
}
