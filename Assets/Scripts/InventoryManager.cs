using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }


    public bool IsPicked { get; private set; }
    public ItemUI PickedItemUI { get; private set; }
    
    private List<Item> itemList;
    private bool isToolTipShow = false;
    private ToolTip toolTip;
    private Canvas canvas;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        LoadItems();
    }

    private void Start() {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        toolTip = canvas.transform.Find("ToolTip").GetComponent<ToolTip>();

        IsPicked = false;
        PickedItemUI = canvas.transform.Find("PickedItem").GetComponent<ItemUI>();
        PickedItemUI.Hide();
    }

    private void Update() {
        if (IsPicked) {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, 
                Input.mousePosition, null, out pos);
            PickedItemUI.SetPos(pos);
        }
        else if (isToolTipShow) {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out pos);
            toolTip.SetPos(pos);
        }
    }


    public void PickUpItem(Item item, int amount = 1) {
        IsPicked = true;
        PickedItemUI.SetItem(item, amount);
        PickedItemUI.Show();
        toolTip.Hide();
    }

    public void GiveUpItem(int amount = 1) {
        PickedItemUI.SubAmount(amount);
        if (PickedItemUI.Amount <= 0) {
            IsPicked = false;
            PickedItemUI.Hide();
        }
    }

    public Item GetItemByID(int ID) {
        foreach (Item item in itemList) {
            if (item.ID == ID)
                return item;
        }
        return null;
    }
    

    public void ShowToolTip(string text) {
        if (IsPicked)
            return;
        toolTip.Show(text);
        isToolTipShow = true;
    }

    public void HideToolTip() {
        toolTip.Hide();
        isToolTipShow = false;
    }


    // 加载 Json 中的 Items
    private void LoadItems() {
        string rawJsonText = Resources.Load<TextAsset>("Items").text;
        itemList = new List<Item>();

        JsonData items = JsonMapper.ToObject(rawJsonText);
        foreach (JsonData item in items) {
            int ID = int.Parse(item["id"].ToString());
            string name = item["name"].ToString();
            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), item["type"].ToString());
            int capacity = int.Parse(item["capacity"].ToString());
            float buyPrice = float.Parse(item["buyPrice"].ToString());
            float sellPrice = float.Parse(item["sellPrice"].ToString());
            string description = item["description"].ToString();
            string sprite = item["sprite"].ToString();

            Item newItem = null;
            switch (type) {
                case ItemType.Consumable:
                    int hp = int.Parse(item["hp"].ToString());
                    int mp = int.Parse(item["mp"].ToString());
                    newItem = new Consumable(ID, name, type, description, capacity, buyPrice, sellPrice, sprite, hp, mp);
                    break;
                case ItemType.Equipment:
                    int strength = int.Parse(item["strength"].ToString());
                    int intellect = int.Parse(item["intellect"].ToString());
                    int agility = int.Parse(item["agility"].ToString());
                    int stamina = int.Parse(item["stamina"].ToString());
                    EquipmentType equipType = (EquipmentType)System.Enum.Parse(typeof(EquipmentType), item["equipType"].ToString());
                    newItem = new Equipment(ID, name, type, description, capacity, buyPrice, sellPrice, sprite, strength, intellect, agility, stamina, equipType);
                    break;
                case ItemType.Weapon:
                    int damage = int.Parse(item["damage"].ToString());
                    WeaponType weaponType = (WeaponType)System.Enum.Parse(typeof(WeaponType), item["weaponType"].ToString());
                    newItem = new Weapon(ID, name, type, description, capacity, buyPrice, sellPrice, sprite, damage, weaponType);
                    break;
                case ItemType.Material:
                    newItem = new Material(ID, name, type, description, capacity, buyPrice, sellPrice, sprite);
                    break;
            }
            if (newItem != null)
                itemList.Add(newItem);
        }
    }

    public void LoadInventory() {
        // ...
        Chest.Instance.Load();
    }

    public void SaveInventory() {
        // ...
        Chest.Instance.Save();
    }

}
