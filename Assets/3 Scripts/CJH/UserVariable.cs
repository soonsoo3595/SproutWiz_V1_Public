using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 세이브해야되는 유저 데이터들은 여기
public class UserVariable
{
    // 인벤토리
    public List<SlotItem> seedInven = new List<SlotItem>();
    public List<SlotItem> scrollInven = new List<SlotItem>();
    public List<SlotItem> harvestInven = new List<SlotItem>();

    public ItemRetention itemRetention = new();

    // 재화
    public int gold;

    public int spell;

    public UserVariable() 
    {
        for(int i = 0; i < 20; i++)
        {
            seedInven.Add(new SlotItem());
            scrollInven.Add(new SlotItem());
            harvestInven.Add(new SlotItem());
        }

        gold = 50000;
        spell = 50;
    }

}
