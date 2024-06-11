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
}
