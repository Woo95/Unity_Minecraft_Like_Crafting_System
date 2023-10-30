using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingInputSlot : MonoBehaviour, IPointerClickHandler
{
	public Crafting crafting;
	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(CO_CraftInputHandler());
	}

	IEnumerator CO_CraftInputHandler()	// to make sure ItemSlot ClickHandler runs first
	{
		yield return null;
		crafting.InteractInputPanel();
	}

	//public void UpdatePannel(List<ItemSlot> craftInputList)
	//{
	//	foreach (ItemSlot item in craftInputList)
	//	{
	//		ItemData itemData = item.GetItemData();
	//		if (itemData != null)
	//		{
	//			if (itemData.Count <= 1)
	//				Destroy(itemData.gameObject);
	//			else
	//				itemData.Count -= 1;
	//		}
	//	}
	//}
}
