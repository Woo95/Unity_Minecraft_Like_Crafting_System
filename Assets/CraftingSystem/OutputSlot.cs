using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OutputSlot : ItemSlot
{
	public void CreateOutputItem(Item outputItem, ItemData outputItemData)
	{
		m_Item = outputItem;
		m_ItemData = Instantiate(outputItemData);

		m_ItemData.transform.SetParent(transform);
		m_ItemData.SetItem(m_Item);

		m_ItemData.GetComponent<Image>().raycastTarget = true;
		m_ItemData.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		m_ItemData.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		m_ItemData.Count = 1;
	}

	public void DestroyCrafting()
	{
		if (m_ItemData != null)
			Destroy(m_ItemData.gameObject);
	}
}
