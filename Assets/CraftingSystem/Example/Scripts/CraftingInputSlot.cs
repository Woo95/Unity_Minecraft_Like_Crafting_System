using System.Collections;
using UnityEngine.EventSystems;

public class CraftingInputSlot : ItemSlot
{
	public Crafting crafting;
	public override void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(CO_CraftInputHandler());

        base.OnPointerClick(eventData);
    }

	IEnumerator CO_CraftInputHandler()	// to make sure ItemSlot ClickHandler runs first
	{
		yield return null;
		crafting.InteractInputPanel();
	}
}
