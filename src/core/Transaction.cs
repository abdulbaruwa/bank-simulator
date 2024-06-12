namespace BankSimulator.core;

using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;

    [Serializable]
    public class Transaction
    {
        private const long serialVersionUID = 1L;

        public int Step { get; }

        [Name("globalStep")]
        public int GlobalStep { get; set; } = -1;

        [Name("action")]
        public string Action { get; }

        [Name("amount")]
        public double Amount { get; }

        [Name("idOrig")]
        public string IdOrig { get; }

        [Name("nameOrig")]
        public string NameOrig { get; }

        [Name("typeOrig")]
        public SuperActor.Type TypeOrig { get; }

        public double OldBalanceOrig { get; }
        public double NewBalanceOrig { get; }

        [Name("idDest")]
        public string IdDest { get; }

        [Name("nameDest")]
        public string NameDest { get; }

        [Name("typeDest")]
        public SuperActor.Type TypeDest { get; }

        public double OldBalanceDest { get; }
        public double NewBalanceDest { get; }

        [Name("isFraud")]
        public bool IsFraud { get; set; } = false;

        [Name("isFlaggedFraud")]
        public bool IsFlaggedFraud { get; set; } = false;

        public bool IsUnauthorizedOverdraft { get; set; } = false;
        public bool IsSuccessful { get; set; } = false;

        public Transaction(int step, string action, double amount, SuperActor originator, double oldBalanceOrig,
                           double newBalanceOrig, SuperActor destination, double oldBalanceDest, double newBalanceDest)
        {
            Step = step;
            Action = action;
            Amount = amount;

            IdOrig = originator.Id;
            NameOrig = originator.Name;
            TypeOrig = originator.ActorType;
            OldBalanceOrig = oldBalanceOrig;
            NewBalanceOrig = newBalanceOrig;

            IdDest = destination.Id;
            NameDest = destination.Name;
            TypeDest = destination.ActorType;
            OldBalanceDest = oldBalanceDest;
            NewBalanceDest = newBalanceDest;
        }

        public bool IsFailedTransaction()
        {
            return IsFlaggedFraud || IsUnauthorizedOverdraft;
        }

        public override string ToString()
        {
            var properties = new List<string>
            {
                Step.ToString(),
                Action,
                Output.FastFormatDouble(Output.PrecisionOutput, Amount),
                IdOrig,
                Output.FastFormatDouble(Output.PrecisionOutput, OldBalanceOrig),
                Output.FastFormatDouble(Output.PrecisionOutput, NewBalanceOrig),
                IdDest,
                Output.FastFormatDouble(Output.PrecisionOutput, OldBalanceDest),
                Output.FastFormatDouble(Output.PrecisionOutput, NewBalanceDest),
                Output.FormatBoolean(IsFraud),
                Output.FormatBoolean(IsFlaggedFraud),
                Output.FormatBoolean(IsUnauthorizedOverdraft),
                Output.FormatBoolean(IsSuccessful)
            };

            return string.Join(Output.OutputSeparator, properties);
        }
    }

    public static class Output
    {
        public const string OutputSeparator = ","; // Assuming comma as the separator
        public const int PrecisionOutput = 2;

        public static string FastFormatDouble(int precision, double value)
        {
            return value.ToString($"F{precision}");
        }

        public static string FormatBoolean(bool value)
        {
            return value ? "true" : "false";
        }
    }

    public class SuperActor
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Type ActorType { get; set; }

        public enum Type
        {
            ActorType1,
            ActorType2
        }

        public Type GetType()
        {
            return ActorType;
        }
    }
