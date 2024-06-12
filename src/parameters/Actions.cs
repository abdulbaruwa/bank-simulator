namespace BankSimulator.parameters
{
    public class Actions
    {
        public string Action { get; set; } = ""!;
        public int MaxCount { get; set; }
        public int MinCount { get; set; }
        public decimal AverageAmount { get; set; }
        public decimal StdAmount { get; set; }
        public double Frequency { get; set; }
    }
    public class BalanceClient
    {
        public int RangeStart { get; set; }
        public int RangeEnd { get; set; }
        public double Percentage { get; set; }

        public BalanceClient(int rangeStart, int rangeEnd, double percentage)
        {
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
            Percentage = percentage;
        }
    }

    public class OverdraftLimit
    {
        public double LowerBound { get; set; }
        public double HigherBound { get; set; }
        public double Limit { get; set; }
    }
}
