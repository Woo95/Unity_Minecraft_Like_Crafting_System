using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class Crafting : MonoBehaviour
{
	[SerializeField] 
	List<Recipe> recipeList = new List<Recipe>();
	[SerializeField]
	List<string> recipeCode = new List<string>();
	[SerializeField]
	List<string> recipeCodeDecrypted = new List<string>();

	[SerializeField]
	GameObject craftingInputPanel;
	[SerializeField]
	ItemSlot craftingOutputSlot;

	[ContextMenu("Parsing Recipe")]
	void Editor_Recipe()
	{
		Item[] input;
		string inputCode;
		recipeCode.Clear();
		for (int i=0; i< recipeList.Count; i++)
		{
			if (recipeList[i] == null)
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
						inputCode += "@";

					inputCode += input[j].itemType.ToString();
				}
				else
					inputCode += ((eItemType)0).ToString();
			}
			recipeCode.Add(inputCode);
		}

		//XXXXXXXXA1A1@A1XXXX   => A1A1@A1
		//A1A1XXXXA1XXXXXXXX   => A1A1XXXXA1
		string _code;
		recipeCodeDecrypted.Clear();
		for (int j = 0; j < recipeCode.Count; j++)
		{
			//1. 앞부분지우기
			_code = recipeCode[j];
			_code = DecryptedRecipeCode(_code);
			recipeCodeDecrypted.Add(_code);
		}


		// XXXXXXXXXXXA1@A1XXXX => A1A1@A1
		// A1A1XXXXXA1XXXXXX => A1A1XXXXA1
		// XXA1XX@XXA1XXXX	 => A1XX@XXA1
		// XXA1XXXXXXXXXXX   => A1

		//bool removeX = true;
		//string decrypedRecipeCode;
		//for (int i = 0; i < recipeCode.Count; i++)
		//{
		//	string eachRecipeCode = recipeCode[i];
		//	decrypedRecipeCode = "";
		//	for (int j = 0; j < eachRecipeCode.Length; j++)
		//	{
		//		char character = eachRecipeCode[j];

		//		if (character == 'X')
		//			if (removeX) continue;
		//		else
		//			removeX = !removeX;

		//		decrypedRecipeCode += character;
		//	}
		//	recipeCodeFin.Add(decrypedRecipeCode);
		//}
}

	string DecryptedRecipeCode(string _code)
	{
		//Debug.Log(" I1:" + _code);
		while (true)
		{
			if (_code.Substring(0, 1) == "X")
			{
				_code = _code.Substring(1);
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
		//Debug.Log(" I3:" + _code);
		return _code;
	}

	public bool CheckCraftingPanel()
	{
		return true;
	}
	public bool CheckRecipe(string strInputRecipe)
	{
		// replace XX to empty string
		string decryptedRecipeCode = DecryptedRecipeCode(strInputRecipe);
		for (int i=0; i < recipeCode.Count; ++i)
		{
			if (recipeCodeDecrypted[i] == decryptedRecipeCode)
				return true;
		}
		return false;
	}

	public void InteractInputPanel()
	{
		// 1. Craft보드에 아이템을 놓거나, 버리거나 할때 작동
		// 2. Craft보드의 데이터를 string -> // XXXXXXXXXXXXXAAA XX
		List<ItemSlot> craftingInputList = 
			new List<ItemSlot>(craftingInputPanel.transform.GetComponentsInChildren<ItemSlot>());

		string inputCode = "";
		for (int i = 0; i < craftingInputList.Count; i++)
		{
			if (craftingInputList[i].GetItemData() != null)
			{
				if (i > 0 && i % 3 == 0)
					inputCode += "@";

				inputCode += craftingInputList[i].GetItem().itemType.ToString();
			}
			else
			{
				inputCode += ((eItemType)0).ToString();
			}
		}

		Debug.Log(inputCode);

		// 3. XXXXXXA1A1@A1XXXX -> A1A1@A1
		// 4. A1A1@A1 이 recipeCode2에 존재하는가?
		// 5. Output 에 출력
	}
}
