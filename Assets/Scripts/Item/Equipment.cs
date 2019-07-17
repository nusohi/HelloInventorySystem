using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public int Strength;    // 力量
    public int Intellect;   // 智力
    public int Agility;     // 敏捷
    public int Stamina;     // 体力

    public EquipmentType EquipmentType;

    public Equipment(int id, string name, ItemType type, string des, int capacity, float buyPrice, float sellPrice, string sprite,
        int strength, int intellect, int agility, int stamina, EquipmentType equipmentType)
        : base(id, name, type, des, capacity, buyPrice, sellPrice, sprite) {
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipmentType = equipmentType;
    }

    public override string GetToolTipText() {
        string text = base.GetToolTipText();

        string equipTypeText = "";
        switch (EquipmentType) {
            case EquipmentType.Head:
                equipTypeText = "头部";
                break;
            case EquipmentType.Neck:
                equipTypeText = "脖子";
                break;
            case EquipmentType.Chest:
                equipTypeText = "胸部";
                break;
            case EquipmentType.Ring:
                equipTypeText = "戒指";
                break;
            case EquipmentType.Leg:
                equipTypeText = "腿部";
                break;
            case EquipmentType.Bracer:
                equipTypeText = "护腕";
                break;
            case EquipmentType.Boots:
                equipTypeText = "靴子";
                break;
            case EquipmentType.Shoulder:
                equipTypeText = "护肩";
                break;
            case EquipmentType.Belt:
                equipTypeText = "腰带";
                break;
            case EquipmentType.OffHand:
                equipTypeText = "副手";
                break;
        }

        return string.Format("{0}\n\n<color=blue>装备类型：{1}\n力量：{2}\n智力：{3}\n敏捷：{4}\n体力：{5}</color>", text, equipTypeText, Strength, Intellect, Agility, Stamina);
    }

}

public enum EquipmentType
{
    None,
    Head,
    Neck,
    Chest,
    Ring,
    Leg,
    Bracer,
    Boots,
    Shoulder,
    Belt,
    OffHand
}