using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingOutputSlot : ItemSlot, IPointerClickHandler
{
	public List<ItemSlot> m_CraftInputList;
	public override void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log("Baby");

		if (SlotAndCursor())
		{
			if (SlotAndCursorSameItem())
			{
				StackAllItemSlotToCursor();
				UpdateInputPanel(m_CraftInputList);
			}
		}
		else if (SlotAndNoCursor())
        {
			PickItem();
			UpdateInputPanel(m_CraftInputList);
		}
	}

	public void CreateOutputItem(Item outputItem, List<ItemSlot> craftInputList)
	{
		ItemData outputItemData = null;
		foreach (ItemSlot item in craftInputList)
		{
			if (item.GetItemData() != null)
			{
				outputItemData = item.GetItemData();
				break;
			}
		}
		if (outputItemData == null) // error checker
			return;

		m_Item = outputItem;
		m_ItemData = Instantiate(outputItemData);

		m_ItemData.transform.SetParent(transform);
		m_ItemData.SetItem(m_Item);

		m_ItemData.GetComponent<Image>().raycastTarget = true;
		m_ItemData.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		m_ItemData.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		m_ItemData.Count = 1;

		m_CraftInputList = craftInputList;
	}

	public void StackAllItemSlotToCursor()
	{
		ItemData cursorItemData = ItemCursor.instance.GetCursorItemData();

		cursorItemData.Count += m_ItemData.Count;

		DestroyItem();
	}

	public void DestroyItem()
	{
		if (m_ItemData != null)
			Destroy(m_ItemData.gameObject);
	}

	public void UpdateInputPanel(List<ItemSlot> craftInputList)
	{
		foreach (ItemSlot item in craftInputList)
		{
			ItemData itemData = item.GetItemData();
			if (itemData != null)
			{
				if (itemData.Count <= 1)
					Destroy(itemData.gameObject);
				else
					itemData.Count -= 1;
			}
		}
	}
}
