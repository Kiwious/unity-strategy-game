using System;
using System.Collections;
using Factory;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Province {
    public ProvinceData provinceData;
    public Mesh provinceMesh;

    public Province(ProvinceData provinceData, Mesh provinceMesh) {
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

    public IEnumerator IncreasePopulationRoutine() {
        while (true) {
            IncreasePopulation(100);
            yield return new WaitForSeconds(5f);
        }
    }
}