using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public string Name;
    public int Id;
    public float Potency;
    public int Cost;
    public int Targets;

    public string ForwardAbilityDesc;

    public delegate void AbilityCast(Entity caster, ref Entity target, Ability cast);

    protected AbilityCast _cast;

    public enum KEY_ATTRIBUTE
    {
        STRENGTH = 0,
        DEXTERITY,
        CONSTITUTION,
        INTELLIGENCE,
        WISDOM
    }

    public KEY_ATTRIBUTE KeyAttr;

    public AbilityCast GetCast()
    {
        return _cast;
    }

    public void SetCast(AbilityCast cast)
    {
        _cast = cast;
    }

    public virtual void CastAbility(Entity caster, ref Entity target)
    {
        Debug.LogError("This is a generic cast. This should never happen. If you see this, panic");
    }
}

public class AttackAbility : Ability
{
    public override void CastAbility(Entity caster, ref Entity target)
    {
        float damage = (caster.Strength + caster.Level) * Random.GetFloatInclusive(0.8f, 1.2f);
        damage *= Potency;
        damage += (caster.GetMainHand().weaponDamage * Random.GetFloatInclusive(caster.GetMainHand().weaponRangeLower, caster.GetMainHand().weaponRangeUpper));
        int final_damage = Mathf.CeilToInt(damage);
        target.TakeDamage(final_damage);
    }
}

public class SpellAbility : Ability
{
    public override void CastAbility(Entity caster, ref Entity target)
    {
        float damage = (caster.Intelligence + caster.Level) * Random.GetFloatInclusive(0.8f, 1.2f);
        damage *= Potency;
        damage += caster.GetMainHand().weaponDamage * Random.GetFloatInclusive(caster.GetMainHand().weaponRangeLower, caster.GetMainHand().weaponRangeUpper);
        int final_damage = Mathf.CeilToInt(damage);
        target.TakeDamage(final_damage);
    }
}

public static class CastsLogic
{
    public static void Attack(Entity caster, ref Entity target, Ability ability)
    {
        float damage = (caster.Strength + caster.Level) * Random.GetFloatInclusive(0.8f, 1.2f);
        damage *= ability.Potency;
        damage = damage + (caster.GetMainHand().weaponDamage * Random.GetFloatInclusive(caster.GetMainHand().weaponRangeLower, caster.GetMainHand().weaponRangeUpper));
        int final_damage = Mathf.CeilToInt(damage);
        target.TakeDamage(final_damage);
    }
}