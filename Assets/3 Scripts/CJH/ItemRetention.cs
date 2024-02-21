using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRetention
{
    public Dictionary<string, int> seed = new();
    public Dictionary<string, int> harvest = new();
    public Dictionary<string, int> material = new();

    public void Push(string id, int count)
    {
        if (id == null) return;

        switch (id[0]) 
        {
            case 'S':
                if(seed.ContainsKey(id))
                {
                    seed[id] += count;
                }
                else
                {
                    seed.Add(id, count);
                }
                break;
            case 'H':
                if (harvest.ContainsKey(id))
                {
                    harvest[id] += count;
                }
                else
                {
                    harvest.Add(id, count);
                }
                break;
            case 'M':
                if(material.ContainsKey(id))
                {
                    material[id] += count;
                }
                else
                {
                    material.Add(id, count);
                }
                break;
        }
    }

    public int Get(string id)
    {
        char key = id[0];

        switch (key)
        {
            case 'S':
                if (seed.ContainsKey(id))
                {
                    return seed[id];
                }
                else
                {
                    return 0;
                }
            case 'H':
                if (harvest.ContainsKey(id))
                {
                    return harvest[id];
                }
                else
                {
                    return 0;
                }
            case 'M':
                if (material.ContainsKey(id))
                {
                    return material[id];
                }
                else
                {
                    return 0;
                }
            default:
                return 0;
        }
    }

}
