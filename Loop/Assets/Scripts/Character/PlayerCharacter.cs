using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Entity
{
    protected List<Ability> _abilityPool = new List<Ability>();
    protected List<Ability> _activeAbilities = new List<Ability>();

    protected Template _template;
    protected Template _template2;
    protected TemplateSet _templateSet1, _templateSet2;

    public void SetAbilityPool(List<Ability> @new)
    {
        _abilityPool = @new;
    }

    // Needed for when we add things via zodiac stone systems.
    public void AddAbility(Ability a)
    {

    }

    public void RemoveAbility(Ability a)
    {

    }

    public void SetTemplate1(Template t1, TemplateSet ts1)
    {
        _template = t1;
        _templateSet1 = ts1;
    }

    public void SetTemplate2(Template t1, TemplateSet ts2)
    {
        _template2 = t1;
        _templateSet2 = ts2;
    }

    public void RecalculateMaxHPAndMana()
    {
        // ****TODO: Unsure if this is what I want for final HP/MP values. 
        MaxHitpoints = 10 + Vitality + Strength / 2 + Dexterity / 3;
        MaxMana = 10 + (Intelligence / 2 + Wisdom / 2);
    }
}
