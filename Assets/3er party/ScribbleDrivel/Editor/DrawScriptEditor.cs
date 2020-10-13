using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace LylekGames
{
    [CustomEditor(typeof(DrawScript))]
    public class DrawScriptEditor : Editor
    {
        DrawScript myDrawScript;
        SerializedObject serialObject;
        SerializedProperty optimizeProperty;
        
        public void OnEnable()
        {
            myDrawScript = (DrawScript)target;
            serialObject = new SerializedObject(myDrawScript);
            optimizeProperty = serialObject.FindProperty("optimize");

            myDrawScript.GetDefaultSettings();
        }
        public override void OnInspectorGUI()
        {
            serialObject.Update();

            GUI.enabled = false;
            MonoScript script = MonoScript.FromMonoBehaviour((DrawScript)target);
            script = EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false) as MonoScript;
            EditorGUILayout.Space();

            if (!myDrawScript.GetComponent<Image>())
            {
                GUI.enabled = true;
                EditorGUILayout.HelpBox("This object must contain an Image component!", MessageType.Error);
                if (GUILayout.Button("Add Image Component"))
                {
                    myDrawScript.gameObject.AddComponent<Image>();
                    if (!myDrawScript.gameObject.GetComponent<CanvasRenderer>())
                        myDrawScript.gameObject.AddComponent<CanvasRenderer>();
                    if (!myDrawScript.gameObject.GetComponent<RectTransform>())
                        myDrawScript.gameObject.AddComponent<RectTransform>();
                }
                GUI.enabled = false;
            }
            else
            {
                GUI.enabled = true;
            }

            EditorGUILayout.LabelField("Optimization", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(optimizeProperty);

            if (myDrawScript.optimize)
            {
                if (myDrawScript.canvas && myDrawScript.canvas.renderMode == RenderMode.WorldSpace)
                {
                    EditorGUILayout.HelpBox("Optimization is not compatible with a World-Space Canvas!", MessageType.Error);
                    if (GUILayout.Button("Fix"))
                        myDrawScript.canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    GUI.enabled = false;
                }
                else if (myDrawScript.transform.rotation != Quaternion.identity)
                {
                    EditorGUILayout.HelpBox("Optimization is not compatible with rotated/askew Canvas!", MessageType.Error);
                    if (GUILayout.Button("Fix"))
                        myDrawScript.transform.rotation = Quaternion.identity;
                    GUI.enabled = false;
                }
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Components", EditorStyles.boldLabel);
            myDrawScript.canvas = EditorGUILayout.ObjectField("Canvas", myDrawScript.canvas, typeof(Canvas), false) as Canvas;
            if (!myDrawScript.canvas)
            {
                EditorGUILayout.HelpBox("This variable should not be null!", MessageType.Warning);
                if (GUILayout.Button("Find Canvas"))
                {
                    if (myDrawScript.transform.root.gameObject.GetComponent<Canvas>())
                        myDrawScript.canvas = myDrawScript.transform.root.gameObject.GetComponent<Canvas>();
                }
            }
            myDrawScript.brushPrefab = EditorGUILayout.ObjectField("Brush Prefab (Shape)", myDrawScript.brushPrefab, typeof(GameObject), false) as GameObject;
            if (!myDrawScript.brushPrefab)
            {
                EditorGUILayout.HelpBox("This variable can not be null!", MessageType.Error);
                if (GUILayout.Button("Get Default Brush"))
                    myDrawScript.brushPrefab = Resources.Load("Brush") as GameObject;
            }
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            myDrawScript.brushColor = EditorGUILayout.ColorField("Brush Color", myDrawScript.brushColor);
            myDrawScript.brushSize = EditorGUILayout.IntSlider("Brush Size", myDrawScript.brushSize, 1, 50);
            myDrawScript.spacing = EditorGUILayout.Slider("Spacing", myDrawScript.spacing, 0.1f, 2.0f);
            EditorGUILayout.Space();
            if (myDrawScript.drawHistory.Count > 0)
            {
                EditorGUILayout.LabelField("History", EditorStyles.boldLabel);
                for (int i = 0; i < myDrawScript.drawHistory.Count; i++)
                    myDrawScript.drawHistory[i] = EditorGUILayout.ObjectField(myDrawScript.drawHistory[i], typeof(GameObject), false) as GameObject;
            }
            else
                EditorGUILayout.LabelField("History (none)", EditorStyles.boldLabel);

            EditorGUILayout.Space();
            if (GUILayout.Button("Reset to Default Settings"))
                myDrawScript.GetDefaultSettings();

            serialObject.ApplyModifiedProperties();
        }
    }
}
