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
    public int seedCost;    // ���� ���� �� ����

    [Header("Fire")]
    public Sprite[] firePlants;
    public HarvestItem fireHarvest;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          

    [Header("Water")]
    public Sprite[] waterPlants;
    public HarvestItem waterHarvest;

    [Header("Grass")]
    public Sprite[] grassPlants;
    public HarvestItem grassHarvest;

    public int harvestCost; // ��Ȯ�� �Ǹ� �� ����

    public int GetMaxGrowPoint() => grade + 3;
}
