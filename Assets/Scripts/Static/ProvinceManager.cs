using System;
using UnityEngine;
using System.Collections.Generic;
using Factory;
using Unity.VisualScripting;
using ColorUtility = UnityEngine.ColorUtility;

public static class ProvinceManager {
    private static readonly Texture2D PROVINCE_TEXTURE = Resources.Load<Texture2D>("provinces");
    private static readonly Dictionary<string, List<int>> STATE_CONFIG = Json.Load<Dictionary<string, List<int>>>("states");
    private static readonly List<Province> provinceList;
    private static readonly Dictionary<State, List<Province>> stateProvinceMap = new();
    
    static ProvinceManager() {
        provinceList = GenerateProvinces();
        GenerateStates();
    }
    
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

    private static List<Province> GenerateProvinces() {
        var provinceMap = GenerateProvinceMap(PROVINCE_TEXTURE);
        var provinces = new List<Province>();
        

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

    private static void GenerateStates() {
        foreach (var kvp in STATE_CONFIG) {
            string stateId = kvp.Key;
            List<int> provinceIds = kvp.Value;

            List<Province> provincesInState = new List<Province>();

            foreach (int index in provinceIds) {
                if (index >= 0 && index < provinceList.Count) {
                    provincesInState.Add(provinceList[index]);
                }
            }
            State state = StateFactory.Create(provincesInState, int.Parse(stateId));
            stateProvinceMap[state] = provincesInState;
        }
    }
    
    public static void DisplayMap() {
        GameObject mapParentGameObject = new GameObject("MAP");
        
        foreach (var kvp in stateProvinceMap) {
            State state = kvp.Key;
            List<Province> assignedProvinces = kvp.Value;

            GameObject stateGameObject = state.GetGameObject();
            
            stateGameObject.transform.SetParent(mapParentGameObject.transform);

            foreach (var province in assignedProvinces) {
                GameObject provinceGameObject = province.GetGameObject();
                provinceGameObject.transform.SetParent(stateGameObject.transform);
            }
        }
    }
}
