using System;
using Unity.VisualScripting;

[Serializable]
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

    public ProvinceData GetProvinceData() {
        return _provinceData;
    }

    public ProvinceMesh GetProvinceMesh() {
        return _provinceMesh;
    }

    public string GetDebugSummary() {
        return $"<color=#{_provinceData.Color.ToHexString()}>Province: {_provinceData.ID} - Population: {_provinceData.Population}</color>";
    }
}