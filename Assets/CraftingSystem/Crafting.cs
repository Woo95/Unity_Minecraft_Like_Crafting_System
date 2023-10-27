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
	GameObject craftingPanel;
}
