using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Item m_Item = null;

    [SerializeField]
    ItemData m_ItemData;
	Image image;

    // Start is called before the first frame update
    void Start()
    {
		image = GetComponent<Image>();
		if (m_Item)
        {
            m_ItemData.SetItem(m_Item);
			image.raycastTarget = true;
        }
    }

	public void OnPointerClick(PointerEventData eventData)
	{
        Debug.Log("인벤토리 슬롯 클릭");
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			PickItem();
		}
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
			PlaceItem();
		}
	}

    public void PlaceItem()
    {
		if (!m_ItemData && ItemCursor.instance.ItemCursorExist())
		{
			ItemCursor.instance.ClearCursor(out m_ItemData, out m_Item);
			m_ItemData.transform.SetParent(transform);
			m_ItemData.GetComponent<Image>().raycastTarget = true;
			m_ItemData.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
		}
	}

    public void PickItem()
    {
		if (m_ItemData && !ItemCursor.instance.ItemCursorExist())
		{
			m_ItemData.GetComponent<Image>().raycastTarget = false;
			ItemCursor.instance.SetClickItemData(m_ItemData, m_Item);

			m_Item = null;
			m_ItemData = null;
		}
	}
}
