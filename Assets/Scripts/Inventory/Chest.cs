using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Inventory
{
    public static Chest Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }
}
