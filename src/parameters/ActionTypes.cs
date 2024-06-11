using Microsoft.Extensions.Logging;
using System.Globalization;

using CsvHelper;

namespace BankSimulator.parameters;

public static class ActionTypes
{
    private static readonly ILogger _logger;
    private static HashSet<string> actions = new HashSet<string>();
    private static Dictionary<string, int> maxOccurrencesPerAction = new Dictionary<string, int>();
    
    static ActionTypes()
    {
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        _logger = loggerFactory.CreateLogger("ActionType");
    }
    public static void LoadActionTypes(string filename)
    {
        var parameters = CSVReader<Actions>.Read(filename);

        foreach (var paramLine in parameters)
        {
            var action = paramLine.Action;
            actions.Add(action);
        }
    }

    public static void LoadMaxOccurrencesPerClient(string filename)
    {
        var parameters = CSVReader<Actions>.Read(filename);
        int loaded = 0;
        foreach (var paramLine in parameters)
        {
            if (IsValidAction(paramLine.Action))
            {
                maxOccurrencesPerAction[paramLine.Action] = paramLine.MaxCount;
                loaded++;
            }
        }
        if (loaded != actions.Count)
        {
            _logger.LogWarning($"Missing action in {filename}");
        }
    }

    public static int GetMaxOccurrenceGivenAction(string action)
    {
        return maxOccurrencesPerAction[action];
    }

    public static bool IsValidAction(string name)
    {
        return actions.Contains(name);
    }

    public static HashSet<string> GetActions()
    {
        return actions;
    }
}

internal class CSVReader<T>
{
    internal static IEnumerable<T> Read(string filename)
    {
        using (var reader = new StreamReader(filename))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            return csv.GetRecords<T>();
        }
    }
}