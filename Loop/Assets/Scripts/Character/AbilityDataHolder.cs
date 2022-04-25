using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityDataHolder
{
    private static List<Ability> km_playerAbilities = new List<Ability>();
    private static List<Ability> km_enemyAbilities = new List<Ability>();

    public static void AddPlayerAbility(Ability a)
    {
        km_playerAbilities.Add(a);
    }

    public static void AddEnemyAbility(Ability a)
    {
        km_enemyAbilities.Add(a);
    }

    public static Ability GetPlayerAbility(int i)
    {
        return km_playerAbilities.Find(x => x.Id == i);
    }

    public static Ability GetMonsterAbility(int i)
    {
        return km_enemyAbilities.Find(x => x.Id == i);
    }
}
