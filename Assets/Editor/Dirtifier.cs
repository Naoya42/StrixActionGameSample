using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Dirtifier : MonoBehaviour
{
    [MenuItem("Assets/Dirtify")]
    public static void Dirtify() {
        foreach (var assetPath in AssetDatabase.GetAllAssetPaths()) {
            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            EditorUtility.SetDirty(asset);
        }

        AssetDatabase.SaveAssets();
    }
}
