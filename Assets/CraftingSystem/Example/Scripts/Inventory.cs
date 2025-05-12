using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region singletone
	public static Inventory instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

    [SerializeField]
    GameObject inventoryPanel;

	[SerializeField]
	private TMPro.TextMeshProUGUI descriptionText;
	[SerializeField]
	private TMPro.TextMeshProUGUI nameText;

    public void DisplayMessage(ItemInfo item = null)
    {
        if (item)
        {
            descriptionText.text = item.description;
            nameText.text = item.name;
        }
        else
        {
			descriptionText.text = "";
			nameText.text = "";
		}
    }
}
