using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(SliceRenamer))] 
public class SliceRenamerEditor : Editor
{
	private SliceRenamer _target;

    public override void OnInspectorGUI()
    {
    	_target = (SliceRenamer)target;

        EditorGUILayout.LabelField("This script rename all slices within a spritesheet with a given name followed by a number.");
        EditorGUILayout.Separator();

        DrawStringField();
        DrawObjectField();

        
        if (GUILayout.Button("EXECUTE"))
        {
            RenameSlices();
        }
    }

    public void DrawStringField()
    {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("String Example");

            _target.newName = EditorGUILayout.TextField(_target.newName);

            EditorGUILayout.EndHorizontal();
    }

	private void DrawObjectField()
	{
		EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Name Prefix");
        _target.texture2D = EditorGUILayout.ObjectField(_target.texture2D, typeof(Texture2D), false);

        EditorGUILayout.EndHorizontal();		
	}

	private void RenameSlices()
    {
  		string path = AssetDatabase.GetAssetPath (_target.texture2D);
        TextureImporter textureImporter = AssetImporter.GetAtPath (path) as TextureImporter;

        SpriteMetaData[] sliceMetaData = textureImporter.spritesheet;

        int index = 0;
        foreach (SpriteMetaData individualSliceData in sliceMetaData)
        {
            sliceMetaData[index].name = string.Format (_target.newName + "_{0}", index);

            index++;
        }

        textureImporter.spritesheet = sliceMetaData;
        EditorUtility.SetDirty (textureImporter);
        textureImporter.SaveAndReimport ();

        AssetDatabase.ImportAsset (path, ImportAssetOptions.ForceUpdate);
    }
}
