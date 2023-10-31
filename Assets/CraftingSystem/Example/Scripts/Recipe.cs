using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
public class Recipe : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
{
	public Item[] input = new Item[9];
	public Item output;
	public int outputAmount = 1;

	private void OnValidate()
	{
		// force the outputAmount always greater than 0
		outputAmount = Mathf.Max(outputAmount, 1);
	}
}
