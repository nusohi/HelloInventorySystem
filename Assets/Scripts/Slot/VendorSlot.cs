using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VendorSlot : Slot
{
    public override void OnPointerDown(PointerEventData eventData) {
        // 左键卖，右键买
        if (eventData.button == PointerEventData.InputButton.Left && InventoryManager.Instance.IsPicked == true) {
            transform.parent.parent.SendMessage("SellItem");
        }
        else if(eventData.button == PointerEventData.InputButton.Right && InventoryManager.Instance.IsPicked == false) {
            if (HasItem()) {
                transform.parent.parent.SendMessage("BuyItem", ItemUI.Item);
            }
        }
    }
}
