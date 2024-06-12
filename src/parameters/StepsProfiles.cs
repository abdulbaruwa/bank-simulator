
namespace BankSimulator.parameters;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class StepsProfiles
{
    private const int COLUMN_ACTION = 0, COLUMN_MONTH = 1, COLUMN_DAY = 2, COLUMN_HOUR = 3, COLUMN_COUNT = 4,
                      COLUMN_SUM = 5, COLUMN_AVERAGE = 6, COLUMN_STD = 7, COLUMN_STEP = 8;

    private List<Dictionary<string, StepActionProfile>> profilePerStep;
    private List<Dictionary<string, double>> probabilitiesPerStep = new List<Dictionary<string, double>>();
    private List<int> stepTargetCount;
    private int totalTargetCount;

    public StepsProfiles(string filename, double multiplier, int nbSteps)
    {
        var parameters = CSVReader<StepActionProfile>.Read(filename);

        profilePerStep = new List<Dictionary<string, StepActionProfile>>(nbSteps);
        for (int i = 0; i < nbSteps; i++)
        {
            profilePerStep.Add(new Dictionary<string, StepActionProfile>());
        }

        stepTargetCount = Enumerable.Repeat(0, nbSteps).ToList();

        foreach (var line in parameters)
        {
            if (ActionTypes.IsValidAction(line.Action))
            {
                int step = line.Step;
                int count = line.Count;

                if (step < nbSteps)
                {
                    var actionProfile = new StepActionProfile(step,
                        line.Action,
                        line.Month,
                        line.Day,
                        line.Hour,
                        count,
                        line.Sum,
                        line.Avg,
                        line.Std);

                    profilePerStep[step][line.Action] = actionProfile;
                    stepTargetCount[step] += count;
                }
            }
        }

        ComputeProbabilitiesPerStep();
        ModifyWithMultiplier(multiplier);
    }

    private void ModifyWithMultiplier(double multiplier)
    {
        for (int step = 0; step < stepTargetCount.Count; step++)
        {
            int newMaxCount = Convert.ToInt32(Math.Round(stepTargetCount[step] * multiplier));
            stepTargetCount[step] = newMaxCount;
        }
        totalTargetCount = stepTargetCount.Sum();
    }

    private void ComputeProbabilitiesPerStep()
    {
        for (int i = 0; i < profilePerStep.Count; i++)
        {
            var stepProfile = profilePerStep[i];
            int stepCount = stepTargetCount[i];

            var stepProbabilities = stepProfile.ToDictionary(
                kvp => kvp.Key,
                kvp => (double)kvp.Value.Count / stepCount
            );
            probabilitiesPerStep.Add(stepProbabilities);
        }
    }

    public int GetTargetCount(int step)
    {
        return stepTargetCount[step];
    }

    public Dictionary<string, double> GetProbabilitiesPerStep(int step)
    {
        return probabilitiesPerStep[step];
    }

    public int GetTotalTargetCount()
    {
        return totalTargetCount;
    }

    public StepActionProfile GetActionForStep(int step, string action)
    {
        return profilePerStep[step].GetValueOrDefault(action);
    }

    public Dictionary<string, List<double>> ComputeSeries(Func<StepActionProfile, double> getter)
    {
        var series = new Dictionary<string, List<double>>();
        foreach (var action in ActionTypes.GetActions())
        {
            series.Add(action, new List<double>());
        }

        foreach (var profileStep in profilePerStep)
        {
            foreach (var action in ActionTypes.GetActions())
            {
                if (profileStep.ContainsKey(action))
                {
                    series[action].Add(getter(profileStep[action]));
                }
                else
                {
                    series[action].Add(0d);
                }
            }
        }
        return series;
    }
}