using System;
using UnityEngine;

[Serializable]
public class ProvinceData
{
    public Color Color { get; private set; }
    public int Population { get; private set; }
    public int ID;

    public ProvinceData(Color color, int population, int id)
    {
        Color = color;
        Population = population;
        ID = id;
    }

    public void IncreasePopulation(int amount)
    {
        Population += amount;
        Debug.Log($"Increased population is province {ID} by {amount}; New Population is {Population}");
    }
}