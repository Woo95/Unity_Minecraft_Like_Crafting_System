using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Update()
	{
		if (m_ItemData != null)
		{
			m_ItemData.transform.position = Input.mousePosition;
		}
	}

	public void SetClickItemData(ItemData itemData)
	{
		m_ItemData = itemData;
	}
}
