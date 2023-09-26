using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static JSONManager;

public class LevelEditorWindow : EditorWindow
{
    [MenuItem("GameData/LevelEditor")]
    public static void ShowExample()
    {
        LevelEditorWindow wnd = GetWindow<LevelEditorWindow>();
        wnd.titleContent = new GUIContent("Level Editor");
    }
    public GameObject goalObj;
    private void OnGUI()
    {
        int level = 0;

        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("prev"))
        {

        }

        EditorGUILayout.LabelField("Level " + level);

        if (GUILayout.Button("next"))
        {

        }
        GUILayout.EndHorizontal();
        string jsonFilesDirectory = "Assets/_Assets/Resources/levels/easy";
        GUILayout.BeginHorizontal();
        jsonFilesDirectory = EditorGUILayout.TextField("path", jsonFilesDirectory);
        GUILayout.EndHorizontal();
        goalObj = (GameObject)EditorGUILayout.ObjectField(goalObj, typeof(GameObject), true);
        if (GUILayout.Button("Load All Data"))
        {
            string[] jsonFiles = Directory.GetFiles(jsonFilesDirectory, "*.json");

            string jsonContent = File.ReadAllText(jsonFiles[0]);
            //JSONData jsonData = JsonUtility.FromJson<JSONData>(jsonContent);

            //var json = AssetDatabase.LoadAssetAtPath<TextAsset>(jsonContent).text;
            JObject jobject = JObject.Parse(jsonContent);
            //Debug.Log(JsonConvert.DeserializeObject<JSONData>(jobject.ToString()).goals[0][0].id);
            JSONData jsonData = JsonConvert.DeserializeObject<JSONData>(jobject.ToString());

            int index = 0;
            for(int i = 0; i < jsonData.goals.Count; i++)
            {
                for(int j = 0; j < jsonData.goals[i].Count; j++)
                {
                    Debug.Log(jsonData.goals[i][j].id + " " + index);
                    index++;
                    Debug.Log(index);
                }
            }



        }
        GUILayout.EndVertical();
    }
}
