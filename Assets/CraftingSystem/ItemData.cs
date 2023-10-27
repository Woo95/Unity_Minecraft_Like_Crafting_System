using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IPointerEnterHandler, /*IPointerClickHandler,*/ IPointerExitHandler
{
	Item item = null;

	private int count = 0;
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
		this.item = item;
		itemIcon = GetComponent<Image>();
		Count = 7;

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
			itemIcon.sprite = item.icon;
			itemIcon.gameObject.SetActive(true);
			itemCountText.gameObject.SetActive(true);
			itemCountText.text = count.ToString();
		}
	}

	public void UseItemInSlot()
	{
		if (CanUseItem())
		{
			item.Use();
			if (item.isConsumable)
			{
				Count--;
			}
		}
	}

	private bool CanUseItem()
	{
		return (item != null && count > 0);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (item != null)
		{
			Inventory.instance.DisplayMessage(item);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (item != null)
		{
			Inventory.instance.DisplayMessage();
		}
	}
}
