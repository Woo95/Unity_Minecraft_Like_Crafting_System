using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Recipe", menuName = "Items/New Recipe")]
public class Recipe : ScriptableObject
{
	public ItemInfo[] input = new ItemInfo[9];	// 9 => 3x3 crafting input pannel. Could be any nXn!
	public ItemInfo output;
	public int outputAmount = 1;

	private void OnValidate()
	{
		// force the outputAmount always greater than 0
		outputAmount = Mathf.Max(outputAmount, 1);
	}
}
