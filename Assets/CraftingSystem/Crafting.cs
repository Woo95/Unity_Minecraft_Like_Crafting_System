using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
	#region singletone
	public static Crafting instance;
	private void Awake()
	{
		instance = this;
	}
	#endregion

	[SerializeField] 
	List<Recipe> recipeList = new List<Recipe>();
	[SerializeField]
	List<string> recipeCode = new List<string>();

	[SerializeField]
	GameObject craftingPanel;

	[ContextMenu("Parsing Recipe")]
	void Editor_Recipe()
	{
		Item[] input;
		string inputCode = "";
		recipeCode.Clear();
		for (int i=0; i< recipeList.Count; i++)
		{
			input = recipeList[i].input;
			inputCode = "";
			for (int j=0; j < input.Length; j++)
			{
				if (input[j] != null)
					inputCode += input[j].itemType.ToString();
				else
					inputCode += ((eItemType)0).ToString();
			}
		}
		recipeCode.Add(inputCode);
	}

}
