using System.Collections.Generic;
using UnityEngine;

public class Province {
    private readonly ProvinceData _provinceData;
    private readonly ProvinceMesh _provinceMesh;

    public Province(ProvinceData provinceData, ProvinceMesh provinceMesh) {
        _provinceData = provinceData;
        _provinceMesh = provinceMesh;
    }

    public int IncreasePopulation(int amount) {
        _provinceData.IncreasePopulation(amount);
        return _provinceData.Population;
    }

    public GameObject GetGameObject() {
        GameObject go = new GameObject($"Province-{_provinceData.Name}");
        MeshFilter mf = go.AddComponent<MeshFilter>();
        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        
        Material provinceMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
        provinceMaterial.color = _provinceData.Color;

        mf.mesh = _provinceMesh.GetMesh();
        mr.material = provinceMaterial;
        go.tag = "PROVINCE";
        return go;
    }
}