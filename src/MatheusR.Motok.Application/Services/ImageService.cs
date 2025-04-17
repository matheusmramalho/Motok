using MatheusR.Motok.Application.Exceptions;
using MatheusR.Motok.CC.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MatheusR.Motok.Application.Services;
public class ImageService : IImageService
{
    private readonly IHostEnvironment _environment;
    private readonly ILogger<ImageService> _logger;

    public ImageService(IHostEnvironment environment, ILogger<ImageService> logger)
    {
        _environment = environment;
        _logger = logger;
    }

    public void DeleteImage(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentNullException(nameof(fileName), "O nome do arquivo não pode ser vazio.");
        }

        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads", "Images");
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!File.Exists(filePath)) return;

        try
        {
            File.Delete(filePath);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Erro ao deletar arquivo {fileName}: {ex.Message}");
        }
    }

    public async Task<string> SaveImageAsync(string base64Image)
    {
        // Extrai o tipo de conteúdo e os bytes da imagem
        var imageParts = base64Image.Split(',').ToList();
        if (imageParts.Count < 2)
        {
            throw new ArgumentException("Formato base64 inválido.");
        }

        var contentType = imageParts[0].Split(':')[1].Split(';')[0];
        var extension = GetFileExtension(contentType);
        var imageBytes = Convert.FromBase64String(imageParts[1]);

        // Define o caminho de salvamento
        var uploadsFolder = Path.Combine(_environment.ContentRootPath, "Uploads", "Images");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        // Gera um nome único para o arquivo
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        // Salva o arquivo
        await File.WriteAllBytesAsync(filePath, imageBytes);

        // Retorna o caminho relativo ou completo conforme sua necessidade
        return fileName;
    }

    private string GetFileExtension(string contentType)
    {
        return contentType switch
        {
            "image/png" => ".png",
            "image/bmp" => ".bmp",
            _ => throw new MotokApplicationException($"Tipo de imagem não suportada: {contentType}")
        };
    }
}
