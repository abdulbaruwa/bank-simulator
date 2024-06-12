namespace BankSimulator.utils;

using Microsoft.Extensions.Configuration;

public class Config : IConfig
{
    private readonly IConfiguration _configuration;

    public Config(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("ConfigurationSettings");
    }

    public int Seed => _configuration.GetValue<int>("Seed");
    public int NbSteps => _configuration.GetValue<int>("NbSteps");
    public int Multiplier => _configuration.GetValue<int>("Multiplier");
    public int NbClients => _configuration.GetValue<int>("NbClients");
    public int NbFraudsters => _configuration.GetValue<int>("NbFraudsters");
    public int NbMerchants => _configuration.GetValue<int>("NbMerchants");
    public int NbBanks => _configuration.GetValue<int>("NbBanks");
    public double MerchantReuseProbability => _configuration.GetValue<double>("MerchantReuseProbability");
    public double ClientReuseProbability => _configuration.GetValue<double>("ClientReuseProbability");
    public double ClientAcquaintanceProbability => _configuration.GetValue<double>("ClientAcquaintanceProbability");
    public double FirstPartyFraudProbability => _configuration.GetValue<double>("FirstPartyFraudProbability");
    public double ThirdPartyFraudProbability => _configuration.GetValue<double>("ThirdPartyFraudProbability");
    public double ThirdPartyNewVictimProbability => _configuration.GetValue<double>("ThirdPartyNewVictimProbability");
    public double ThirdPartyPercentHighRiskMerchants => _configuration.GetValue<double>("ThirdPartyPercentHighRiskMerchants");
    public long TransferLimit => _configuration.GetValue<long>("TransferLimit");
    public string TransactionsTypes => _configuration.GetValue<string>("TransactionsTypes");
    public string AggregatedTransactions => _configuration.GetValue<string>("AggregatedTransactions");
    public string ClientsProfiles => _configuration.GetValue<string>("ClientsProfiles");
    public string InitialBalancesDistribution => _configuration.GetValue<string>("InitialBalancesDistribution");
    public string OverdraftLimits => _configuration.GetValue<string>("OverdraftLimits");
    public string MaxOccurrencesPerClient => _configuration.GetValue<string>("MaxOccurrencesPerClient");
    public string TypologiesFolder => _configuration.GetValue<string>("TypologiesFolder");
    public string OutputPath => _configuration.GetValue<string>("OutputPath");
    public bool SaveToDB => _configuration.GetValue<bool>("SaveToDB");
    public string DbUrl => _configuration.GetValue<string>("DbUrl");
    public string DbUser => _configuration.GetValue<string>("DbUser");
    public string DbPassword => _configuration.GetValue<string>("DbPassword");
}
