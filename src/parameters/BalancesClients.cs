namespace BankSimulator.parameters;

using BankSimulator.utils;
using System;
using System.Collections.Generic;
using System.Linq;

public static class BalancesClients
{
    private const int ColumnLow = 0;
    private const int ColumnHigh = 1;

    private static RandomCollection<List<double>> balanceRangePicker;
    private static readonly SortedDictionary<double, double> overdraftLimits = new SortedDictionary<double, double>();

    public static void InitBalanceClients(string filename)
    {
        balanceRangePicker = new RandomCollection<List<double>>();
        var parameters = CSVReader<BalanceClient>.Read(filename);

        foreach (var paramLine in parameters)
        {
            var balanceRange = new List<double>
                {
                    paramLine.RangeStart,
                    paramLine.RangeEnd
                };

            balanceRangePicker.Add(paramLine.Percentage, balanceRange);
        }
    }

    public static void InitOverdraftLimits(string filename)
    {
        var parameters = CSVReader<OverdraftLimit>.Read(filename);

        double lastValueHigh = double.NegativeInfinity;

        foreach (var paramLine in parameters)
        {
            var valueLow = paramLine.LowerBound;
            var valueHigh  = paramLine.HigherBound;
            if (valueLow > valueHigh)
            {
                throw new ArgumentException($"A range should be strictly increasing: {valueLow} > {valueHigh}");
            }
            if (valueLow != lastValueHigh)
            {
                throw new ArgumentException("Ranges should be a partition of R and provided in increasing lower bound order.");
            }

            overdraftLimits.Add(valueLow, paramLine.Limit);
            lastValueHigh = valueHigh;
        }
        if (lastValueHigh != double.PositiveInfinity)
        {
            throw new ArgumentException("The last range should not have an upper bound.");
        }
    }

    public static double PickNextBalance(Random random)
    {
        var balanceRange = balanceRangePicker.Next();
        double rangeSize = balanceRange[ColumnHigh] - balanceRange[ColumnLow];

        return balanceRange[ColumnLow] + random.NextDouble() * rangeSize;
    }

    public static double GetOverdraftLimit(double meanTransaction)
    {
        var entry = overdraftLimits.FirstOrDefault(x => x.Key <= meanTransaction);
        return entry.Value;
    }
}