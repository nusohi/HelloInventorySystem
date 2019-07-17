using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material : Item
{
    public Material(int id, string name, ItemType type, string des, int capacity, float buyPrice, float sellPrice, string sprite)
        : base(id, name, type, des, capacity, buyPrice, sellPrice, sprite) {
    }
}
