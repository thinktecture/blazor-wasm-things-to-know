using System.Threading.Tasks;

namespace BlazorLibs.Webcam
{
    public interface IWebcamService
    {
        Task StartVideoAsync(WebcamOptions options);
        Task TakePictureAsync();
    }
}
