using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator.core;

using BankSimulator.parameters;
using System;
using System.Collections.Generic;
using System.Linq;

public class ClientProfile
{
    private Dictionary<string, ClientActionProfile> profile;
    private Dictionary<string, double> actionProbability = new Dictionary<string, double>();
    private readonly Dictionary<string, int> targetCount = new Dictionary<string, int>();
    private int clientTargetCount;

    public ClientProfile(Dictionary<string, ClientActionProfile> profile, Random random)
    {
        this.profile = profile;
        this.clientTargetCount = 0;
        foreach (var action in ActionTypes.GetActions())
        {
            int targetCountAction = PickTargetCount(action, random);
            targetCount[action] = targetCountAction;
            clientTargetCount += targetCountAction;
        }
        ComputeActionProbability();
    }

    private int PickTargetCount(string action, Random random)
    {
        var actionProfile = profile[action];
        int targetCountAction;

        int rangeSize = actionProfile.MaxCount - actionProfile.MinCount;

        if (rangeSize == 0)
        {
            targetCountAction = actionProfile.MinCount;
        }
        else
        {
            targetCountAction = actionProfile.MinCount + random.Next(rangeSize);
        }

        //TODO: check if this is really mandatory
        int maxCountAction = ActionTypes.GetMaxOccurrenceGivenAction(actionProfile.Action);
        if (targetCountAction > maxCountAction)
        {
            targetCountAction = maxCountAction;
        }

        return targetCountAction;
    }

    private void ComputeActionProbability()
    {
        actionProbability = targetCount
            .ToDictionary(
                entry => entry.Key,
                entry => (double)entry.Value / clientTargetCount
            );
    }

    public Dictionary<string, double> GetActionProbability()
    {
        return actionProbability;
    }

    public int GetClientTargetCount()
    {
        return clientTargetCount;
    }

    public ClientActionProfile GetProfilePerAction(string action)
    {
        return profile[action];
    }
}