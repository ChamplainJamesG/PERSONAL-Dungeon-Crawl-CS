using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EquipmentTemplate
{
    public int Id;
    public string BaseName;
    public int StrWeight, DexWeight, ConWeight, IntWeight, WisWeight, PieWeight;
    public string baseDesc;
    
    public int CalcWeight()
    {
        return StrWeight + DexWeight + ConWeight + IntWeight + WisWeight + PieWeight;
    }
}

public class MainHandTemplate : EquipmentTemplate
{
    // these are static values per template
    // for example a tier 1 sword and a tier 9 sword will always have a WR of 0.8-1.2
    public float WeaponRangeLower, WeaponRangeUpper;
    // templates have static weapon damage per tier.
    public int WeaponDamage;
}

public class ArmorTemplate : EquipmentTemplate
{
    // all armor will have a static hardness per tier.
    public int Hardness;
}

public class EquipmentTier
{
    public List<string> Prefixes = new List<string>();
    public List<string> Suffixes = new List<string>();
    // i.e, how many points are distributed per item.
    public int DistPtsAvg;
    public int DistPtsLower, DistPtsUpper;

    // This modified how the main equipment stat is per tier.
    // For example, lets say an iron sword does 10 equipment damage.
    // We want a steel sword to be stronger. Thus a Steel sword will have a higher TierMod (i.e, 1.2).
    // THIS IS A MULTIPLIER EFFECT. TAKE CARE IT DOES NOT GET TOO POWERFUL TOO QUICKLY.
    public float TierMod;
}

public static class ItemGenerationData
{
    //private static List<EquipmentTemplate> km_templates = new List<EquipmentTemplate>();
    private static List<MainHandTemplate> km_weaponTemplates = new List<MainHandTemplate>();
    private static List<ArmorTemplate> km_armorTemplates = new List<ArmorTemplate>();

    private static List<EquipmentTier> km_equipmentTiers = new List<EquipmentTier>();

    public static void AddNewMainHand(MainHandTemplate mht)
    {
        km_weaponTemplates.Add(mht);
    }

    public static void AddNewArmorTemplate(ArmorTemplate at)
    {
        km_armorTemplates.Add(at);
    }

    public static void AddNewTier(EquipmentTier et)
    {
        km_equipmentTiers.Add(et);
    }

    public static void GenerateNewWeapon(int tier)
    {
        MainHandTemplate mainHandTemplate = Random.GetRandomInList(km_weaponTemplates);
        EquipmentTier activeTier = km_equipmentTiers[tier];

        int ptsToDist = Random.GetInt(activeTier.DistPtsLower, activeTier.DistPtsUpper);

        // chance for item to not have shit rolls
        if(ptsToDist < activeTier.DistPtsAvg)
        {
            int randomChance = Random.GetInt(0, 100);
            if (randomChance > 50)
                ptsToDist = activeTier.DistPtsAvg;
        }

        // find our WD
        int weaponDamage = Mathf.FloorToInt(activeTier.TierMod * (float)mainHandTemplate.WeaponDamage);
        // put a maximal value in the main stat.
        int mainStatValue = activeTier.DistPtsAvg;

        // key is the stat's weight. The value is the current amount of pts currently distributed to that stat.
        Dictionary<int, int> allStatsAndWeights = new Dictionary<int, int>();

        // init.
        allStatsAndWeights.Add(mainHandTemplate.StrWeight, 0);
        allStatsAndWeights.Add(mainHandTemplate.DexWeight, 0);
        allStatsAndWeights.Add(mainHandTemplate.ConWeight, 0);
        allStatsAndWeights.Add(mainHandTemplate.IntWeight, 0);
        allStatsAndWeights.Add(mainHandTemplate.WisWeight, 0);
        allStatsAndWeights.Add(mainHandTemplate.PieWeight, 0);

        int fullWeight = mainHandTemplate.CalcWeight();

        // sort
        //var secondary = from entry in allStatsAndWeights orderby entry.Key ascending select entry;
        Dictionary<int, int> secondary = allStatsAndWeights.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        allStatsAndWeights = secondary;

        // find the main stat first...
        int highest = -1;
        foreach (var kvp in secondary)
        {
            if (kvp.Key > highest)
                highest = kvp.Key;
        }

        allStatsAndWeights[highest] = mainStatValue;

        for (; ptsToDist > 0; --ptsToDist)
        {
            int randomVal = Random.GetInt(1, fullWeight);

            foreach(var kvp in allStatsAndWeights)
            {
                if(randomVal < kvp.Key && kvp.Key != highest)
                {
                    if(allStatsAndWeights[kvp.Key] < mainStatValue)
                    {
                        ++allStatsAndWeights[kvp.Key];
                        break;
                    }
                }

                randomVal -= kvp.Key;
            }
        }

        MainHand finalWep = new MainHand();
        finalWep.Description = mainHandTemplate.baseDesc;
        finalWep.Name = mainHandTemplate.BaseName;
        finalWep.StrMod = allStatsAndWeights[mainHandTemplate.StrWeight];
        finalWep.DexMod = allStatsAndWeights[mainHandTemplate.DexWeight];
        finalWep.IntMod = allStatsAndWeights[mainHandTemplate.IntWeight];
        finalWep.VitMod = allStatsAndWeights[mainHandTemplate.ConWeight];
        finalWep.WisMod = allStatsAndWeights[mainHandTemplate.WisWeight];
        //finalWep.StrMod = secondary[mainHandTemplate.StrWeight];
        finalWep.weaponDamage = weaponDamage;
        finalWep.weaponRangeLower = mainHandTemplate.WeaponRangeLower;
        finalWep.weaponRangeUpper = mainHandTemplate.WeaponRangeUpper;

        Debug.Log("We have just generated a very cool weapon" +
            "!");
        Debug.Log("<color=blue>Its name is: " + finalWep.Name + "</color>");
        Debug.Log("<color=red>Its weapon power is: " + finalWep.weaponDamage + "</color>");
        Debug.Log("And it's modifiers are as follows...");
        Debug.Log("<color=red>" + finalWep.StrMod + " Str!</color>");
        Debug.Log("<color=yellow>" + finalWep.DexMod + " Dex!</color>");
        Debug.Log("<color=cyan>" + finalWep.VitMod + " Vit!</color>");
        Debug.Log("<color=green>" + finalWep.IntMod + " Int!</color>");
        Debug.Log("<color=white>" + finalWep.WisMod + " Wis!</color>");
    }
}