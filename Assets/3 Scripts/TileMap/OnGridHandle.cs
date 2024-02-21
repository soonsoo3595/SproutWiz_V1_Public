using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGridHandle : MonoBehaviour
{
    [SerializeField] CameraPointer myCamera;

    Vector3 lastPosition;
    Vector3 newPosition;

    Vector2Int lastBoxPos;
    ScrollData scrollData;

    Vector2Int gridSize;
    int unitySize;

    private void Start()
    {
        gameObject.SetActive(false);

        gridSize = GridManager.instance.gridSize;
        unitySize = GridManager.instance.unityGridSize;
    }

    private void OnEnable()
    {
        myCamera = FindObjectOfType<CameraPointer>();
    }

    public void SetHandle(ScrollData data)
    {
        scrollData = data;
        gameObject.SetActive(true);
        transform.position = ((Vector3Int)data.Axis) * 10;
    }

    public void DisableHandle()
    {
        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        SetLastBoxPos();

        myCamera.Freeze(true);
    }

    private void OnMouseDrag()
    {
        newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 deltaPos = newPosition - lastPosition;

        if (deltaPos.magnitude >= 10)
        {
            SnapMove();
        }
    }

    private void OnMouseUp()
    {
        myCamera.Freeze(false);
    }

    private void SnapMove()
    {
        int snapx = Mathf.RoundToInt(newPosition.x / 10) * 10;
        int snapy = Mathf.RoundToInt(newPosition.y / 10) * 10;
        Vector2Int targetPos = new Vector2Int(snapx, snapy);

        if (restrictMoveTo(targetPos)) return;

        transform.position = (Vector3Int)targetPos;
        MoveScroll(targetPos);
        lastPosition = newPosition;
        SetLastBoxPos();
    }

    private bool restrictMoveTo(Vector2Int targetPos)
    {
        // Á¶°Ç¹® Á¤¸® ÇÊ¿ä.
        Vector2Int curScrollSize = GridManager.instance.curScrollSize;

        targetPos /= unitySize;

        if (targetPos.x < 0 || targetPos.y < 0)
            return true;

        if (curScrollSize.x == 0 && curScrollSize.y == 0) // ¾¾¾Ñ
        {
            Node node = GridManager.instance.GetNode(targetPos);
            if (node == null) return true;

            if (!node.isActivate)
            {
                return true;
            }
            if (targetPos.x > gridSize.x - 1 || targetPos.y > gridSize.y - 1)
            {
                return true;
            }
        }

        if (targetPos.x >= gridSize.x - curScrollSize.x || targetPos.y >= gridSize.y - curScrollSize.y)
        {
            return true;
        }


        return false;
    }

    private void MoveScroll(Vector2Int temp)
    {
        Vector2Int moveDir = (temp / 10) - lastBoxPos;

        scrollData.Axis += moveDir;
        GridManager.instance.MovePreView(moveDir);
    }

    private void SetLastBoxPos()
    {
        lastBoxPos.x = Mathf.RoundToInt(transform.position.x / 10);
        lastBoxPos.y = Mathf.RoundToInt(transform.position.y / 10);
    }
}
