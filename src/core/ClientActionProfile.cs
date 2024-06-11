using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator.core;
public class ClientActionProfile
{
    public string Action { get; }
    public int MinCount { get; }
    public int MaxCount { get; }
    public double AvgAmount { get; }
    public double StdAmount { get; }

    public ClientActionProfile(string action, int minCount, int maxCount, double avgAmount, double stdAmount)
    {
        Action = action;
        MinCount = minCount;
        MaxCount = maxCount;
        AvgAmount = avgAmount;
        StdAmount = stdAmount;
    }
}