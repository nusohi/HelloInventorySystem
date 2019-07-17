using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : Inventory
{
    private Text propertyText;

    public static CharacterPanel Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    protected override void Start() {
        base.Start();       /// 坑
        propertyText = transform.Find("PropertyText").GetComponent<Text>();
        UpdateProperty();
    }


    public void PutOn(Item item) {
        Item preItem = null;
        foreach (EquipmentSlot slot in SlotList) {
            // 位置正确
            if (slot.IsRightItem(item)) {
                if (slot.HasItem()) {
                    // 原位置有装备
                    preItem = slot.ItemUI.Item;
                    slot.ItemUI.SetItem(item);  // 数量一定为 1
                }
                else {
                    // 原位置没有装备
                    slot.StoreItem(item);       // 数量一定为 1
                }
                break;
            }
        }

        if (preItem != null) {
            Chest.Instance.StoreItem(preItem);  /// 暂时为 Chest (应该为 Knapsack)
        }
        UpdateProperty();
    }

    public void PutOff(Item item) {
        Chest.Instance.StoreItem(item);         /// 暂时为 Chest (应该为 Knapsack)
        UpdateProperty();
    }

    public void UpdateProperty() {
        int strength = 0, intellect = 0, agility = 0, stamina = 0, damage = 0;

        foreach (EquipmentSlot slot in SlotList) {
            if (!slot.HasItem()) continue;
            // Debug.Log(slot.gameObject.name);

            Item item = slot.ItemUI.Item;
            if(item is Equipment) {
                Equipment equip = (Equipment)item;
                strength += equip.Strength;
                intellect += equip.Intellect;
                agility += equip.Agility;
                stamina += equip.Stamina;
            }
            else if(item is Weapon) {
                damage += ((Weapon)item).Damage;
            }
        }

        string text = string.Format("力量：{0}\n智力：{1}\n敏捷：{2}\n体力：{3}\n攻击力：{4} ", strength, intellect, agility, stamina, damage);
        propertyText.text = text;
    }

}
