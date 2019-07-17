using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : Inventory
{
    public static Vendor Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }


    public int[] ItemIDList;
    private Player player;


    protected override void Start() {
        base.Start();
        InitVendor();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    private void InitVendor() {
        foreach(int ID in ItemIDList) {
            StoreItem(ID);
        }
    }

    // Player 卖东西
    public void SellItem() {
        int amount = InventoryManager.Instance.PickedItemUI.Amount;
        int takeAmount = Input.GetKey(KeyCode.LeftControl) ? 1 : amount;
        float money = takeAmount * InventoryManager.Instance.PickedItemUI.Item.SellPrice;

        InventoryManager.Instance.GiveUpItem(takeAmount);
        player.AddMoney(money);
    }

    // Player 买东西
    public void BuyItem(Item item) {
        if (player.SubMoney(item.BuyPrice)) {
            Chest.Instance.StoreItem(item);
        }
    }
}
