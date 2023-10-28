using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
	[SerializeField] 
	List<Recipe> recipeList = new List<Recipe>();
	[SerializeField]
	List<string> recipeCode = new List<string>();
	[SerializeField]
	List<string> recipeCodeModified = new List<string>();

	[SerializeField]
	GameObject craftingInputPanel;
	[SerializeField]
	CraftingOutputSlot craftingOutputSlot;

	private void OnValidate()	// Start Editor
	{
		InitializeRecipeCodes();
	}
	//[ContextMenu("Show Recipe")]
	void InitializeRecipeCodes()
	{
		string inputCode;
		#region Add all Recipe input patterns to the recipeCode List
		Item[] input;
		recipeCode.Clear();
		for (int i=0; i< recipeList.Count; i++)
		{
			if (recipeList[i] == null) // error notifier
			{
				Debug.LogError("recipeList["+ i +"]" + "was null");
				recipeList.RemoveAt(i);
				return;
			}

			input = recipeList[i].input;
			inputCode = "";
			for (int j=0; j < input.Length; j++)
			{
				if (input[j] != null)
				{
					if (j > 0 && j % 3 == 0)
						inputCode += '@';	// nextline's first element notify char

					inputCode += input[j].itemType.ToString();
				}
				else
					inputCode += ((eItemType)0).ToString();
			}
			recipeCode.Add(inputCode);
		}
		#endregion

		#region Modify all recipeCode and add to the recipeCodeModified List
		recipeCodeModified.Clear();
		for (int j = 0; j < recipeCode.Count; j++)
		{
			//1. 앞부분지우기
			inputCode = recipeCode[j];
			inputCode = ModifiedRecipeCode(inputCode);
			recipeCodeModified.Add(inputCode);
		}
		#endregion
		//XXXXXXXXA1A1@A1XXXX   => A1A1@A1
		//A1A1XXXXA1XXXXXXXX   => A1A1XXXXA1
		// XXXXXXXXXXXA1@A1XXXX => A1A1@A1
		// A1A1XXXXXA1XXXXXX => A1A1XXXXA1
		// XXA1XX@XXA1XXXX	 => A1XX@XXA1
		// XXA1XXXXXXXXXXX   => A1
	}

	string ModifiedRecipeCode(string _code)  // FIX
	{
		//Debug.Log(" I1:" + _code);
		while (true)
		{
			if (_code.Substring(0, 1) == "X")
			{
				_code = _code.Substring(1);
				if (_code == "") return "";
			}
			else break;
		}
		//Debug.Log(" I2:" + _code);

		//1. 뒷부분지우기
		while (true)
		{
			if (_code.Substring(_code.Length - 1, 1) == "X")
			{
				_code = _code.Substring(0, _code.Length - 1);
			}
			else break;
		}
		if (_code.Length > 0)
		{
			if (_code.Substring(0, 1) == "@")
			{
				_code = _code.Substring(1);
			}
		}
		return _code;
	}

	public int CheckRecipe(string inputCode)
	{
		return recipeCodeModified.FindIndex(code => code == inputCode);
	}

	public void InteractInputPanel()
	{
		craftingOutputSlot.DestroyCrafting();

		List<ItemSlot> craftInputList = 
			new List<ItemSlot>(craftingInputPanel.transform.GetComponentsInChildren<ItemSlot>());

		#region Craft Input Panel Code - Before Modified
		ItemData outputItemDataFrame = null;
		string inputCode = "";
		for (int i = 0; i < craftInputList.Count; i++)
		{
			if (craftInputList[i].GetItemData() != null)
			{
				if (i > 0 && i % 3 == 0)
					inputCode += "@";

				inputCode += craftInputList[i].GetItem().itemType.ToString();

				// save empty frame
				outputItemDataFrame = craftInputList[i].GetItemData();
			}
			else
			{
				inputCode += ((eItemType)0).ToString();
			}
		}
		Debug.Log("Current InputPanel Code - (Plain): " + inputCode);
		#endregion

		#region Craft Input Panel Code - After Modified
		inputCode = ModifiedRecipeCode(inputCode);
		Debug.Log("Current InputPanel Code - (Modified): " + inputCode);
		if (inputCode == "") return;
		#endregion

		#region RecipeCodeModified index by searching from the Modified Craft Input Panel Code
		int foundRecipeIndex = recipeCodeModified.FindIndex(code => code == inputCode);
		if (foundRecipeIndex == -1) return; // -1 means not found
		#endregion

		#region Generate Item to the Output Slot
		Item outputItem = recipeList[foundRecipeIndex].output;
		craftingOutputSlot.CreateOutputItem(outputItem, outputItemDataFrame);
		#endregion
	}	
}