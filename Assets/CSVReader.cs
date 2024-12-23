using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    [System.Serializable]
    public class BlockType
    {
        public string typeName;
        public GameObject prefab;
    }

    public List<BlockType> blockTypes; // ブロック種類とPrefabのマッピング

    private Dictionary<string, GameObject> blockDictionary;

    void Start()
    {
        // ブロックの種類を辞書に変換
        blockDictionary = new Dictionary<string, GameObject>();
        foreach (var blockType in blockTypes)
        {
            blockDictionary[blockType.typeName] = blockType.prefab;
        }

        LoadBlocksFromCSV("data");
    }

    void LoadBlocksFromCSV(string fileName)
    {
        // Resources フォルダ内のCSVを読み込む
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null)
        {
            Debug.LogError("CSV file not found!");
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        for (int i = 1; i < lines.Length; i++) // 1行目はヘッダー
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] values = line.Split(',');

            string type = values[0];
            float posX = float.Parse(values[1]);
            float posY = float.Parse(values[2]);
            float posZ = float.Parse(values[3]);

            Vector3 position = new Vector3(posX, posY, posZ);

            // 辞書からPrefabを取得して生成
            if (blockDictionary.TryGetValue(type, out GameObject prefab))
            {
                Instantiate(prefab, position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"Block type '{type}' not found!");
            }
        }
    }
}
