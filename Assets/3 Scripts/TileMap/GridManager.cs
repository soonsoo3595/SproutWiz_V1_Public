using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Setting")]
    [SerializeField] public Vector2Int gridSize;
    [SerializeField] Vector2Int activeLeftDown, activeRightTop;
    [HideInInspector] public int unityGridSize = 10;

    [SerializeField] ImageSerializer imageSerializer;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    ScrollPreView preView;
    Slot slot;

    public RotateTool rotateTool;
    public Vector2Int curScrollSize;
    private int gridUpgradeLevel = 0;

    [HideInInspector] public bool isPreview { get; set; }

    public static GridManager instance;

    private void Awake()
    {
        // TODO: 맵 데이터를 로드해 오는 방식으로 바꿔야함.
        if(instance == null)
        {
            CreatGrid();
            instance = this;
        }

        preView = GetComponent<ScrollPreView>();
    }

    private void CreatGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinate = new Vector2Int(x, y);
                bool activate = false;

                if(x >= activeLeftDown.x && x <= activeRightTop.x && y >= activeLeftDown.y && y <= activeRightTop.y)
                {
                    activate = true;
                }

                grid.Add(coordinate, new Node(coordinate, activate));
            }
        }
    }

    public void CreatPreView(ItemData item, Slot slot)
    {
        this.slot = slot;

        InventoryMgr.instance.TempPreview.SetActive(false);

        preView.Creat(item);
    }

    public void DropScrollBlock()
    {
        bool success = preView.DropScroll();

        if (success) rotateTool.Raise();

        FarmInputMgr.instance.IsControlling = success;
    }

    public void RotatePreView(int type)
    {
        preView.Rotate(type);
    }

    public void MovePreView(Vector2Int pos)
    {
        preView.Move(pos);
    }

    public void CancelPreView()
    {
        preView.Cancel();

        FarmInputMgr.instance.IsControlling = false;
    }

    public void ApplyPreView()
    {
        bool success = preView.ApplyToTile();

        if (success)
        {
            slot.UseItem();
        }

        FarmInputMgr.instance.IsControlling = false;
    }

    public bool ApplySeedItem(Node node, string pid)
    {
        if (node.growthStep != Growth.Non) return false;

        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("grow");

        node.plant = GameMgr.Plants.Get(pid);
        node.maxGrowPoint = node.plant.GetMaxGrowPoint();

        node.IncreaseGrowPoint(1);
        imageSerializer.UpdateImage(node);

        return true;
    }

    public bool ApplyScrollitem(Node node, ItemData item)
    {
        if (item.itemType != ItemType.Scroll) return false;
        if (node.growthStep == Growth.Non || node.growthStep == Growth.Bloom) return false;

        ScrollItem scroll = item as ScrollItem;

        if (node.growthStep == Growth.Seed)
        {
            node.IncreaseGrowPoint(scroll.tier);
            node.element = scroll.element;
        }
        else if(node.element == scroll.element)
        {
            if(Random.value <= 0.4f)
            {
                node.IncreaseGrowPoint(scroll.tier + 1);
            }
            else
            {
                node.IncreaseGrowPoint(scroll.tier);
            }
        }
        else if(node.element != scroll.element)
        {
            if ((int)(node.element)%3 == (int)(scroll.element -1) % 3)
            {
                node.DecreaseGrowPoint(scroll.tier + 1);
            }
            else
            {
                node.IncreaseGrowPoint(scroll.tier);
            }
        }
        else
        {
            return false;
        }
        
        imageSerializer.UpdateImage(node);

        return true;
    }

    public void HarvestCrop(Node node)
    {
        HarvestItem harvest = null;

        if (node.element == Element.Fire)
            harvest = node.plant.fireHarvest;
        else if (node.element == Element.Water)
            harvest = node.plant.waterHarvest;
        else if (node.element == Element.Grass)
            harvest = node.plant.grassHarvest;

        GameMgr.Instance.soundEffect.PlayOneShotSoundEffect("harvest");
        InventoryMgr.instance.AcquireItem(harvest);

        node.resetNode();
        imageSerializer.UpdateImage(node);
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    public void ExpandGrid()
    {
        if (gridUpgradeLevel > 13) return;

        gridUpgradeLevel++;

        int expansionDirection = gridUpgradeLevel % 4;
        Vector2Int rangeOfLeftDown = new Vector2Int(0, 0);
        Vector2Int rangeOfRightTop = new Vector2Int(0, 0);

        switch (expansionDirection)
        {
            case 1:
                activeLeftDown.x--;
                rangeOfLeftDown = new Vector2Int(activeLeftDown.x, activeLeftDown.y);
                rangeOfRightTop = new Vector2Int(activeLeftDown.x, activeRightTop.y);
                break;
            case 2:
                activeLeftDown.y--;
                rangeOfLeftDown = new Vector2Int(activeLeftDown.x, activeLeftDown.y);
                rangeOfRightTop = new Vector2Int(activeRightTop.x, activeLeftDown.y);
                break;
            case 3:
                activeRightTop.x++;
                rangeOfLeftDown = new Vector2Int(activeRightTop.x, activeLeftDown.y);
                rangeOfRightTop = new Vector2Int(activeRightTop.x, activeRightTop.y);
                break;
            case 0:
                activeRightTop.y++;
                rangeOfLeftDown = new Vector2Int(activeLeftDown.x, activeRightTop.y);
                rangeOfRightTop = new Vector2Int(activeRightTop.x, activeRightTop.y);
                break;
            default:
                break;
        }

        for (int x = rangeOfLeftDown.x; x <= rangeOfRightTop.x; x++)
        {
            for (int y = rangeOfLeftDown.y; y <= rangeOfRightTop.y; y++)
            {
                Vector2Int coordinate = new Vector2Int(x, y);

                grid[coordinate].ToggleActivate();
            }
        }
    }
}
