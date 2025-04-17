namespace MatheusR.Motok.Domain.Exceptions;
public class DomainBusinessException: Exception
{
    public DomainBusinessException(string? message) : base(message)
    { }
}
