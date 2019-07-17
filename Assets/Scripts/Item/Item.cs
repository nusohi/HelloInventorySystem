using System.Collections;
using System.Collections.Generic;

public class Item
{
    public int ID;
    public string Name;
    public ItemType Type;
    public int Capacity;
    public float BuyPrice;
    public float SellPrice;
    public string Description;
    public string Sprite;

    public Item(int id, string name, ItemType type, string des, int capacity, float buyPrice, float sellPrice, string sprite) {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Description = des; 
        this.Capacity = capacity;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
        this.Sprite = sprite;
    }

    public virtual string GetToolTipText() {
        string text = "";
        text += "{0}\n";
        text += "购买价格：{1}\n";
        text += "销售价格：{2}\n";
        text += "描述：{3}\n";
        text = string.Format(text, Name, BuyPrice, SellPrice, Description);
        return text;
    }

}

public enum ItemType
{
    Consumable,
    Equipment,
    Weapon,
    Material
}
