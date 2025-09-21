using System;
using Factory;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Province {
    public ProvinceData provinceData;
    public ProvinceMesh provinceMesh;

    public Province(ProvinceData provinceData, ProvinceMesh provinceMesh) {
        this.provinceData = provinceData;
        this.provinceMesh = provinceMesh;
    }

    public int IncreasePopulation(int amount) {
        provinceData.IncreasePopulation(amount);
        return provinceData.Population;
    }

    public string GetDebugSummary() {
        return $"<color=#{provinceData.Color.ToHexString()}>Province: {provinceData.ID} - Population: {provinceData.Population}</color>";
    }

    public GameObject GenerateGameObject() {
        return ProvinceFactory.GenerateGameObject(this);
    }
}