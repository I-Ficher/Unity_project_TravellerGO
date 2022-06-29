using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New item",menuName ="Item/Create New Item")]
public class Item:ScriptableObject {
    public int id;
    public string Name;
    public int value;
    public Sprite icon;
    public ItemType itemType;

    public enum ItemType
    {
        Sphere,
        Power
    }

}
