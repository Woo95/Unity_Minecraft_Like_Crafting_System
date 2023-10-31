using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType   /* !WARNING! - When generating Recipe Code, it exclude some of the X chars. So, Don't use any X char for Itemtypes!!! - !WARNING! */
{
    XX, // empty
    B1, C1, C2, C3, 
    C4, C5, C6, F1, 
    G1, G2, G3, O1,
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

    //public void Use()
    //{
    //    Debug.Log("This is the Use() function of item: " + name + " - " + description);
    //}
}