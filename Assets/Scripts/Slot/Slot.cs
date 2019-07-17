using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public GameObject ItemUIPrefab;
    public ItemUI ItemUI;
    private bool hasItem = false;   // 辅助、补充


    public void StoreItem(Item item, int amount = 1) {
        if (transform.childCount == 0) {
            GameObject go = Instantiate(ItemUIPrefab, transform);

            ItemUI = go.GetComponent<ItemUI>();
            ItemUI.SetItem(item, amount);
            hasItem = true;         // hasItem
        }
        else if (ItemUI.Item.ID == item.ID) {
            ItemUI.AddAmount(amount);           /// 未检查容量   默认不写参数 amount
        }
    }

    public bool IsFilled() {
        return HasItem() ? ItemUI.Amount >= ItemUI.Item.Capacity : false;
    }

    public int GetItemID() {
        return HasItem() ? ItemUI.Item.ID : -1;
    }

    public int GetItemAmount() {
        return HasItem() ? ItemUI.Amount : -1;
    }

    public bool HasItem() {
        return transform.childCount > 0 && hasItem;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (HasItem()) {
            string text = ItemUI.Item.GetToolTipText();
            InventoryManager.Instance.ShowToolTip(text);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (HasItem()) {
            InventoryManager.Instance.HideToolTip();
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData) {

        if (eventData.button == PointerEventData.InputButton.Right) {
            if (InventoryManager.Instance.IsPicked || !HasItem()) return;

            Item item = ItemUI.Item;
            if(item is Weapon ||item is Equipment) {
                DestroyImmediate(ItemUI.gameObject);    // 立即销毁，可解决延迟问题     // 先销毁再 PutOn
                CharacterPanel.Instance.PutOn(item);
            }
        }

        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Item pickedItem = null;
        int amount = 0;
        if (InventoryManager.Instance.IsPicked) {
            pickedItem = InventoryManager.Instance.PickedItemUI.Item;
            amount = InventoryManager.Instance.PickedItemUI.Amount;
        }

        /*
            自身不是空
                1. IsPicked == true
                    自身的id == pickedItem.id  放置当前鼠标上的物品
                    自身的id != pickedItem.id  交换
                2. IsPicked == false
                    把当前物品放到PickedItem
            自身是空
                1. IsPicked == true  pickedItem放在这个位置
                2. IsPicked == false 不处理
        */

        if (HasItem()) {
            if (InventoryManager.Instance.IsPicked) {
                if (ItemUI.Item.ID == pickedItem.ID) {
                    int ctrlAmount = Input.GetKey(KeyCode.LeftControl) ? (amount + 1) / 2 : amount;

                    int leftSpace = pickedItem.Capacity - ItemUI.Amount - ctrlAmount;       // 全放下后Slot剩余的容量
                    int takeAmount = leftSpace >= 0 ? ctrlAmount : ctrlAmount + leftSpace;  // 最大能放下的数量
                    StoreItem(pickedItem, takeAmount);
                    InventoryManager.Instance.GiveUpItem(takeAmount);
                }
                else {
                    InventoryManager.Instance.PickedItemUI.Exchange(ItemUI);
                }
            }
            else {
                int takeAmount = Input.GetKey(KeyCode.LeftControl) ? (ItemUI.Amount + 1) / 2 : ItemUI.Amount;
                InventoryManager.Instance.PickUpItem(ItemUI.Item, takeAmount);
                if (takeAmount >= ItemUI.Amount)
                    Destroy(ItemUI.gameObject);
                ItemUI.SubAmount(takeAmount);
            }
        }
        else {
            if (InventoryManager.Instance.IsPicked) {
                int takeAmount = Input.GetKey(KeyCode.LeftControl) ? (amount + 1) / 2 : amount;
                StoreItem(pickedItem, takeAmount);
                InventoryManager.Instance.GiveUpItem(takeAmount);
            }
        }

    }

    public void DestroyItemUI() {
        if (ItemUI != null) {
            hasItem = false;
            Destroy(ItemUI.gameObject);
        }
    }

}
