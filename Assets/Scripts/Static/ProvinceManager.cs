using System;
using UnityEngine;
using System.Collections.Generic;
using Factory;
using Unity.VisualScripting;
using ColorUtility = UnityEngine.ColorUtility;

public static class ProvinceManager {
    
    private static readonly Texture2D PROVINCE_TEXTURE = Resources.Load<Texture2D>("provinces");
    
    private static Dictionary<string, List<Vector2>> GenerateProvinceMap(Texture2D texture) {
        Dictionary<string, List<Vector2>> provinceMap = new();
        for (int x = 0; x < texture.width; x++) {
            for (int y = 0; y < texture.height; y++) {
                string key = texture.GetPixel(x, y).ToHexString();
                Vector2 coordinate = new Vector2(x,y);

                if (provinceMap.ContainsKey(key)) {
                    provinceMap[key].Add(coordinate);
                } else {
                    provinceMap[key] = new List<Vector2> {coordinate};
                }
            }
        }
        return provinceMap;
    }

    private static List<GameObject> GenerateProvinces() {
        var provinceMap = GenerateProvinceMap(PROVINCE_TEXTURE);
        var provinces = new List<GameObject>();
        

        foreach (var key in provinceMap.Keys) {
            var contour = ProvinceContourExtractor.TraceProvince(PROVINCE_TEXTURE,$"#{key}");
            try {
                ColorUtility.TryParseHtmlString($"#{key}", out Color color);
                provinces.Add(ProvinceFactory.Create(contour, color));
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
        }

        return provinces;
    }
    
    public static void DisplayProvinces() {
        GameObject provinceParent = new GameObject("Provinces");
        provinceParent.tag = "PROVINCE_PARENT";
        
        var provinces = GenerateProvinces();

        foreach (var province in provinces) {
            province.transform.SetParent(provinceParent.transform);
        }
    }
}
