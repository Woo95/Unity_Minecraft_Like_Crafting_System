using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//Holds reference and count of items, manages their visibility in the Inventory panel
public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public Item item = null;

    [SerializeField]
    ItemData itemData;

    // Start is called before the first frame update
    void Start()
    {
        if(item)
        {
            itemData.SetItem(item);
        }
    }

	public void OnPointerClick(PointerEventData evenData)
	{
        Debug.Log("ºó°÷ Å¬¸¯");
	}
}
