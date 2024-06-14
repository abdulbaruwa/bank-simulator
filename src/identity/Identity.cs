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
