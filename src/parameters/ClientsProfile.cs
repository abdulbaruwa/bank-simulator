namespace BankSimulator.parameters;

using System;
using System.Collections.Generic;
using BankSimulator.core;
using BankSimulator.utils;
using Microsoft.Extensions.Logging;

public class ClientsProfiles
{
    private readonly ILogger<ClientsProfiles> _logger;
    private Dictionary<string, RandomCollection<ClientActionProfile>> profilePickerPerAction = new Dictionary<string, RandomCollection<ClientActionProfile>>();

    public ClientsProfiles(string filename, ILogger<ClientsProfiles> logger)
    {
        _logger = logger;
        var parameters = CSVReader<ClientActionProfile>.Read(filename);

        foreach (string action in ActionTypes.GetActions())
        {
            profilePickerPerAction[action] = new RandomCollection<ClientActionProfile>();
        }

        foreach (var profileString in parameters)
        {
            if (ActionTypes.IsValidAction(profileString.Action))
            {
                var profilePicker = profilePickerPerAction[profileString.Action];
                var clientActionProfile = new ClientActionProfile(
                    profileString.Action,
                    profileString.MaxCount,
                    profileString.MinCount,
                    profileString.AverageAmount,
                    profileString.StdAmount
                );
                profilePicker.Add(profileString.Freq, clientActionProfile);
            }
        }

        foreach (var profile in profilePickerPerAction.Values)
        {
            if (profile.IsEmpty())
            {
                _logger.LogWarning($"missing action in {filename}");
                break;
            }
        }
    }

    public ICollection<ClientActionProfile> GetProfilesFromAction(string action)
    {
        return profilePickerPerAction[action].GetCollection();
    }

    public ClientActionProfile PickNextActionProfile(string action)
    {
        return profilePickerPerAction[action].Next();
    }

    public void SetRandom(Random random)
    {
        foreach (var profilePicker in profilePickerPerAction.Values)
        {
            profilePicker.SetRandom(random);
        }
    }
}