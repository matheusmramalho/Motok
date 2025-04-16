namespace MatheusR.Motok.CC.Models;
public class ApiResponseModel
{
    public ApiResponseModel(string mensagem)
    {
        Mensagem = mensagem;
    }

    public string? Mensagem { get; set; }
}
