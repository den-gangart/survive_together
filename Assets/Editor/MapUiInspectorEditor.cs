using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(MapGenerator))]
public class MapUiInspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var script = (MapGenerator)target;
        
        if(GUILayout.Button("Generate Map"))
        {
            if(Application.isPlaying)
            {
                script.MakeMap();
            }
        }

        if (GUILayout.Button("Load Map At File"))
        {
            if (Application.isPlaying)
            {
                script.LoadMap();
            }
        }

        if (GUILayout.Button("Save Map to File"))
        {
            if (Application.isPlaying)
            {
                script.SaveDataMap();
            }
        }
    }
}
