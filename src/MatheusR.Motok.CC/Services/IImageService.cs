namespace MatheusR.Motok.CC.Services;
public interface IImageService
{
    void DeleteImage(string fileName);
    Task<string> SaveImageAsync(string base64Image);
}
