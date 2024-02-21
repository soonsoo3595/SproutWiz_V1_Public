using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPreView : MonoBehaviour
{
    [SerializeField] OnGridHandle moveHandle;

    ScrollData scrollData;
    List<Node> nodes = new List<Node>();
    List<Node> activeNodes = new List<Node>();

    GameObject ScrollBlock;
    ItemData itemData;
    Element element;

    int currentDirection = 0;

    private void Awake()
    {
        moveHandle = FindObjectOfType<OnGridHandle>();
        scrollData = new ScrollData();
    }

    public void Creat(ItemData item)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        element = Element.Non;

        if (item.itemType == ItemType.Seed)
        {
            SeedItem seed = item as SeedItem;
            ScrollBlock = Instantiate(seed.block, mousePos, Quaternion.identity);
            itemData = seed;
        }
        else if(item.itemType == ItemType.Scroll)
        {
            ScrollItem scroll = item as ScrollItem;
            ScrollBlock = Instantiate(scroll.block, mousePos, Quaternion.identity);
            itemData = scroll;
            element = scroll.element;
        }
    }

    public bool DropScroll()
    {
        if(ScrollBlock == null) return false;
        scrollData = ScrollBlock.GetComponent<Scroll>().GetData();

        if (scrollData == null)
        {
            Cancel();
            Destroy(ScrollBlock);
            return false;
        }

        GridManager.instance.curScrollSize = scrollData.Size;

        if (itemData.itemType == ItemType.Seed)
        {
            scrollData.Size = new Vector2Int(0, 0);
            GridManager.instance.curScrollSize = scrollData.Size;
        }

        // 맵을 벗어나는치 체크
        if (!DeployableArea(scrollData.Size))
        {
            Cancel();
            Destroy(ScrollBlock);
            return false;
        }


        bool success = SetOnTile();

        if(success)
            moveHandle.SetHandle(scrollData);

        Destroy(ScrollBlock);

        return success;
    }

    private bool SetOnTile()
    {
        currentDirection = 0;
        nodes.Clear();
        activeNodes.Clear();

        for (int y = 0; y <= scrollData.Size.y; y++)
        {
            for (int x = 0; x <= scrollData.Size.x; x++)
            {
                Vector2Int tagetCoor = new Vector2Int(x, y) + scrollData.Axis;
                Node node = GridManager.instance.GetNode(tagetCoor);

                nodes.Add(node);
                node.ToggleOnPreView(true, element);

                if (scrollData.LocalCoor[new Vector2Int(x, y)].isAtive)
                {
                    if (node == null) return false;

                    // 씨앗은 비활성화 타일에 배치 불가
                    if (itemData.itemType == ItemType.Seed && !node.isActivate)
                    {
                        ToastMessage.instance.ShowToast("씨앗을 밭 위에 배치하세요");
                        return false;
                    }

                    node.ToggleIsActiveBlock(true, element);
                    activeNodes.Add(node);
                }
                else
                {
                    node.ToggleIsActiveBlock(false, element);
                }
            }
        }

        if (nodes.Count > 0) return true;
        else return false;
    }

    public void Move(Vector2Int dir)
    {
        List<Node> newNodes = new List<Node>();
        List<Node> newActiveNodes = new List<Node>();

        foreach (Node node in nodes)
        {
            node.ToggleOnPreView(false, element);
        }

        foreach (Node node in activeNodes)
        {
            node.ToggleIsActiveBlock(false, element);
        }

        foreach (Node node in nodes)
        {
            Vector2Int newPos = node.coordinates + dir;

            Node newNode = GridManager.instance.GetNode(newPos);
            newNode.ToggleOnPreView(true, element);

            newNodes.Add(newNode);
        }

        foreach(Node node in activeNodes)
        {
            Vector2Int newPos = node.coordinates + dir;

            Node newNode = GridManager.instance.GetNode(newPos);
            newNode.ToggleIsActiveBlock(true, element);

            newActiveNodes.Add(newNode);
        }

        nodes.Clear();
        activeNodes.Clear();
        nodes = newNodes;
        activeNodes = newActiveNodes;
    }

    public void Rotate(int type)
    {
        if (itemData.itemType == ItemType.Seed) return;

        Dictionary<Vector2Int, BlockUnit> rotatedScrollBlock = new Dictionary<Vector2Int, BlockUnit>();

        // 맵을 벗어나는치 체크
        if (!DeployableArea(scrollData.Size)) return;

        foreach (Node node in nodes)
        {
            node.ToggleOnPreView(false, element);
        }
        foreach (Node node in activeNodes)
        {
            node.ToggleIsActiveBlock(false, element);
        }

        nodes.Clear();
        activeNodes.Clear();

        for (int y = 0; y < scrollData.Size.y + 1; y++)
        {
            for (int x = 0; x < scrollData.Size.x + 1; x++)
            {
                //왼쪽으로 회전. 4x4 행렬-열행 교체
                Vector2Int tagetCoor = new Vector2Int((scrollData.Size.y) - y, x);

                Node node = GridManager.instance.GetNode(tagetCoor + scrollData.Axis);
                rotatedScrollBlock[tagetCoor] = scrollData.LocalCoor[new Vector2Int(x, y)];


                node.ToggleOnPreView(true, element);
                nodes.Add(node);

                if (scrollData.LocalCoor[new Vector2Int(x, y)].isAtive)
                {
                    node.ToggleIsActiveBlock(true, element);
                    activeNodes.Add(node);
                }
                else
                {
                    node.ToggleIsActiveBlock(false, element);
                }
            }
        }

        // 행열-열행 교체
        scrollData.Size = new Vector2Int(scrollData.Size.y, scrollData.Size.x);

        // Move 제한을 위한 값 최신화
        GridManager.instance.curScrollSize = scrollData.Size;

        currentDirection++;
        if (currentDirection > 3)
        {
            currentDirection = 0;
        }

        scrollData.LocalCoor = rotatedScrollBlock;
    }

    private bool DeployableArea(Vector2Int size)
    {
        // 돌리기 전에 가능한지 여부 판단.
        for (int y = 0; y < size.y + 1; y++)
        {
            for (int x = 0; x < size.x + 1; x++)
            {
                Vector2Int tagetCoor = new Vector2Int((size.y) - y, x);

                Node node = GridManager.instance.GetNode(tagetCoor + scrollData.Axis);
                if (node == null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool ApplyToTile()
    {
        List<bool> isSuccess = new List<bool>();

        foreach (Node node in nodes)
        {
            node.ToggleOnPreView(false, element);

            if (node.isActivate && node.IsActiveBlock)
            {
                if (itemData.itemType == ItemType.Seed)
                {
                    isSuccess.Add(GridManager.instance.ApplySeedItem(node, itemData.id));
                }
                else if (itemData.itemType == ItemType.Scroll)
                {
                    isSuccess.Add(GridManager.instance.ApplyScrollitem(node, itemData));
                }
                else
                {
                    isSuccess.Add(false);
                }
            }

            isSuccess.Add(false);
        }


        foreach (Node node in activeNodes)
        {
            node.ToggleIsActiveBlock(false, element);
        }

        foreach (bool success in isSuccess)
        {
            if (success)
            {
                if(itemData.itemType == ItemType.Scroll)
                    GameMgr.Instance.soundEffect.PlayScroll(itemData);

                moveHandle.DisableHandle();
                nodes.Clear();
                activeNodes.Clear();
                return true;
            }
        }

        if (itemData.itemType == ItemType.Scroll)
            ToastMessage.instance.ShowToast("적용 가능한 작물이 없습니다!");

        moveHandle.DisableHandle();
        return false;
    }

    public void Cancel()
    {
        foreach (Node node in nodes)
        {
            node.ToggleOnPreView(false, element);
        }

        foreach (Node node in activeNodes)
        {
            node.ToggleIsActiveBlock(false, element);
        }

        moveHandle.DisableHandle();
    }
}
