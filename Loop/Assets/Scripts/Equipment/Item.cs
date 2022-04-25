using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string Name;
    public int Id;
    public string Description;
}

public class Equipment : Item
{
    public int StrMod, DexMod, VitMod, IntMod, WisMod;
}

public class MainHand : Equipment
{
    public int weaponDamage;
    public float weaponRangeLower, weaponRangeUpper;
}

public class Armor : Equipment
{
    public int Hardness;
    public int Weight;
}

public class Accessory : Equipment
{

}