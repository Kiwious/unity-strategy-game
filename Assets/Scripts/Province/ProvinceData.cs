using UnityEngine;

public class ProvinceData
{
    public Color Color { get; private set; }
    public int Population { get; private set; }
    public string Name { get; private set; } // optional

    public ProvinceData(Color color, int population, string name)
    {
        Color = color;
        Population = population;
        Name = name;
    }

    public void IncreasePopulation(int amount)
    {
        Population += amount;
    }
}