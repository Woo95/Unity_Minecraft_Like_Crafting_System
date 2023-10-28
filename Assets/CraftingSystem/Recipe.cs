using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
public class Recipe : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
{
	public Item[] input = new Item[9];
	public Item output;
	[TextArea]
	public string description = "";

	public void Use()
	{

	}
}