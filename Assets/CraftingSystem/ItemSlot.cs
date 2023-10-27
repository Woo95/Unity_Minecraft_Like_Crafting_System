using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum eSlotType
{
	INVENTORY,
	CRAFT_INPUT,
	CRAFT_OUTPUT
}

public enum eSlotStatus
{
	ITEM_EXIST,
	ITEM_EMPTY
}

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Item m_Item = null;

    [SerializeField]
    ItemData m_ItemData;
	Image image;

	//public eSlotType m_eSlotType;
	//public eSlotStatus m_eSlotStatus;

	// Start is called before the first frame update
	void Start()
    {
		image = GetComponent<Image>();
		if (m_Item)
        {
            m_ItemData.SetItem(m_Item);
			image.raycastTarget = true;

			//m_eSlotType = eSlotType.INVENTORY;
			//m_eSlotStatus = eSlotStatus.ITEM_EXIST;
		}
		else
		{
			//m_eSlotStatus = eSlotStatus.ITEM_EMPTY;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
        Debug.Log("인벤토리 슬롯 클릭");
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (SlotAndCursor())
			{
				// swap slot and cursor items
				SwapItem();
			}
			else if (SlotAndNoCursor())
			{
				// pick up on item from the slot to the cursor
				PickItem();
			}
			else if (NoSlotAndCursor())
			{
				// drop item from the cursor to the slot
				PlaceItem();
			}
		}
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
			if (SlotAndCursor())
			{
				// swap slot and cursor items
				SwapItem();
			}
			else if (SlotAndNoCursor())
			{
				// pick up half of the item from the slot to the cursor
			}
			else if (NoSlotAndCursor())
			{
				// drop each item from the cursor to the slot
			}
		}
	}
	public void PickItem()
	{
		m_ItemData.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetClickItemData(m_ItemData, m_Item);

		m_Item = null;
		m_ItemData = null;
	}
	public void PlaceItem()
    {
		ItemCursor.instance.ClearCursor(out m_ItemData, out m_Item);

		m_ItemData.transform.SetParent(transform);
		m_ItemData.GetComponent<Image>().raycastTarget = true;
		m_ItemData.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}
	public void SwapItem()
	{
		ItemData slotItemData = m_ItemData;
		Item slotItem = m_Item;

		PlaceItem();

		slotItemData.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetClickItemData(slotItemData, slotItem);
	}


	#region OnPointerClick Conditions
	public bool SlotAndCursor()
	{
        return (m_Item && m_ItemData) && ItemCursor.instance.ItemCursorExist();
	}
	public bool SlotAndNoCursor()
	{
        return (m_Item && m_ItemData) && !ItemCursor.instance.ItemCursorExist();
	}
	public bool NoSlotAndCursor()
	{
		return !(m_Item && m_ItemData) && ItemCursor.instance.ItemCursorExist();
	}
	#endregion
}
