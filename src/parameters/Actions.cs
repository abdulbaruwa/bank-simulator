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
    public class StepActionProfile
    {
        public string Action { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Count { get; set; }
        public double Sum { get; set; }
        public double Avg { get; set; }
        public double Std { get; set; }
        public int Step { get; set; }

        public StepActionProfile(int step, string action, int month, int day, int hour, int count, double sum, double avg, double std)
        {
            Action = action;
            Month = month;
            Day = day;
            Hour = hour;
            Count = count;
            Sum = sum;
            Avg = avg;
            Std = std;
            Step = step;
        }
    }
}
