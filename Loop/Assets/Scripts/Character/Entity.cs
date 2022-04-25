using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public string Name;

    public int Strength, Dexterity, Vitality, Intelligence, Wisdom;

    public readonly int MaximumActions = 4;
    public int Level;

    protected List<Ability> _abilities = new List<Ability>();

    protected List<OutgoingStatus> _outgoingStatuses = new List<OutgoingStatus>();
    protected List<IncomingStatus> _incomingStatuses = new List<IncomingStatus>();

    public int MaxHitpoints, CurrentHitpoints;
    public int MaxMana, currentMana;

    protected MainHand _mainHand;
    protected Armor _armor;
    protected Accessory[] _accessories = new Accessory[2];

    public void TakeDamage(int damage)
    {
        /****TODO:
         * Go through each status and modify damage as needed.
         * Go through equipment to further reduce damage. 
         */
        damage -= (Vitality / 2);
        damage -= _armor.Hardness;


        if (damage < 1)
            damage = 1;
        CurrentHitpoints -= damage;
    }

    public void Heal(int damage)
    {
        damage += (Wisdom / 2);
        CurrentHitpoints += damage;
    }

    public MainHand GetMainHand()
    {
        return _mainHand;
    }
}