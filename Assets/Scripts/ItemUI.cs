using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private Text amountText;
    private Image itemImage;


    private int amount;
    public int Amount
    {
        get { return amount; }
        private set
        {
            amount = value;
            if (Item.Capacity > 1)
                amountText.text = amount.ToString();
            else
                amountText.text = "";
        }
    }
    private Item item;
    public Item Item
    {
        get { return item; }
        private set {
            item = value;
            itemImage.sprite = Resources.Load<Sprite>(item.Sprite);
        }
    }


    private void Awake() {
        amountText = GetComponentInChildren<Text>();
        itemImage = GetComponent<Image>();
    }

    private void Start() {
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
    }

    public void SetItem(Item item, int amount = 1) {
        this.Item = item;
        this.Amount = amount;
    }

    public void AddAmount(int amount = 1) {
        this.Amount += amount;
    }

    public void SubAmount(int amount) {
        this.Amount -= amount;
    }

    public void SetAmount(int amount) {
        this.Amount = amount;
    }

    public void Exchange(ItemUI another) {
        Item tempItem = Item;
        int tempAmount = Amount;

        Item = another.Item;
        Amount = another.Amount;

        another.Item = tempItem;
        another.Amount = tempAmount;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void SetPos(Vector3 pos) {
        transform.localPosition = pos;
    }

}
