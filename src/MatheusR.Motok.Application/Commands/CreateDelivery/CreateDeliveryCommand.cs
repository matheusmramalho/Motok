using MatheusR.Motok.Domain.Enums;
using MediatR;

namespace MatheusR.Motok.Application.Commands.CreateDelivery;
public class CreateDeliveryCommand : IRequest<Unit>
{
    public CreateDeliveryCommand(string nome, string cnpj, DateTime dataNascimento, string numeroCnh, LicenteType tipoCnh, string? imagemCnh)
    {
        Nome = nome;
        Cnpj = cnpj;
        DataNascimento = dataNascimento;
        NumeroCnh = numeroCnh;
        TipoCnh = tipoCnh;
        ImagemCnh = imagemCnh;
    }

    public string Nome { get; set; }
    public string Cnpj { get; set; }
    public DateTime DataNascimento { get; set; }
    public string NumeroCnh { get; set; }
    public LicenteType TipoCnh { get; set; }
    public string? ImagemCnh { get; set; }
}
