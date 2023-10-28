using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Item m_Item = null;

    [SerializeField]
    protected ItemData m_ItemData;
	protected Image image;

	void Start()
    {
		image = GetComponent<Image>();
		if (m_Item)
        {
            m_ItemData.SetItem(m_Item);
			image.raycastTarget = true;
		}
		else
		{
			if (m_ItemData)
			{
				Destroy(m_ItemData.gameObject);
				m_ItemData = null;
			}
		}
	}

	public ItemData GetItemData()
	{
		return m_ItemData;
	}
	public Item GetItem()
	{
		return m_Item;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (SlotAndCursor())
			{
				if (!SlotAndCursorSameItem()) // if not different item
					SwapItem();
				else // put cursor item and stack them
					StackAllItemCursorToSlot();

			}
			else if (SlotAndNoCursor())
			{
				PickItem(); // pick up on item from the slot to the cursor
			}
			else if (NoSlotAndCursor())
			{
				PlaceItem(); // drop item from the cursor to the slot
			}
		}
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
			if (SlotAndCursor())
			{
				if (!SlotAndCursorSameItem()) // if not different item
					SwapItem();
				else // place one by one and stack them
					StackOneItemCursorToSlot();
			}
			else if (SlotAndNoCursor())
			{
				PickHalfOfItem(); // pick up half of the item from the slot to the cursor
			}
			else if (NoSlotAndCursor())
			{
				DropOneItemCursorToSlot(); // drop each item from the cursor to the slot
			}
		}
	}

	#region OnPointerClick Events
	public void PickItem()
	{
		m_ItemData.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetCursorItemData(m_ItemData, m_Item);

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
		ItemCursor.instance.SetCursorItemData(slotItemData, slotItem);
	}

	public void PickHalfOfItem()
	{
		int totalItemCount = m_ItemData.Count;
		int halfItemCount = (int)(totalItemCount * 0.5f);

		if (halfItemCount <= 0)
			return;

		m_ItemData.Count = halfItemCount;

		ItemData cursorItemData = Instantiate(m_ItemData); // create a duplicate
		cursorItemData.SetItem(m_Item);

		cursorItemData.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetCursorItemData(cursorItemData, m_Item);

		cursorItemData.Count = totalItemCount - halfItemCount;
	}
	public void StackAllItemCursorToSlot()
	{
		ItemData cursorItemData = ItemCursor.instance.GetCursorItemData();

		m_ItemData.Count += cursorItemData.Count;

		Destroy(cursorItemData.gameObject);
	}
	public void StackOneItemCursorToSlot()
	{
		ItemData cursorItemData = ItemCursor.instance.GetCursorItemData();

		m_ItemData.Count += 1;
		cursorItemData.Count -= 1;

		if (cursorItemData.Count <= 0)
			Destroy(cursorItemData.gameObject);
	}
	public void DropOneItemCursorToSlot()
	{
		ItemCursor itemCursorIns = ItemCursor.instance;
		ItemData cursorItemData = itemCursorIns.GetCursorItemData();
		Item cursorItem = itemCursorIns.GetCursorItem();
		cursorItemData.Count -= 1;

		ItemData slotItemData = Instantiate(cursorItemData);
		m_ItemData = slotItemData;
		m_Item = cursorItem;

		m_ItemData.transform.SetParent(transform);
		m_ItemData.SetItem(m_Item);
		
		m_ItemData.GetComponent<Image>().raycastTarget = true;
		m_ItemData.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		m_ItemData.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		m_ItemData.Count = 1;

		if (cursorItemData.Count <= 0)
			Destroy(cursorItemData.gameObject);
	}
#endregion

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

	public bool SlotAndCursorSameItem()
	{
		return ItemCursor.instance.GetCursorItem() == m_Item;
	}
	#endregion
}
