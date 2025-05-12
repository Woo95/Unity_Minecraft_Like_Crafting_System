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

	private ItemInstance m_ItemData;
	private ItemInfo m_Item;

	private void Update()
	{
		if (m_ItemData != null)
		{
			m_ItemData.transform.position = Input.mousePosition;
		}
	}

	public ItemInfo GetCursorItem()
	{
		return m_Item;
	}

	public ItemInstance GetCursorItemData()
	{
		return m_ItemData;
	}

	public void SetCursorItemData(ItemInstance itemData, ItemInfo item)
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

	public void ClearCursor(out ItemInstance itemDataOut, out ItemInfo itemOut)
	{
		itemOut = m_Item;
		m_Item = null;

		itemDataOut = m_ItemData;
		m_ItemData = null;
	}
}
