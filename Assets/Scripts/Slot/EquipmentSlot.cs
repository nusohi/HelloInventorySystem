using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : Slot
{
    public WeaponType WeaponType;
    public EquipmentType EquipmentType;

    public bool IsRightItem(Item item) {
        return (item is Weapon && ((Weapon)item).WeaponType == WeaponType)
            || (item is Equipment && ((Equipment)item).EquipmentType == EquipmentType);
    }

    public override void OnPointerDown(PointerEventData eventData) {
        bool needUpdate = false;    // 是否刷新 CharacterPanel 上的属性值

        // 右键脱下
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (InventoryManager.Instance.IsPicked || !HasItem()) return;

            DestroyItemUI();        // 解决延迟问题   // Destroy(transform.GetChild(0).gameObject);
            transform.parent.parent.SendMessage("PutOff", ItemUI.Item);    // CharacterPanel
            InventoryManager.Instance.HideToolTip();
            needUpdate = true;
        }

        // 左键
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        /*
        *   手上有东西
        *       当前装备槽 有装备 -> 交换（IsRightItem()）
        *       当前装备槽 无装备 -> 放下（IsRightItem()）
        *   手上没东西
        *       当前装备槽 有装备 -> 拿手上
        */

        if (InventoryManager.Instance.IsPicked) {
            if (HasItem()) {
                // IsRightItem 则交换
                if (IsRightItem(InventoryManager.Instance.PickedItemUI.Item)) {
                    InventoryManager.Instance.PickedItemUI.Exchange(ItemUI);
                    needUpdate = true;
                }
            }
            else {
                // IsRightItem 则放下
                if (IsRightItem(InventoryManager.Instance.PickedItemUI.Item)) {
                    StoreItem(InventoryManager.Instance.PickedItemUI.Item);     // 数量一定为 1
                    InventoryManager.Instance.GiveUpItem();
                    needUpdate = true;
                }
            }
        }
        else {
            if (HasItem()) {
                // 拿手上
                InventoryManager.Instance.PickUpItem(ItemUI.Item);     // 数量一定为 1
                DestroyItemUI();        // 解决延迟问题   // Destroy(transform.GetChild(0).gameObject);
                needUpdate = true;
            }
        }

        // 刷新 CharacterPanel 上的属性值
        if (needUpdate) {
            transform.parent.parent.SendMessage("UpdateProperty");
        }

    }


}
