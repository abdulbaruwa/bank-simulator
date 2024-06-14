namespace BankSimulator.identity;
using System.Collections.Generic;

public interface IIdentifiable
{
    string GetId();

    string GetName();

    Identity GetIdentity();

    Dictionary<string, object> GetIdentityAsMap();
}
