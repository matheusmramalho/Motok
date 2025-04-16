using MatheusR.Motok.Domain.Entities.Base;
using MatheusR.Motok.Domain.Enums;

namespace MatheusR.Motok.Domain.Entities;
public class Rent : Entity
{
    #region MainProps
    public DateTime InitialDate { get; private set; }
    public DateTime? FinalDate { get; private set; }
    public decimal ExpectedPrice { get; private set; }
    public decimal? FinalPrice { get; private set; }
    public bool? HasFine { get; private set; }
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
    DateTime finalDate,
    decimal expectedPrice,
    RentPlanType rentPlan,
    Delivery delivery,
    Motorcycle motorcycle)
    {
        InitialDate = initialDate;
        FinalDate = finalDate;
        ExpectedPrice = expectedPrice;
        RentPlan = rentPlan;
        Delivery = delivery;
        DeliveryId = delivery.Id;
        Motorcycle = motorcycle;
        MotorcycleId = motorcycle.Id;
    }
}
