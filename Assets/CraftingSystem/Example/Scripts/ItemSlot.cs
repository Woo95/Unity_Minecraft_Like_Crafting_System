using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemInfo m_Item = null;

    [SerializeField]
    protected ItemInstance m_ItemInstance;
	protected Image image;

	void Start()
    {
		image = GetComponent<Image>();
		if (m_Item)
        {
            m_ItemInstance.SetItem(m_Item);
			image.raycastTarget = true;
		}
		else
		{
			if (m_ItemInstance)
			{
				Destroy(m_ItemInstance.gameObject);
				m_ItemInstance = null;
			}
		}
	}

	public ItemInstance GetItemData()
	{
		return m_ItemInstance;
	}
	public ItemInfo GetItem()
	{
		return m_Item;
	}

	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			if (SlotAndCursor())
			{
				if (!SlotAndCursorSameItem()) // if not same item
					SwapItem();
				else 
					StackAllItemCursorToSlot(); // put cursor item and stack them

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
				if (!SlotAndCursorSameItem()) // if not same item
					SwapItem();
				else
					StackOneItemCursorToSlot(); // place one by one and stack them
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
		m_ItemInstance.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetCursorItemData(m_ItemInstance, m_Item);

		m_Item = null;
		m_ItemInstance = null;
	}
	public void PlaceItem()
    {
		ItemCursor.instance.ClearCursor(out m_ItemInstance, out m_Item);

		m_ItemInstance.transform.SetParent(transform);
		m_ItemInstance.GetComponent<Image>().raycastTarget = true;
		m_ItemInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}
	public void SwapItem()
	{
		ItemInstance slotItemData = m_ItemInstance;
		ItemInfo slotItem = m_Item;

		PlaceItem();

		slotItemData.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetCursorItemData(slotItemData, slotItem);
	}

	public void PickHalfOfItem()
	{
		int totalItemCount = m_ItemInstance.Count;
		int halfItemCount = (int)(totalItemCount * 0.5f);

		if (halfItemCount <= 0)
			return;

		m_ItemInstance.Count = halfItemCount;

		ItemInstance cursorItemData = Instantiate(m_ItemInstance); // create a duplicate
		cursorItemData.SetItem(m_Item);
		cursorItemData.gameObject.name = m_Item.name;

		cursorItemData.GetComponent<Image>().raycastTarget = false;
		ItemCursor.instance.SetCursorItemData(cursorItemData, m_Item);

		cursorItemData.Count = totalItemCount - halfItemCount;
	}
	public void StackAllItemCursorToSlot()
	{
		ItemInstance cursorItemData = ItemCursor.instance.GetCursorItemData();

		m_ItemInstance.Count += cursorItemData.Count;

		Destroy(cursorItemData.gameObject);
	}

	public void StackAllItemSlotToCursor()
	{
		ItemInstance cursorItemData = ItemCursor.instance.GetCursorItemData();

		cursorItemData.Count += m_ItemInstance.Count;

		DestroyCurrentItem();
	}
	public void DestroyCurrentItem()
	{
		if (m_ItemInstance != null)
		{
			Destroy(m_ItemInstance.gameObject);
			m_ItemInstance = null;
		}
	}

	public void StackOneItemCursorToSlot()
	{
		ItemInstance cursorItemData = ItemCursor.instance.GetCursorItemData();

		m_ItemInstance.Count += 1;
		cursorItemData.Count -= 1;

		if (cursorItemData.Count <= 0)
			Destroy(cursorItemData.gameObject);
	}
	public void DropOneItemCursorToSlot()
	{
		ItemCursor itemCursorIns = ItemCursor.instance;
		ItemInstance cursorItemData = itemCursorIns.GetCursorItemData();
		ItemInfo cursorItem = itemCursorIns.GetCursorItem();
		cursorItemData.Count -= 1;

		ItemInstance slotItemData = Instantiate(cursorItemData);
		m_ItemInstance = slotItemData;
		m_Item = cursorItem;

		m_ItemInstance.SetItem(m_Item);
		m_ItemInstance.gameObject.name = m_Item.name;
		m_ItemInstance.transform.SetParent(transform);

		m_ItemInstance.GetComponent<Image>().raycastTarget = true;
		m_ItemInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		m_ItemInstance.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		m_ItemInstance.Count = 1;

		if (cursorItemData.Count <= 0)
			Destroy(cursorItemData.gameObject);
	}
#endregion

	#region OnPointerClick Conditions
	public bool SlotAndCursor()
	{
        return (m_Item && m_ItemInstance) && ItemCursor.instance.ItemCursorExist();
	}
	public bool SlotAndNoCursor()
	{
        return (m_Item && m_ItemInstance) && !ItemCursor.instance.ItemCursorExist();
	}
	public bool NoSlotAndCursor()
	{
		return !(m_Item && m_ItemInstance) && ItemCursor.instance.ItemCursorExist();
	}

	public bool SlotAndCursorSameItem()
	{
		return ItemCursor.instance.GetCursorItem() == m_Item;
	}
	#endregion
}
