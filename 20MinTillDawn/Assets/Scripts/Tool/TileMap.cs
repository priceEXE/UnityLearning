using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TileMapEditor : MonoBehaviour
{
    public List<TileBase> tileBases = new List<TileBase>();
    private void LoadTileBases()
    {
        string folderPath = "Assets/TileMap/Tiles"; // 替换为你的文件夹路径

        // 获取指定文件夹中的所有 TileBase 资源
        string[] assetPaths = AssetDatabase.FindAssets("t:TileBase", new[] { folderPath });
        
        foreach (string assetPath in assetPaths)
        {
            string fullPath = AssetDatabase.GUIDToAssetPath(assetPath);
            TileBase tileBase = AssetDatabase.LoadAssetAtPath<TileBase>(fullPath);
            
            if (tileBase != null)
            {
                tileBases.Add(tileBase);
                Debug.Log("Loaded TileBase: " + tileBase.name);
            }
        }
    }

    void Awake() {
        LoadTileBases();
    }

    public TileBase GetTileBase(int index)
    {
        if(index > 0 && index < tileBases.Count)
        {
            return tileBases[index];
        }
        else
        {
            return null;
        }
            
    }


}
