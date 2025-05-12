using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemInstance : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	ItemInfo m_ItemInfo = null;

	[SerializeField]
	int count = 0;
	public int Count
	{
		get { return count; }
		set
		{
			count = value;
			UpdateGraphic();
		}
	}

	Image itemIcon;
	[SerializeField] 
	TextMeshProUGUI itemCountText;

	public void SetItem(ItemInfo item)
	{
		m_ItemInfo = item;
		itemIcon = GetComponent<Image>();
		gameObject.name = item.name;
		count = count == 0 ? 1 : count;

		UpdateGraphic();
	}

	//Change Icon and count
	void UpdateGraphic()
	{
		if (count < 1)
		{
			//item = null;
			itemIcon.gameObject.SetActive(false);
			itemCountText.gameObject.SetActive(false);
		}
		else
		{
			//set sprite to the one from the item
			itemIcon.sprite = m_ItemInfo.icon;
			itemIcon.gameObject.SetActive(true);
			itemCountText.gameObject.SetActive(true);
			itemCountText.text = count.ToString();
		}
	}

	//public void UseItemInSlot()
	//{
	//	if (CanUseItem())
	//	{
	//		m_Item.Use();
	//		if (m_Item.isConsumable)
	//		{
	//			Count--;
	//		}
	//	}
	//}

	//private bool CanUseItem()
	//{
	//	return (m_Item != null && count > 0);
	//}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (m_ItemInfo != null)
		{
			Inventory.instance.DisplayMessage(m_ItemInfo);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (m_ItemInfo != null)
		{
			Inventory.instance.DisplayMessage();
		}
	}
}
