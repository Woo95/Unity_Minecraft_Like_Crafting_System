using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemCursor : MonoBehaviour
{
	#region singletone
	public static ItemCursor instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	private ItemData m_ItemData;
	private Item m_Item;

	private void Update()
	{
		if (m_ItemData != null)
		{
			m_ItemData.transform.position = Input.mousePosition;
		}
	}

	public Item GetCursorItem()
	{
		return m_Item;
	}

	public ItemData GetCursorItemData()
	{
		return m_ItemData;
	}

	public void SetCursorItemData(ItemData itemData, Item item)
	{
		m_Item = item;
		m_ItemData = itemData;
		m_ItemData.transform.SetParent(transform);
		m_ItemData.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
	}

	public bool ItemCursorExist()
	{
		return m_ItemData != null;
	}

	public void ClearCursor(out ItemData itemDataOut, out Item itemOut)
	{
		itemOut = m_Item;
		m_Item = null;

		itemDataOut = m_ItemData;
		m_ItemData = null;
	}
}
