using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;


public static class ExtendedMethods 
{
    public static T PickOne<T>(this IEnumerable<T> col)
    {
        int rnd = UnityEngine.Random.Range(0 , col.Count());
        return col.ToArray()[rnd];
    }
    public static T PickOneNoRepeat<T>(this IEnumerable<T> col, T lastPick)
    {
        int rnd = UnityEngine.Random.Range(0 , col.Count());
        var pick = col.ToArray()[rnd];
        while(pick.Equals(lastPick))
        {
            rnd = UnityEngine.Random.Range(0 , col.Count());
            pick = col.ToArray()[rnd];
        }
        return pick;
    }
}
public static class RandomService
{
    static System.Random rnd;
    public static int seed { get; private set; }
    static RandomService()
    {
        rnd = new System.Random();
        seed = rnd.Next(Int32.MinValue, Int32.MaxValue);
        
        rnd = new System.Random(seed);
    }
    public static void SetSeed(int seed)
    {
    RandomService.seed = seed;
    rnd = new System.Random(RandomService.seed);
    }
    public static bool RollD100(int chance) => rnd.Next(1, 101) <= chance;
    public static int GetRandom(int min, int max) => rnd.Next(min, max);
}

 public class InvalidRuleDeclarationException : Exception
{
    public override string Message => "Duplicate tag in special rule declaration, can only define one rule per tag";
}