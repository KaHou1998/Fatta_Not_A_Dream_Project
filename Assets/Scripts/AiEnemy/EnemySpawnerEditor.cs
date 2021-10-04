using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor
{
    EnemySpawner spawner;

    public void OnEnable()
    {
        spawner = (EnemySpawner)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (spawner.waypoints == null)
        {
            return;
        }
      
        if(spawner.waypoints.Count < 5)
        {
            if (GUILayout.Button("Generated Random Waypoint", EditorStyles.miniButton))
            {
                spawner.GenerateRandomWaypoint();
            }
        }

        if(spawner.waypoints.Count != 0)
        {
            if (GUILayout.Button("Remove Waypoint", EditorStyles.miniButton))
            {
                spawner.RemoveLastWaypoint();
            }
        }

        if (spawner.waypoints.Count == 0)
        {
            EditorGUILayout.HelpBox("Empty Waypoint", MessageType.Info);
        }

        EditorUtility.SetDirty(spawner);
    }

    
}
