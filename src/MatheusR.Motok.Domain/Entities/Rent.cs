using MatheusR.Motok.Domain.Entities.Base;
using MatheusR.Motok.Domain.Enums;
using MatheusR.Motok.Domain.Exceptions;

namespace MatheusR.Motok.Domain.Entities;
public class Rent : Entity
{
    #region Main Props
    public DateTime InitialDate { get; private set; }
    public DateTime? FinalDate { get; private set; }
    public DateTime ExpectedFinalDate { get; private set; }
    public decimal ExpectedPrice { get; private set; }
    public decimal? FinalPrice { get; private set; }
    public bool? HasFine { get; private set; }
    public bool IsRentActive { get; set; }
    public RentPlanType RentPlan { get; private set; }
    #endregion

    #region Relations
    public Delivery Delivery { get; private set; }
    public Guid DeliveryId { get; private set; }
    public Motorcycle Motorcycle { get; private set; }
    public Guid MotorcycleId { get; private set; }
    #endregion

    protected Rent() { }

    public Rent(
    DateTime initialDate,
    DateTime expectedFinalDate,
    decimal expectedPrice,
    RentPlanType rentPlan,
    Delivery delivery,
    Motorcycle motorcycle,
    DateTime? finalDate)
    {
        InitialDate = initialDate;
        ExpectedFinalDate = expectedFinalDate;
        FinalDate = finalDate;
        ExpectedPrice = expectedPrice;
        RentPlan = rentPlan;
        Delivery = delivery;
        DeliveryId = delivery.Id;
        Motorcycle = motorcycle;
        MotorcycleId = motorcycle.Id;
        IsRentActive = true;
    }

    public void UpdateFinalDate(DateTime finalDate)
    {
        FinalDate = finalDate;
    }

    public void UpdateFinalPrice(decimal finalPrice)
    {
        FinalPrice = finalPrice;
    }

    public void CalculateExpectedPrice()
    {
        switch (RentPlan)
        {
            case RentPlanType.Plan7Days:
                ExpectedPrice = GetDailyRate(RentPlanType.Plan7Days) * 7;
                break;
            case RentPlanType.Plan15Days:
                ExpectedPrice = GetDailyRate(RentPlanType.Plan15Days) * 15;
                break;
            case RentPlanType.Plan30Days:
                ExpectedPrice = GetDailyRate(RentPlanType.Plan30Days) * 30;
                break;
            case RentPlanType.Plan45Days:
                ExpectedPrice = GetDailyRate(RentPlanType.Plan45Days) * 45;
                break;
            case RentPlanType.Plan50Days:
                ExpectedPrice = GetDailyRate(RentPlanType.Plan50Days) * 50;
                break;
            default:
                throw new DomainBusinessException("Plano não existente: Opções disponíveis de 1 a 5");
        }
    }

    public void FinalizeRent()
    {
        IsRentActive = false;
    }

    public (decimal TotalPrice, decimal DailyPrice) CalculateDailyPrice()
    {
        int daysUsed = 0;

        // Caso a FinalDate não tenha sido informada (ainda não devolvido)
        if (!FinalDate.HasValue)
        {
            daysUsed = (ExpectedFinalDate - InitialDate).Days;
            return (ExpectedPrice, ExpectedPrice / daysUsed);
        }

        // Validações quando FinalDate existe
        if (FinalDate.Value < InitialDate)
            throw new DomainBusinessException("Data final não pode ser anterior à data inicial.");

        daysUsed = (FinalDate.Value - InitialDate).Days;
        if (daysUsed <= 0)
            throw new DomainBusinessException("Período de locação inválido.");

        decimal totalPrice;
        decimal dailyPrice;

        // Caso 1: Devolução antecipada (com multa)
        if (FinalDate.Value < ExpectedFinalDate)
        {
            int unusedDays = (ExpectedFinalDate - FinalDate.Value).Days;
            decimal dailyRate = GetDailyRate(RentPlan);

            // Valor das diárias usadas
            decimal usedDaysValue = daysUsed * dailyRate;

            // Cálculo da multa
            decimal finePercentage = RentPlan switch
            {
                RentPlanType.Plan7Days => 0.2m,
                RentPlanType.Plan15Days => 0.4m,
                _ => 0m
            };

            decimal fineValue = unusedDays * dailyRate * finePercentage;

            totalPrice = usedDaysValue + fineValue;
            HasFine = true;
        }
        // Caso 2: Devolução tardia (cobrar adicional por dia)
        else if (FinalDate.Value > ExpectedFinalDate)
        {
            int extraDays = (FinalDate.Value - ExpectedFinalDate).Days;
            totalPrice = ExpectedPrice + (extraDays * 50m);
            HasFine = false;
        }
        // Caso 3: Devolução no dia exato
        else
        {
            totalPrice = ExpectedPrice;
            HasFine = false;
        }

        dailyPrice = totalPrice / daysUsed;

        return (totalPrice, dailyPrice);
    }

    public decimal GetDailyRate(RentPlanType rentPlanType)
    {
        return rentPlanType switch
        {
            RentPlanType.Plan7Days => 30m,
            RentPlanType.Plan15Days => 28m,
            RentPlanType.Plan30Days => 22m,
            RentPlanType.Plan45Days => 20m,
            RentPlanType.Plan50Days => 18m,
            _ => throw new DomainBusinessException("Plano não existente")
        };
    }
}
