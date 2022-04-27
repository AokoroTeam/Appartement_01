using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Search;
using System.IO;
using System.Linq;
using UnityEditor.Formats.Fbx.Exporter;

namespace Aokoro.Editor.Replacer
{
    public class LODExporterWindow : EditorWindow
    {
        private GameObject prefab;
        private int tab;
        [SerializeField]
        private GameObject[] targets;

        [MenuItem("Tools/Aokoro/Exporter")]
        static void CreateWindow()
        {
            EditorWindow.GetWindow<LODExporterWindow>("Exporter");
        }

        private LODGroup lodGroup;

        private string exportPath;
        private void Awake()
        {
            exportPath = Application.dataPath;
        }
        private void OnGUI()
        {
            GUILayout.BeginVertical("GroupBox");
            lodGroup = EditorGUILayout.ObjectField("LOD group", lodGroup, typeof(LODGroup), true) as LODGroup;
            if (lodGroup != null)
            {
                if (GUILayout.Button("Setup"))
                    SetupLODGroup(lodGroup);

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Export path"))
                    exportPath = EditorUtility.OpenFolderPanel("Choose export path", exportPath, "");
                EditorGUILayout.LabelField(exportPath);

                GUILayout.EndHorizontal();
                if (GUILayout.Button("Export"))
                    ExportModel(lodGroup, exportPath);
            }
            GUILayout.EndVertical();
        }

        private static void SetupLODGroup(LODGroup lODGroup)
        {
            LOD[] lods = lODGroup.GetLODs();

            for (int i = 0; i < lODGroup.lodCount; i++)
            {
                LOD lod = lods[i];
                Renderer[] renderersArray = lod.renderers;

                //Create a new parent for the export
                Transform parent = null;

                for (int j = 0; j < renderersArray.Length; j++)
                {
                    var renderer = renderersArray[j];
                    string newName = GetNewName(lODGroup, i, j);

                    string lodGroupName = $"LOD_{i}";
                    if (parent == null)
                    {
                        if (renderer.transform.parent.name != lodGroupName)
                            parent = new GameObject(lodGroupName).transform;
                        else
                            parent = renderer.transform.parent;

                        parent.SetParent(lODGroup.transform);
                        parent.localPosition = Vector3.zero;
                    }

                    renderer.transform.SetParent(parent);
                    renderer.transform.SetAsLastSibling();

                    renderer.gameObject.name = newName;
                }
            }
        }

        private static void ExportModel(LODGroup group, string exportPath)
        {
            GameObject gameObject = group.gameObject;
            var renderers = group.GetLODs().SelectMany(lod => lod.renderers);

            string filePath = Path.Combine(Application.dataPath, exportPath, $"{gameObject.name}.fbx");
            string exportedPath = ModelExporter.ExportObjects(filePath, renderers.Select(ctx => ctx.gameObject).ToArray());

            //If export is successful
            if (!string.IsNullOrEmpty(exportedPath))
            {
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                if (exportedPath.StartsWith(Application.dataPath))
                    exportedPath = "Assets" + exportedPath.Substring(Application.dataPath.Length);

                UnityEngine.Object[] meshes = AssetDatabase.LoadAllAssetRepresentationsAtPath(exportedPath) as UnityEngine.Object[];

                //Reassign meshes
                for (int i = 0; i < meshes.Length; i++)
                {
                    if (meshes[i] is Mesh mesh)
                    {
                        var renderer = renderers.FirstOrDefault(ctx => ctx.gameObject.name == mesh.name);
                        if (renderer != default)
                            renderer.GetComponent<MeshFilter>().mesh = mesh;
                    }
                }
            }

        }
        private static string GetNewName(LODGroup lODGroup, int i, int j) => $"{lODGroup.gameObject.name}_{j}_LOD_{i}";
    }
}
