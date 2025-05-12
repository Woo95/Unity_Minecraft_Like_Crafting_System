using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingOutputSlot : ItemSlot
{
	private List<ItemSlot> m_CraftInputList;
	public override void OnPointerClick(PointerEventData eventData)
	{
		if (SlotAndCursor())
		{
			if (SlotAndCursorSameItem())
			{
				StackAllItemSlotToCursor();
				UpdateInputPanel();
			}
		}
		else if (SlotAndNoCursor())
        {
			PickItem();
			UpdateInputPanel();
		}
	}

	#region OutputItem Handler
	public void CreateOutputItem(Recipe foundRecipe, List<ItemSlot> craftInputList)
	{
		m_CraftInputList = craftInputList;

		ItemInstance outputPrefab = OutputItemData();
		if (outputPrefab == null) // error checker
			return;

		m_Item = foundRecipe.output;
		m_ItemInstance = Instantiate(outputPrefab);
		m_ItemInstance.SetItem(m_Item);
		m_ItemInstance.gameObject.name = m_Item.name;
		m_ItemInstance.transform.SetParent(transform);

		m_ItemInstance.GetComponent<Image>().raycastTarget = true;
		m_ItemInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		m_ItemInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		m_ItemInstance.Count = MinInputItemAmount() * foundRecipe.outputAmount;
	}

	int MinInputItemAmount()
	{
		int outputItemAmount = int.MaxValue;
		foreach (ItemSlot item in m_CraftInputList)
		{
			ItemInstance itemData = item.GetItemData();
			if (itemData != null)
			{
				if (itemData.Count <= outputItemAmount)
				{
					outputItemAmount = itemData.Count;
				}
			}
		}
		return outputItemAmount;
	}

	ItemInstance OutputItemData()
	{
		ItemInstance outputItemData = null;
		foreach (ItemSlot item in m_CraftInputList)
		{
			ItemInstance itemData = item.GetItemData();
			if (itemData != null)
			{
				outputItemData = itemData;
				break;
			}
		}
		return outputItemData;
	}
	
	#endregion

	#region InputPanel Handler
	void UpdateInputPanel()
	{
		int lowestItemCount = MinInputItemAmount();
		foreach (ItemSlot itemSlot in m_CraftInputList)
		{
			ItemInstance itemData = itemSlot.GetItemData();
			
			if (itemData != null)
			{
				if (itemData.Count <= lowestItemCount)
				{
					itemSlot.m_Item = null;
					Destroy(itemData.gameObject);
				}
				else
					itemData.Count -= lowestItemCount;
			}
		}
	}
	#endregion
}
