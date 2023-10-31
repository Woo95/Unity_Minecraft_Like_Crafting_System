using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType
{
    XX, // empty
    A1, A2, A3, B1,
    C1, C2, C3, C4,
    C5, C6, F1, O1,
    P1, P2, S1, S2,
    T1, Y1
}
//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
public class Item : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
{
    public Sprite icon;
    [TextArea]
    public string description = "";
    public bool isConsumable = false;
    public eItemType itemType;

    public void Use()
    {
        Debug.Log("This is the Use() function of item: " + name + " - " + description);
    }
}