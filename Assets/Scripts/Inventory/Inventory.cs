using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    protected List<Slot> SlotList;

    private CanvasGroup canvasGroup;
    private float targetAlpha = 1f;
    private float alphaSpeed = 0.25f;


    protected virtual void Start() {
        SlotList = new List<Slot>(GetComponentsInChildren<Slot>());
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (canvasGroup.alpha != targetAlpha) {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, alphaSpeed);
            if (Mathf.Abs(canvasGroup.alpha - targetAlpha) <= 0.001f) {
                canvasGroup.alpha = targetAlpha;
            }
        }
    }


    public bool StoreItem(int ID) {
        Item item = InventoryManager.Instance.GetItemByID(ID);
        return StoreItem(item);
    }

    public bool StoreItem(Item item) {
        if (item == null) {
            return false;   // 找不到该 ID 的 Item
        }

        if (item.Capacity == 1) {
            Slot slot = FindEmptySlot();
            if (slot == null)
                return false;   // 找不到空 Slot
            slot.StoreItem(item);
        }
        else {
            Slot slot = FindSameIDSlot(item);
            if (slot != null) {
                slot.StoreItem(item);
            }
            else {
                slot = FindEmptySlot();
                if (slot == null)
                    return false;   // 找不到空 Slot
                slot.StoreItem(item);
            }
        }
        return true;
    }


    private Slot FindEmptySlot() {
        foreach (Slot slot in SlotList) {
            if (!slot.HasItem())
                return slot;
        }
        return null;
    }

    private Slot FindSameIDSlot(Item item) {
        foreach (Slot slot in SlotList) {
            if (slot.HasItem() && slot.GetItemID() == item.ID && !slot.IsFilled())
                return slot;
        }
        return null;
    }
    

    public void Hide() {
        targetAlpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show() {
        targetAlpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void DisplaySwitch() {
        if (targetAlpha == 0f)
            Show();
        else
            Hide();
    }


    public void Load() {    
        string text = "";
        foreach (Slot slot in SlotList) {
            if (slot.HasItem()) {
                text += slot.GetItemID().ToString() + "=" + slot.GetItemAmount().ToString() + "-";
            }
            else {
                text += "0-";
            }
        }
        PlayerPrefs.SetString(this.gameObject.name, text);
    }

    public void Save() {
        if (!PlayerPrefs.HasKey(this.gameObject.name))
            return;

        string text = PlayerPrefs.GetString(this.gameObject.name);
        string[] slots = text.Split('-');

        for(int i = 0; i < slots.Length - 1; i++) {
            if (slots[i] != "0") {
                string[] id_amount = slots[i].Split('=');

                int ID = int.Parse(id_amount[0]);
                int amount = int.Parse(id_amount[1]);
                Item item = InventoryManager.Instance.GetItemByID(ID);

                SlotList[i].StoreItem(item, amount);
            }
        }
    }


}
