namespace BankSimulator.utils;

using System;
using System.Collections.Generic;


public class RandomCollection<T>
{
    private readonly SortedDictionary<double, T> map = new SortedDictionary<double, T>();
    private Random random;
    private double total = 0;

    public RandomCollection()
    {
        this.random = null;
    }

    public RandomCollection(Random random)
    {
        this.random = random;
    }

    public void Add(double weight, T result)
    {
        if (weight > 0)
        {
            total += weight;
            map[total] = result;
        }
    }

    public T Next()
    {
        if (this.random == null)
        {
            throw new NullReferenceException("The RNG must be initialized to pick a random element.");
        }
        if (this.map.Count == 0)
        {
            throw new InvalidOperationException("The collection is empty");
        }

        double value = random.NextDouble() * total;
        foreach (var entry in map)
        {
            if (entry.Key > value)
            {
                return entry.Value;
            }
        }
        throw new InvalidOperationException("Failed to retrieve an element");
    }

    public ICollection<T> GetCollection()
    {
        return map.Values;
    }

    public void SetRandom(Random random)
    {
        this.random = random;
    }

    public bool IsEmpty()
    {
        return map.Count == 0;
    }
}