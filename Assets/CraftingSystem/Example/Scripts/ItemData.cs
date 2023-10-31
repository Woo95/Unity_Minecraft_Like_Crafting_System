using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IPointerEnterHandler, /*IPointerClickHandler,*/ IPointerExitHandler
{
	Item m_Item = null;

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

	public void SetItem(Item item)
	{
		m_Item = item;
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
			itemIcon.sprite = m_Item.icon;
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
		if (m_Item != null)
		{
			Inventory.instance.DisplayMessage(m_Item);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (m_Item != null)
		{
			Inventory.instance.DisplayMessage();
		}
	}
}
