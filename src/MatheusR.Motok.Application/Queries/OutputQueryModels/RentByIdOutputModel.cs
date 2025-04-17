using MatheusR.Motok.Domain.Entities;

namespace MatheusR.Motok.Application.Queries.OutputQueryModels;
public class RentByIdOutputModel
{
    public RentByIdOutputModel(Guid identificador,
        decimal valorDiaria,
        Guid entregadorId,
        Guid motoId,
        DateTime dataInicio,
        DateTime? dataTermino,
        DateTime dataPrevisaoTermino,
        bool aluguelAtivo,
        decimal valorTotal)
    {
        Identificador = identificador;
        ValorDiaria = valorDiaria;
        EntregadorId = entregadorId;
        MotoId = motoId;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
        DataPrevisaoTermino = dataPrevisaoTermino;
        AluguelAtivo = aluguelAtivo;
        ValorTotal = valorTotal;
    }

    public Guid Identificador { get; set; }
    public decimal ValorDiaria { get; set; }
    public decimal ValorTotal { get; set; }
    public Guid EntregadorId { get; set; }
    public Guid MotoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public DateTime DataPrevisaoTermino { get; set; }
    public bool AluguelAtivo { get; set; }

    public static RentByIdOutputModel FromEntity(Rent rent, decimal valorDiaria, decimal valorTotal)
    {
        return new RentByIdOutputModel(rent.Id, valorDiaria, rent.DeliveryId, rent.MotorcycleId, rent.InitialDate, rent.FinalDate, rent.ExpectedFinalDate, rent.IsRentActive, valorTotal);
    }
}
