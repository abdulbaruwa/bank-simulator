namespace BankSimulator.identity;
using System.Collections.Generic;

public class BankIdentity : Identity
{
    private const string BankIdentifier = "B";

    public BankIdentity(string id, string name)
        : base(BankIdentifier + id, name)
    {
    }

    public override Dictionary<string, object> AsMap()
    {
        var map = new Dictionary<string, object>
            {
                { Properties.Name, Name }
            };
        return map;
    }
}
