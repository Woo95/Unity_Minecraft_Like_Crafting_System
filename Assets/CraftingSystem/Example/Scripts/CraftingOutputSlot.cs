using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingOutputSlot : ItemSlot, IPointerClickHandler
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

		ItemData outputPrefab = OutputItemData();
		if (outputPrefab == null) // error checker
			return;

		m_Item = foundRecipe.output;
		m_ItemData = Instantiate(outputPrefab);
		m_ItemData.SetItem(m_Item);
		m_ItemData.gameObject.name = m_Item.name;
		m_ItemData.transform.SetParent(transform);

		m_ItemData.GetComponent<Image>().raycastTarget = true;
		m_ItemData.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		m_ItemData.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		m_ItemData.Count = MinInputItemAmount() * foundRecipe.outputAmount;
	}

	int MinInputItemAmount()
	{
		int outputItemAmount = int.MaxValue;
		foreach (ItemSlot item in m_CraftInputList)
		{
			ItemData itemData = item.GetItemData();
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

	ItemData OutputItemData()
	{
		ItemData outputItemData = null;
		foreach (ItemSlot item in m_CraftInputList)
		{
			ItemData itemData = item.GetItemData();
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
			ItemData itemData = itemSlot.GetItemData();
			
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
