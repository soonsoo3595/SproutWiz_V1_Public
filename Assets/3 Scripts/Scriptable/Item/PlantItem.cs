using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Item/Plant")]
public class PlantItem : ScriptableObject
{
    public int id;
    public int grade;

    [Header("Seed")]
    public SeedItem seed;
    public int seedCost;    // 씨앗 구매 시 가격

    [Header("Fire")]
    public Sprite[] firePlants;
    public HarvestItem fireHarvest;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          

    [Header("Water")]
    public Sprite[] waterPlants;
    public HarvestItem waterHarvest;

    [Header("Grass")]
    public Sprite[] grassPlants;
    public HarvestItem grassHarvest;

    public int harvestCost; // 수확물 판매 시 가격

    public int GetMaxGrowPoint() => grade + 3;
}
