using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int HP;
    public int MP;

    public Consumable(int id, string name, ItemType type, string des, int capacity, float buyPrice, float sellPrice, string sprite, int hp, int mp)
        : base(id, name, type, des, capacity, buyPrice, sellPrice, sprite) {
        HP = hp;
        MP = mp;
    }

    public override string GetToolTipText() {
        return string.Format("{0}\n\n<color=blue>加血：{1}\n加蓝：{2}</color>", base.GetToolTipText(), HP, MP);
    }
}
