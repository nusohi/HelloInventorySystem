using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public ToolTip ToolTip;
    public string ToolTipText;

    private Button button;

    private void Start() {
        if (ToolTip == null) {
            ToolTip = GameObject.Find("ToolTip").GetComponent<ToolTip>();
        }

        button = GetComponent<Button>();
        //button.onClick.AddListener(ShowToolTip);
        //button.onClick.AddListener(ShowChest);
        button.onClick.AddListener(GetRandomItem);
    }

    public void ShowToolTip() {
        Debug.Log("TEST - Show Tool Tip Test!" + " (test: '" + ToolTipText + "' )");
        ToolTip.Show(ToolTipText);
    }

    public void ShowChest() {
        Chest.Instance.DisplaySwitch();
    }

    public void GetRandomItem() {
        int ID = Random.Range(1, 18);
        Chest.Instance.StoreItem(ID);
    }
}
