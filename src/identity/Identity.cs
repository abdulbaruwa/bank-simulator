namespace BankSimulator.identity;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;

    /// <summary>
    /// A core "identity" for an account requires an `id` and a `name`.
    /// </summary>
    public abstract class Identity
    {
        [Name("id")]
        public string Id { get; }

        [Name("name")]
        public string Name { get; }

        protected Identity(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }

        public abstract Dictionary<string, object> AsMap();
    }

public interface IIdentifiable
{
    string GetId();

    string GetName();

    Identity GetIdentity();

    Dictionary<string, object> GetIdentityAsMap();
}

public static class Properties
{
    public const string Id = "id";
    public const string Name = "name";
    public const string Phone = "phone";
    public const string Email = "email";
    public const string Ssn = "ssn";
    public const string Ccn = "ccn";
    public const string HighRisk = "highRisk";
}