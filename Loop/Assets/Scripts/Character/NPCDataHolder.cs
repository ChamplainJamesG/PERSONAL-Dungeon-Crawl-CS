using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NPCDataHolder
{
    private static List<string> k_RandomNPCForeName = new List<string>();
    private static List<string> k_RandomNPCForeNameF = new List<string>();
    private static List<string> k_RandomNPCSurName = new List<string>();

    public static void AddForeName(string s)
    {
        k_RandomNPCForeName.Add(s);
    }

    public static void AddForeNameF(string s)
    {
        k_RandomNPCForeNameF.Add(s);
    }

    public static void AddSureName(string s)
    {
        k_RandomNPCSurName.Add(s);
    }

    public static string GetRandomForeName()
    {
        return Random.GetRandomInList(k_RandomNPCForeName);
    }

    public static string GetRandomForeNameF()
    {
        return Random.GetRandomInList(k_RandomNPCForeNameF);
    }

    public static string GetAnyForeName()
    {
        int r = Random.GetInt(2);

        if (r == 0)
            return GetRandomForeName();
        else
            return GetRandomForeNameF();
    }

    public static string GetRandomSurName()
    {
        return Random.GetRandomInList(k_RandomNPCSurName);
    }
}
