using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public int Damage;
    public WeaponType WeaponType;
    
    public Weapon(int id, string name, ItemType type, string des, int capacity, float buyPrice, float sellPrice, string sprite,
       int damage, WeaponType weaponType)
        : base(id, name, type, des, capacity, buyPrice, sellPrice, sprite) {
        this.Damage = damage;
        this.WeaponType = weaponType;
    }

    public override string GetToolTipText() {
        string baseText = base.GetToolTipText();
        string weaponTypeText = "";

        switch (WeaponType) {
            case WeaponType.OffHand:
                weaponTypeText = "副手";
                break;
            case WeaponType.MainHand:
                weaponTypeText = "主手";
                break;
        }

        return string.Format("{0}\n\n<color=blue>武器类型：{1}\n攻击力：{2}</color>", baseText, weaponTypeText, Damage);
    }

}


public enum WeaponType
{
    None,
    OffHand,
    MainHand
}