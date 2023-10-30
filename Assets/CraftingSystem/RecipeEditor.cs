using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Recipe))]
public class RecipeEditor : Editor
{
	private const int gridSize = 3;

	public override void OnInspectorGUI()
	{
		EditorGUI.BeginChangeCheck();

		EditorGUILayout.LabelField("Recipe Input (3x3):", EditorStyles.boldLabel);
		for (int i = 0; i < gridSize; i++) // Display the 3x3 grid of input items
		{
			EditorGUILayout.BeginHorizontal();
			for (int j = 0; j < gridSize; j++)
			{
				int index = i * gridSize + j;
				EditorGUILayout.PropertyField(serializedObject.FindProperty("input").GetArrayElementAtIndex(index), GUIContent.none, GUILayout.Width(100));
			}
			EditorGUILayout.EndHorizontal();
		}

		GUILayout.Space(10);

		// Display the output item field
		EditorGUILayout.LabelField("Output Item:", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("output"), GUIContent.none, GUILayout.Width(200));

		EditorGUILayout.PrefixLabel("Output Amount:", EditorStyles.boldLabel);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("outputAmount"), GUIContent.none, GUILayout.Width(60), GUILayout.ExpandWidth(false));

		if (EditorGUI.EndChangeCheck())
			serializedObject.ApplyModifiedProperties(); // apply change if change occurs on the end
	}
}