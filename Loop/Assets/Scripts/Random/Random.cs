using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Random
{
    private static System.Random km_rand;
    private static int seed;
    private static bool m_seeded;

    public static void SeedRandom()
    {
        if (m_seeded)
            return;
        seed = System.Guid.NewGuid().GetHashCode();
        km_rand = new System.Random(seed);
        m_seeded = true;
    }

    public static int GetInt(int lower, int upper)
    {
        return km_rand.Next(lower, upper);
    }

    public static int GetInt(int upper)
    {
        return km_rand.Next(upper);
    }

    public static int GetIntInclusive(int lower, int upper)
    {
        return km_rand.Next(lower, upper + 1);
    }

    public static float GetFloatInclusive(float lower, float upper)
    {
        return (float)(km_rand.NextDouble() * (upper - lower) + lower);
    }

    public static T GetRandomInList<T>(List<T> l)
    {
        return l[km_rand.Next(l.Count)];
    }

}
