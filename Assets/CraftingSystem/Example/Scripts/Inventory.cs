using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region singletone
	public static Inventory instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    GameObject inventoryPanel;

	[SerializeField]
	private TMPro.TextMeshProUGUI descriptionText;
	[SerializeField]
	private TMPro.TextMeshProUGUI nameText;

	void Start()
    {
        //Read all itemSlots as children of inventory panel
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
    }

    public void DisplayMessage(Item item = null)
    {
        if (item)
        {
            descriptionText.text = item.description;
            nameText.text = item.name;
        }
        else
        {
			descriptionText.text = "";
			nameText.text = "";
		}
    }
}
