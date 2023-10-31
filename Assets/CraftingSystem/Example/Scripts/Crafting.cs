using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
	[SerializeField] 
	List<Recipe> recipeList = new List<Recipe>();
	[SerializeField]
	List<string> recipeCode = new List<string>();

	[SerializeField]
	GameObject craftingInputPanel;
	[SerializeField]
	CraftingOutputSlot craftingOutputSlot;

	#region GenerateInputCodeFromRecipes before start
	private void OnValidate()   // calls whenever the asset is modified in the Unity Editor.
	{
		GenerateInputCodeFromRecipes();
	}
	void GenerateInputCodeFromRecipes() // add all Recipe input patterns to the recipeCode List
	{
		recipeCode.Clear();
		for (int i=0; i< recipeList.Count; i++)
		{
			if (recipeList[i] == null) // error notifier
			{
				Debug.LogError("recipeList["+ i +"]" + "was null");
				recipeList.RemoveAt(i);
				return;
			}

			Item[] input = recipeList[i].input;
			string inputCode = GenerateInputCode(input);
			recipeCode.Add(inputCode);
		}
	}
	#endregion

	#region Code Handler
	string GenerateInputCode(Item[] input)
	{
		string inputCode = "";
		foreach (Item item in input)
		{
			inputCode += (item != null) ? 
				item.itemType.ToString() : ((eItemType)0).ToString();
		}
		return ModifyInputCode(inputCode);
	}

	string ModifyInputCode(string inputCode)
	{
		int firstCharIndex = 0;
		int lastCharIndex = inputCode.Length - 1;

		while (firstCharIndex < inputCode.Length && inputCode[firstCharIndex] == 'X') // front
			firstCharIndex++;
		while (lastCharIndex >= 0 && inputCode[lastCharIndex] == 'X') // back
			lastCharIndex--;

		if (firstCharIndex < lastCharIndex)	// combine
			return inputCode.Substring(firstCharIndex, lastCharIndex - firstCharIndex + 1);

		return "";
	}
	#endregion

	#region Craft System
	public void InteractInputPanel()    // function calls from the CraftingInputSlot.cs only if any change on InputPanel
	{
		craftingOutputSlot.DestroyCurrentItem();	// to reset output item

		List<ItemSlot> craftInputList =
			new List<ItemSlot>(craftingInputPanel.transform.GetComponentsInChildren<ItemSlot>());

		Item[] input = new Item[craftInputList.Count];
		for (int i = 0; i < craftInputList.Count; i++) // stores the 'Item' objects from the craft input slots
		{
			input[i] = craftInputList[i].GetItem();
		}

		string inputCode = GenerateInputCode(input);
		if (inputCode == "") return;

		Debug.Log(inputCode);

		int foundRecipeIndex = recipeCode.FindIndex(code => code == inputCode);
		if (foundRecipeIndex == -1) return; // -1 means not found

		CreateOutputItem(foundRecipeIndex, craftInputList);
	}

	void CreateOutputItem(int foundRecipeIndex, List<ItemSlot> craftInputList)
	{
		Recipe foundRecipe = recipeList[foundRecipeIndex];
		craftingOutputSlot.CreateOutputItem(foundRecipe, craftInputList);
	}
	#endregion
}