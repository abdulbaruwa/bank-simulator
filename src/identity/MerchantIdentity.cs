namespace BankSimulator.identity;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
public class MerchantIdentity : Identity
{
    [Name("highRisk")]
    private bool highRisk;

    public MerchantIdentity(string id, string name, bool isHighRisk)
        : base(id, name)
    {
        highRisk = isHighRisk;
    }

    public MerchantIdentity(string id, string name)
        : this(id, name, false)
    {
    }

    public bool IsHighRisk()
    {
        return highRisk;
    }

    public void SetHighRisk(bool value)
    {
        highRisk = value;
    }

    public override Dictionary<string, object> AsMap()
    {
        var map = new Dictionary<string, object>
            {
                { Properties.Name, Name },
                { Properties.Id, Id },
                { Properties.HighRisk, highRisk }
            };
        return map;
    }
}

