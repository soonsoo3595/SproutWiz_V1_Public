using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScrollData
{
    public Dictionary<Vector2Int, BlockUnit> LocalCoor { get; set; }
    public Vector2Int Axis { get; set; }
    public Vector2Int Size { get; set; }
}

public class Scroll : MonoBehaviour
{
    ScrollData scrollData = new ScrollData();

    BlockUnit[] blockUnits;
    Collider2D onCollisionTiles;

    public bool isOutOfInventory;

    private void Start()
    {
        blockUnits = GetComponentsInChildren<BlockUnit>();

        //프리펩에서 타일 정렬이 깨졌을 경우 아래 정렬코드 사용.
        //Array.Sort(blockUnits, CompareBlockUnits);

        MakeLocalCoor();
        gameObject.transform.localScale = Vector3.zero;
    }

    public List<bool> GetTest()
    {
        List<bool> returnVelue = new List<bool>();
        
        blockUnits = GetComponentsInChildren<BlockUnit>();

        int index = 0;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                returnVelue.Add(blockUnits[index].GetState());
                index++;
            }
        }

        return returnVelue;
    }

    private void Update()
    {
        FollowingMousePoint();
        CheckInventoryRange();
    }

    private void MakeLocalCoor()
    {
        int index = 0;
        Vector2Int temp = scrollData.Size;
        temp.x = 2; temp.y = 2;
        scrollData.LocalCoor = new Dictionary<Vector2Int, BlockUnit>();

        int tempSize = blockUnits.Length / 4;
        if (tempSize == 0) tempSize = 1;

        for (int y = 0; y < tempSize; y++)
        {
            for (int x = 0; x < tempSize; x++)
            {
                if (blockUnits[index].isAtive)
                {
                    if (x > temp.x) temp.x = x;
                    if (y > temp.y) temp.y = y;
                }

                Vector2Int coor = new Vector2Int(x, y);
                scrollData.LocalCoor.Add(coor, blockUnits[index]);
                index++;
            }
        }
        scrollData.Size = temp;

        SetOutLine();
    }

    private void SetOutLine()
    {
        Vector2Int temp = scrollData.Size;

        int index = 0;
        for (int y = 0; y < blockUnits.Length / 4; y++)
        {
            for (int x = 0; x < blockUnits.Length / 4; x++)
            {
                if(x <= temp.x && y <= temp.y)
                {
                    blockUnits[index].SetSize(true);
                }
                else
                {
                    blockUnits[index].SetSize(false);
                }

                index++;
            }
        }
    }

    private void FollowingMousePoint()
    {
        Vector2 currentPos = transform.position;
        Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
        Vector2 velo = Vector2.zero;

        transform.position = Vector2.SmoothDamp(currentPos, newPos, ref velo, 0.01f, 300f);
    }

    private void CheckInventoryRange()
    {
        if (transform.position.x > InventoryMgr.instance.OutOfInventotyRect())
        {
            isOutOfInventory = true;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            isOutOfInventory = false;
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("UI"))
            onCollisionTiles = collision;
    }

    public ScrollData GetData()
    {
        if (!isOutOfInventory || onCollisionTiles == null)
        {
            return null;
        }
        else
        {
            scrollData.Axis = onCollisionTiles.GetComponent<Tile>().coordinates;

            return scrollData;
        }
    }

    private int CompareBlockUnits(BlockUnit a, BlockUnit b)
    {
        Vector3 aPos = a.transform.position;
        Vector3 bPos = b.transform.position;

        int yComparison = aPos.y.CompareTo(bPos.y);
        if (yComparison != 0)
            return yComparison;

        return aPos.x.CompareTo(bPos.x);
    }
}
