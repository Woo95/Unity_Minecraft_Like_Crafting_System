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

	public ItemData m_ItemData;
	public Item m_Item;

	private void Update()
	{
		if (m_ItemData != null)
		{
			m_ItemData.transform.position = Input.mousePosition;
		}
	}

	public void SetClickItemData(ItemData itemData, Item item)
	{
		m_Item = item;
		m_ItemData = itemData;
		m_ItemData.transform.SetParent(transform);
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
