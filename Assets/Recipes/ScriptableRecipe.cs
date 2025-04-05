using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "scrRecipe", menuName = "Scriptable Recipe")]

public class ScriptableRecipe : ScriptableObject
{

    public List<ScriptableItem.ItemType> Inputs;
    public ScriptableItem Output;
    public float workTime = 10f;
}
