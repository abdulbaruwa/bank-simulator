namespace BankSimulator.identity;

using CsvHelper.Configuration.Attributes;
public class ClientIdentity : Identity
{
    [Name("email")]
    public string Email { get; }

    [Name("ssn")]
    public string Ssn { get; }

    [Name("phoneNumber")]
    public string PhoneNumber { get; }

    public ClientIdentity(string id, string name, string email, string ssn, string phoneNumber)
        : base(id, name)
    {
        Email = email;
        Ssn = ssn;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Creates a new ClientIdentity from this instance, replacing the given property with the provided value.
    /// </summary>
    /// <returns>new ClientIdentity, copying</returns>
    public ClientIdentity ReplaceProperty(string property, string value)
    {
        return property switch
        {
            Properties.Email => new ClientIdentity(Id, Name, value, Ssn, PhoneNumber),
            Properties.Ssn => new ClientIdentity(Id, Name, Email, value, PhoneNumber),
            Properties.Phone => new ClientIdentity(Id, Name, Email, Ssn, value),
            _ => this,
        };
    }

    public override Dictionary<string, object> AsMap()
    {
        return new Dictionary<string, object>
            {
                { Properties.Name, Name },
                { Properties.Email, Email },
                { Properties.Phone, PhoneNumber },
                { Properties.Ssn, Ssn },
                { Properties.Ccn, Id }
            };
    }
}
