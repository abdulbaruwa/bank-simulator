namespace BankSimulator.utils;
public interface IConfig
{
    int Seed { get; }
    int NbSteps { get; }
    int Multiplier { get; }
    int NbClients { get; }
    int NbFraudsters { get; }
    int NbMerchants { get; }
    int NbBanks { get; }
    double MerchantReuseProbability { get; }
    double ClientReuseProbability { get; }
    double ClientAcquaintanceProbability { get; }
    double FirstPartyFraudProbability { get; }
    double ThirdPartyFraudProbability { get; }
    double ThirdPartyNewVictimProbability { get; }
    double ThirdPartyPercentHighRiskMerchants { get; }
    long TransferLimit { get; }
    string TransactionsTypes { get; }
    string AggregatedTransactions { get; }
    string ClientsProfiles { get; }
    string InitialBalancesDistribution { get; }
    string OverdraftLimits { get; }
    string MaxOccurrencesPerClient { get; }
    string TypologiesFolder { get; }
    string OutputPath { get; }
    bool SaveToDB { get; }
    string DbUrl { get; }
    string DbUser { get; }
    string DbPassword { get; }
}
