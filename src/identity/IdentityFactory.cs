namespace BankSimulator.identity;

using System;
using System.Collections.Generic;
using Bogus;
using Bogus.Extensions.UnitedKingdom;
using Bogus.Extensions.UnitedStates;

/// <summary>
/// Wraps the jFairy library and provides an identity generation function.
/// This keeps some of the jFairy confusion to a minimum.
/// </summary>
public class IdentityFactory
{
    private readonly HashSet<string> ccnSet = new HashSet<string>();
    private readonly HashSet<string> merchantIdSet = new HashSet<string>();
    private readonly Faker fairy;

    public IdentityFactory(int randomSeed)
    {
        fairy = new Faker();
    }

    protected string AddSuffixToEmail(string email, string suffix)
    {
        var parts = email.Split('@');
        if (parts.Length != 2)
        {
            throw new ArgumentException("Invalid email format");
        }
        return $"{parts[0]}{suffix}@{parts[1]}";
    }

    public ClientIdentity NextPerson()
    {
        var person = new Faker().Person;
        

        return new ClientIdentity(
            GetNextCreditCard(),
            person.FullName,
            AddSuffixToEmail(person.Email, person.Ssn().Substring(0, 3)),
            person.Ssn(),
            person.Phone
        );
    }

    public BankIdentity NextBank()
    {
        return new BankIdentity(GetNextVAT(), $"Bank of {fairy.Person.LastName}");
    }

    public MerchantIdentity NextMerchant()
    {

        var company = fairy.Company;
        var companyName = company.CompanyName();
        var vat = fairy.Finance.VatNumber(VatRegistrationNumberType.Standard);
        while (!merchantIdSet.Add(vat))
        {
            vat = fairy.Finance.VatNumber(VatRegistrationNumberType.Standard);
        }

        return new MerchantIdentity(vat, companyName);
    }

    public string GetNextVAT()
    {
        return fairy.Finance.VatNumber(VatRegistrationNumberType.Standard);
    }

    public string GetNextCreditCard()
    {
        var ccn = fairy.Finance.CreditCardNumber();
        while (!ccnSet.Add(ccn))
        {
            ccn = fairy.Finance.CreditCardNumber();
        }
        return ccn;
    }

    public string NextMerchantName()
    {
        var company = fairy.Company;
        return company.CompanyName();
    }
}
