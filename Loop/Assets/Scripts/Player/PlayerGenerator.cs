using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class PlayerGenerator
{
    public static int STANDARD_ARRAY;
    public static int DIST_POINTS;
    public static int STAT_CAP_ON_GENERATION;

    public static PlayerCharacter GeneratePlayerCharacter(TemplateSet set1)
    {
        Dictionary<int, List<int>> allStatsAndWeights = new Dictionary<int, List<int>>();
        AddKey(set1.StrWeight, ref allStatsAndWeights);
        AddKey(set1.DexWeight, ref allStatsAndWeights);
        AddKey(set1.VitWeight, ref allStatsAndWeights);
        AddKey(set1.IntWeight, ref allStatsAndWeights);
        AddKey(set1.WisWeight, ref allStatsAndWeights);

        int fullWeight = set1.CalcWeight();

        Dictionary<int, List<int>> secondary = allStatsAndWeights.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        allStatsAndWeights = secondary;

        int dist = DIST_POINTS;

        int[] array = { 0, 0, 0, 0, 0 };

        for (; dist > 0; --dist)
        {
            int rand = Random.GetInt(array.Length);
            ++array[rand];
        }

        array = array.OrderByDescending(x => x).ToArray();

        int ctr = 0;
        foreach(var kvp in allStatsAndWeights)
        {
            if (kvp.Value.Count > 1)
            {
                List<int> usedInds = new List<int>();
                while(usedInds.Count != kvp.Value.Count)
                {
                    int randInd = Random.GetInt(kvp.Value.Count);
                    if (usedInds.Contains(randInd))
                        continue;
                    kvp.Value[randInd] += array[ctr];
                    usedInds.Add(randInd);
                    ++ctr;
                }
            }
            else
            {
                kvp.Value[0] += array[ctr];
                ++ctr;
            }

        }

        PlayerCharacter newCharacter = new PlayerCharacter();
        newCharacter.Strength = GetValueFromKey(set1.StrWeight, ref allStatsAndWeights);
        newCharacter.Dexterity = GetValueFromKey(set1.DexWeight, ref allStatsAndWeights);
        newCharacter.Vitality = GetValueFromKey(set1.VitWeight, ref allStatsAndWeights);
        newCharacter.Intelligence = GetValueFromKey(set1.IntWeight, ref allStatsAndWeights);
        newCharacter.Wisdom = GetValueFromKey(set1.WisWeight, ref allStatsAndWeights);

        // ****TODO: Unsure if this is what I want for final HP/MP values. 
        newCharacter.RecalculateMaxHPAndMana();

        newCharacter.Name = NPCDataHolder.GetAnyForeName() + " " + NPCDataHolder.GetRandomSurName();

        return newCharacter;
    }

    public static PlayerCharacter GeneratePlayerCharacter(Template t1, TemplateSet ts1, Template t2, TemplateSet ts2)
    {
        PlayerCharacter c = GeneratePlayerCharacter(ts1);
        c.SetTemplate1(t1, ts1);
        c.SetTemplate2(t2, ts2);
        return c;
    }

    private static void AddKey(int key, ref Dictionary<int, List<int>> dict)
    {
        if (dict.ContainsKey(key))
            dict[key].Add(STANDARD_ARRAY);
        else
        {
            dict.Add(key, new List<int>());
            dict[key].Add(STANDARD_ARRAY);
        }
    }

    private static int GetValueFromKey(int key, ref Dictionary<int, List<int>> dict)
    {
        int ret;

        if (dict[key].Count > 1)
        {
            int rindex = Random.GetInt(0, dict[key].Count);
            ret = dict[key][rindex];
            dict[key].RemoveAt(rindex);
        }
        else
            ret = dict[key][0];

        return ret;
    }

    /// <summary>
    /// ****TODO: used for testing, will change when we have the finalized player can make their own class or select a premade.
    /// </summary>
    /// <returns></returns>
    public static void GenerateInitialCharacter()
    {
        //var @char = GeneratePlayerCharacter();
        //PlayerMaster.GetInstance().AddPlayer(@char);

        //var @char = GeneratePlayerCharacter(TemplateDataHolder.GetRandomTemplateAndSet());
        var tsas = TemplateDataHolder.GetAnyRandomTemplatesAndSets();
        var @char = GeneratePlayerCharacter(tsas.Item1, tsas.Item2, tsas.Item3, tsas.Item4);
        PlayerMaster.GetInstance().AddPlayer(@char);

        //Debugging...
        Debug.Log("We're generating our random intitial PC!");
        Debug.Log("Their name is: " + @char.Name);
        Debug.Log("Their Template1 is: " + tsas.Item1.TemplateIdentifier);
        Debug.Log("Their Template1 set is: " + tsas.Item2.SetIdentifier);
        Debug.Log("Their Template2 is: " + tsas.Item3.TemplateIdentifier);
        Debug.Log("Their Template2 set is: " + tsas.Item4.SetIdentifier);
        Debug.Log("Their Strength is: " + @char.Strength);
        Debug.Log("Their Dexterity is: " + @char.Dexterity);
        Debug.Log("Their Vitality is: " + @char.Vitality);
        Debug.Log("Their Int is: " + @char.Intelligence);
        Debug.Log("Their Wis is: " + @char.Wisdom);
        Debug.Log("Their HP is: " + @char.MaxHitpoints);
        Debug.Log("Their MP is: " + @char.MaxMana);
    }
}
