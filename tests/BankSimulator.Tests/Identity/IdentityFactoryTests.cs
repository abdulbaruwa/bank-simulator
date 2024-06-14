namespace BankSimulator.Tests.Identity;

using BankSimulator.identity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class IdentityFactoryTest
{
    [Test]
    public void SuccessiveCallsCreateNewPeople()
    {
        IdentityFactory factory = new IdentityFactory(1);
        ClientIdentity identity1 = factory.NextPerson();
        ClientIdentity identity2 = factory.NextPerson();
        Assert.AreNotEqual(identity1, identity2);
        Assert.AreNotEqual(identity1.Name, identity2.Name);
        Assert.AreNotEqual(identity1.Id, identity2.Id);
    }

    [Test]
    public void GeneratedEmailsAddRandomDigits()
    {
        IdentityFactory factory = new IdentityFactory(1);
        ClientIdentity identity = factory.NextPerson();
        string email = identity.Email;
        string[] parts = email.Split('@');
        Assert.AreEqual(2, parts.Length);
        int addrlen = parts[0].Length;

        try
        {
            int suffix = int.Parse(parts[0].Substring(addrlen - 3));
            Assert.IsTrue(suffix > 0, $"{suffix} should be double digits");
        }
        catch (Exception)
        {
            Assert.Fail($"didn't find digits in email: {email}");
        }
    }

    [Test]
    public async Task CollisionTest()
    {
        Console.WriteLine("Starting collisionTest...");
        long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        int max = 1_000_000;
        AtomicInteger collisions = new AtomicInteger(0);
        IdentityFactory factory = new IdentityFactory(1);
        HashSet<string> ccSet = new HashSet<string>(max);

        CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
        try
        {
            await Task.Run(() =>
            {
                while (ccSet.Count < max && !cts.Token.IsCancellationRequested)
                {
                    string cc = factory.GetNextCreditCard();
                    while (!ccSet.Add(cc))
                    {
                        collisions.IncrementAndGet();
                        cc = factory.GetNextCreditCard();
                    }
                }
            }, cts.Token);
        }
        catch (TaskCanceledException)
        {
            Assert.Fail("Test timed out");
        }

        long finish = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Console.WriteLine($"Finished in {finish - start} millis");
        Console.WriteLine($"collisions: {collisions.Get()}");
        Assert.AreEqual(0, collisions.Get());
    }

    [Test]
    public async Task MerchantCollisionTest()
    {
        Console.WriteLine("Starting merchantCollisionTest...");
        long start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        int max = 1_000_000;
        AtomicInteger collisions = new AtomicInteger(0);
        IdentityFactory factory = new IdentityFactory(1);
        HashSet<string> merchantSet = new HashSet<string>(max);

        CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(120));
        try
        {
            await Task.Run(() =>
            {
                while (merchantSet.Count < max && !cts.Token.IsCancellationRequested)
                {
                    MerchantIdentity mi = factory.NextMerchant();
                    while (!merchantSet.Add(mi.Id))
                    {
                        collisions.IncrementAndGet();
                        mi = factory.NextMerchant();
                    }
                }
            }, cts.Token);
        }
        catch (TaskCanceledException)
        {
            Assert.Fail("Test timed out");
        }

        long finish = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Console.WriteLine($"Finished in {finish - start} millis");
        Console.WriteLine($"collisions: {collisions.Get()}");
        Assert.AreEqual(0, collisions.Get());
    }
}

// Helper class to simulate AtomicInteger from Java
public class AtomicInteger
{
    private int value;

    public AtomicInteger(int initialValue)
    {
        value = initialValue;
    }

    public int IncrementAndGet()
    {
        return Interlocked.Increment(ref value);
    }

    public int Get()
    {
        return value;
    }
}