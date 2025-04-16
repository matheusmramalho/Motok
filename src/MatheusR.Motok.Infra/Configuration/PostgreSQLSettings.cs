namespace MatheusR.Motok.Infra.Configuration;
public class PostgreSQLSettings
{
    public string ConnectionString { get; set; }
    public int MaxRetryCount { get; set; } = 5;
    public int CommandTimeout { get; set; } = 30;
}
