using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text MoneyText;
    private float money = 100;
    public float Money
    {
        get { return money; }
        set
        {
            money = value;
            if (MoneyText != null)
                MoneyText.text = money.ToString();
        }
    }


    public void AddMoney(float amount) {
        Money += amount;
    }

    public bool SubMoney(float amount) {
        if (Money >= amount) {
            Money -= amount;
            return true;
        }
        else {
            return false;
        }
    }
}
