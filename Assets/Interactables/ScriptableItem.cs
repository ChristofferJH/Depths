using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scrItem", menuName = "Scriptable Item")]
public class ScriptableItem : ScriptableObject
{
    public enum ItemType { test};

    public ItemType itemType;

    public GameObject ObjectPrefab;
    public GameObject ItemPrefab;

    public enum HeldType { overHead, rightHand};

    public HeldType heldType;
}
