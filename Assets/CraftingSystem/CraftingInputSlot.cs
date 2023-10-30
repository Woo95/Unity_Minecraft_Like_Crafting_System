using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingInputSlot : MonoBehaviour, IPointerClickHandler
{
	public Crafting crafting;
	public void OnPointerClick(PointerEventData eventData)
	{
		StartCoroutine(CO_CraftInputHandler());
	}

	IEnumerator CO_CraftInputHandler()	// to make sure ItemSlot ClickHandler runs first
	{
		yield return null;
		crafting.InteractInputPanel();
	}
}
