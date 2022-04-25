using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class FileReader : MonoBehaviour
{
    private readonly string FILE_PATH = Application.streamingAssetsPath + "/";

    private void Awake()
    {
        // ****TODO: PUT THIS SOMEWHERE BETTER SO I'M NOT CONSTANTLY RESEEDING.
        Random.SeedRandom();
        ReadAbilities();
        ReadNPCData();
        ReadItems();
        ReadEquipment();
        ReadTemplates();
    }

    private XmlDocument CreateXML(string path)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(FILE_PATH + path);
        return doc;
    }

    private void ReadAbilities()
    {
        XmlDocument abilityDoc = CreateXML("Ability.xml");

        var allAbilities = abilityDoc.SelectNodes("root/player_abilities/ability");

        foreach(XmlNode a in allAbilities)
        {
            AbilityDataHolder.AddPlayerAbility(GenerateAbility(a));

        }

        allAbilities = abilityDoc.SelectNodes("root/monster_abilities/ability");

        foreach(XmlNode a in allAbilities)
        {
            AbilityDataHolder.AddEnemyAbility(GenerateAbility(a));
        }

        //Debug.Log("These are some cool abilities generated...");
        //Debug.Log("<color=red>" + AbilityDataHolder.GetPlayerAbility(0).Name + "</color>");
        //Debug.Log("<color=yellow>" + AbilityDataHolder.GetPlayerAbility(0).ForwardAbilityDesc + "</color>");
        //Debug.Log("<color=green>" + AbilityDataHolder.GetPlayerAbility(0).Potency + "</color>");
        //Debug.Log("<color=blue>" + AbilityDataHolder.GetPlayerAbility(0).Cost + "</color>");
    }

    private Ability GenerateAbility(XmlNode a)
    {
        //int value = System.Int32.Parse(a.Attributes[0].Value, System.Globalization.NumberStyles.HexNumber);
        int value = System.Convert.ToInt32(a.Attributes[0].Value, 16);

        Debug.Log("Value is: " + value);

        Ability new_ability = new Ability
        {
            Id = value,
            Name = a.Attributes["name"].Value,
            Potency = int.Parse(a.Attributes["potency"].Value),
            Cost = int.Parse(a.Attributes["cost"].Value),
            ForwardAbilityDesc = a.Attributes["desc"].Value,
            Targets = int.Parse(a.Attributes["targets"].Value)
        };
        new_ability.SetCast(CastsLogic.Attack);
        return new_ability;
    }

    private void ReadNPCData()
    {
        XmlDocument NPCDataDoc = CreateXML("Character.xml");

        XmlNodeList l = NPCDataDoc.SelectNodes("root/forenames/male/name");
        foreach(XmlNode c in l)
            NPCDataHolder.AddForeName(c.Attributes[0].Value);

        l = NPCDataDoc.SelectNodes("root/forenames/female/name");
        foreach (XmlNode c in l)
            NPCDataHolder.AddForeNameF(c.Attributes[0].Value);

        l = NPCDataDoc.SelectNodes("root/surnames/name");
        foreach (XmlNode c in l)
            NPCDataHolder.AddSureName(c.Attributes[0].Value);
    }

    private void ReadItems()
    {

    }

    private void ReadEquipment()
    {
        XmlDocument equipmentDoc = CreateXML("Equipment.xml");

        XmlNodeList weapons = equipmentDoc.SelectNodes("root/main_hand_templates/template");
        foreach(XmlNode c in weapons)
        {
            //EquipmentTemplate newEquipment = GenerateEquipment(c);
            MainHandTemplate newWeapon = new MainHandTemplate();
            //GenerateEquipment2(c, ref newWeapon);
            newWeapon.BaseName = c.Attributes[0].Value;
            newWeapon.StrWeight = int.Parse(c.Attributes["strweight"].Value);
            newWeapon.DexWeight = int.Parse(c.Attributes["dexweight"].Value);
            newWeapon.ConWeight = int.Parse(c.Attributes["conweight"].Value);
            newWeapon.IntWeight = int.Parse(c.Attributes["intweight"].Value);
            newWeapon.WisWeight = int.Parse(c.Attributes["wisweight"].Value);
            newWeapon.baseDesc = c.Attributes["desc"].Value;
            newWeapon.WeaponDamage = int.Parse(c.Attributes["dam"].Value);
            newWeapon.WeaponRangeLower = float.Parse(c.Attributes["rangeL"].Value);
            newWeapon.WeaponRangeUpper = float.Parse(c.Attributes["rangeL"].Value);
            ItemGenerationData.AddNewMainHand(newWeapon);
        }

        XmlNodeList armor = equipmentDoc.SelectNodes("root/armor_templates/template");
        foreach (XmlNode c in armor)
        {
            //ArmorTemplate newArmor = (ArmorTemplate)GenerateEquipment(c);
            ArmorTemplate newArmor = new ArmorTemplate();
            newArmor.BaseName = c.Attributes[0].Value;
            newArmor.StrWeight = int.Parse(c.Attributes["strweight"].Value);
            newArmor.DexWeight = int.Parse(c.Attributes["dexweight"].Value);
            newArmor.ConWeight = int.Parse(c.Attributes["conweight"].Value);
            newArmor.IntWeight = int.Parse(c.Attributes["intweight"].Value);
            newArmor.WisWeight = int.Parse(c.Attributes["wisweight"].Value);
            newArmor.baseDesc = c.Attributes["desc"].Value;
            newArmor.Hardness = int.Parse(c.Attributes["hard"].Value);
            ItemGenerationData.AddNewArmorTemplate(newArmor);
        }

        XmlNodeList tiers = equipmentDoc.SelectNodes("root/tiers/tier");
        foreach(XmlNode t in tiers)
        {
            EquipmentTier newTier = new EquipmentTier();
            newTier.DistPtsAvg = int.Parse(t.Attributes["dist_pts_avg"].Value);
            newTier.DistPtsLower = int.Parse(t.Attributes["dist_pts_lower"].Value);
            newTier.DistPtsUpper = int.Parse(t.Attributes["dist_pts_upper"].Value);
            newTier.TierMod = float.Parse(t.Attributes["tier_mod"].Value);
            ItemGenerationData.AddNewTier(newTier);
        }
    }

    private EquipmentTemplate GenerateEquipment(XmlNode given)
    {
        EquipmentTemplate newTemlpate = new EquipmentTemplate();
        newTemlpate.BaseName = given.Attributes[0].Value;
        newTemlpate.StrWeight = int.Parse(given.Attributes["strweight"].Value);
        newTemlpate.DexWeight = int.Parse(given.Attributes["dexweight"].Value);
        newTemlpate.ConWeight = int.Parse(given.Attributes["conweight"].Value);
        newTemlpate.IntWeight = int.Parse(given.Attributes["intweight"].Value);
        newTemlpate.WisWeight = int.Parse(given.Attributes["wisweight"].Value);
        newTemlpate.baseDesc = given.Attributes["desc"].Value;
        return newTemlpate;
    }

    private void ReadTemplates()
    {
        XmlDocument templateDoc = CreateXML("ClassTemplate.xml");

        PlayerGenerator.STANDARD_ARRAY = int.Parse(templateDoc.SelectSingleNode("root/array").Attributes[0].Value);
        PlayerGenerator.DIST_POINTS = int.Parse(templateDoc.SelectSingleNode("root/pts").Attributes[0].Value);
        PlayerGenerator.STAT_CAP_ON_GENERATION = int.Parse(templateDoc.SelectSingleNode("root/gencap").Attributes[0].Value);

        XmlNodeList allTemplates = templateDoc.SelectNodes("root/template");

        foreach(XmlNode template in allTemplates)
        {
            Template new_t = new Template();
            new_t.TemplateIdentifier = template.Attributes[0].Value;
            XmlNodeList sets = template.SelectNodes("set");
            foreach(XmlNode set in sets)
            {

                XmlNodeList abilities = set.SelectNodes("ability");
                TemplateSet new_template_set = new TemplateSet();
                new_template_set.SetIdentifier = set.Attributes[0].Value;
                XmlNode weights = set.SelectSingleNode("weights");
                new_template_set.StrWeight = int.Parse(weights.Attributes[0].Value);
                new_template_set.DexWeight = int.Parse(weights.Attributes[1].Value);
                new_template_set.VitWeight = int.Parse(weights.Attributes[2].Value);
                new_template_set.IntWeight = int.Parse(weights.Attributes[3].Value);
                new_template_set.WisWeight = int.Parse(weights.Attributes[4].Value);
                foreach (XmlNode ability in abilities)
                {
                    int finder = int.Parse(ability.Attributes[0].Value);
                    new_template_set.AddAbility(finder);
                }
                new_t.AddSet(new_template_set);
            }

            TemplateDataHolder.AddTemplate(new_t);
        }
    }
}
