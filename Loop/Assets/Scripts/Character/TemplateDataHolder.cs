using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template
{
    public List<TemplateSet> m_allSets = new List<TemplateSet>();
    public string TemplateIdentifier;

    public void AddSet(TemplateSet ts)
    {
        m_allSets.Add(ts);
    }
}

public class TemplateSet
{
    private List<Ability> m_abilitiesToLearn = new List<Ability>();
    public string SetIdentifier;

    public int StrWeight, DexWeight, VitWeight, IntWeight, WisWeight;

    public void AddAbility(Ability a)
    {
        m_abilitiesToLearn.Add(a);
    }

    public void AddAbility(int a)
    {
        Ability add = AbilityDataHolder.GetPlayerAbility(a);
        m_abilitiesToLearn.Add(add);
    }

    public int CalcWeight()
    {
        return StrWeight + DexWeight + VitWeight + IntWeight + WisWeight;
    }
}

public static class TemplateDataHolder
{
    private static List<Template> km_allTemplates = new List<Template>();

    public static void AddTemplate(Template t)
    {
        km_allTemplates.Add(t);
    }

    public static TemplateSet GetRandomTemplateAndSet()
    {
        Template t = Random.GetRandomInList(km_allTemplates);
        TemplateSet ts = Random.GetRandomInList(t.m_allSets);

        return ts;
    }


    // DEBUGGING PURPOSES ONLY
    public static (Template, TemplateSet, Template, TemplateSet) GetAnyRandomTemplatesAndSets()
    {
        Template t1 = Random.GetRandomInList(km_allTemplates);
        TemplateSet ts1 = Random.GetRandomInList(t1.m_allSets);

        Template t2 = Random.GetRandomInList(km_allTemplates);
        TemplateSet ts2 = Random.GetRandomInList(t2.m_allSets);

        return (t1, ts1, t2, ts2);
    }
}
