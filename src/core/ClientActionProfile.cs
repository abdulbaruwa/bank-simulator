namespace BankSimulator.core;
public class ClientActionProfile
{
    public string Action { get; }
    public int MinCount { get; }
    public int MaxCount { get; }
    public double AverageAmount { get; set; }
    public double StdAmount { get; set; }
    public int Freq { get; set; }
    public ClientActionProfile(string action, int minCount, int maxCount, double avgAmount, double stdAmount)
    {
        Action = action;
        MinCount = minCount;
        MaxCount = maxCount;
        AverageAmount = avgAmount;
        StdAmount = stdAmount;
    }
}