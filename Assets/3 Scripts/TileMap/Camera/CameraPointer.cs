using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraPointer : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject cameraBorder;

    BoxCollider2D borderCollider;
    float minX, minY, maxX, maxY;

    Vector3 lastPosition;
    Vector3 newPosition;

    bool isDrag = false;
    bool isFreeze = false;
    bool debugMode;

    private void Awake()
    {
        CalculateBorderCoordinate();
    }

    private void Start()
    {
        MoveToCenter();
    }

    private void Update()
    {
        if (!isFreeze)
        {
            MoveToPointer();
        }
        RestrictPosition();

        SetDebugMode();
    }

    public void Freeze(bool toggle)
    {
        isFreeze = toggle;
    }

    private void CalculateBorderCoordinate()
    {
        borderCollider = cameraBorder.GetComponent<BoxCollider2D>();

        minX = borderCollider.offset.x - borderCollider.size.x / 2;
        maxX = borderCollider.offset.x + borderCollider.size.x / 2;
        minY = borderCollider.offset.y - borderCollider.size.y / 2;
        maxY = borderCollider.offset.y + borderCollider.size.y / 2;
    }

    private void MoveToCenter()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        transform.position = Camera.main.ScreenToWorldPoint(screenCenter);
    }

    private void MoveToPointer()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            MoveToMouseDrag();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            MoveToTouchDrag();
        }
    }

    private void MoveToMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 포인터가 UI위인지 확인
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDrag = true;
            }
        }

        if (isDrag)
        {
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            transform.position += (lastPosition - newPosition) * Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
        }
    }

    private void MoveToTouchDrag()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    lastPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    isDrag = true;
                }
            }

            if (isDrag)
            {
                newPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

                transform.position += (lastPosition - newPosition) * Time.deltaTime;
            }
        }

        if (Input.touchCount <= 0)
        {
            isDrag = false;
        }
    }

    private void RestrictPosition()
    {
        float width = mainCamera.orthographicSize * mainCamera.aspect;
        float height = mainCamera.orthographicSize;

        Vector2 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX + width, maxX - width);
        pos.y = Mathf.Clamp(pos.y, minY + height, maxY - height);

        transform.position = pos;

        if (debugMode)
        {
            Debug.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, maxY, 0));
            Debug.DrawLine(new Vector3(maxX - width, maxY - height, 0),
                           new Vector3(minX + width, minY + height, 0), Color.red);
        }
    }

    private void SetDebugMode()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            debugMode = !debugMode;
        }

        if (debugMode)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
