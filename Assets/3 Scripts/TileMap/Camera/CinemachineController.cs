using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineController : MonoBehaviour
{
    //[SerializeField] float scrollSpeed = 10f;
    [SerializeField] float zoomSpeed = 50f;
    [SerializeField] float minZoomSize = 20f;
    [SerializeField] float maxZoomSize = 55f;

    CinemachineVirtualCamera cinemachineCamera;

    public float zoomSmoothness = 5f;
    private float initialDistance;
    private float targetOrthographicSize;

    private void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
        targetOrthographicSize = cinemachineCamera.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        Zoom();
        restrictZoomSize();
    }

    private void Zoom()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            ZoomPlayInWindow();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            zoomSpeed = 0.1f;

            ZoomPlayInAndroid();
        }
    }

    private void ZoomPlayInWindow()
    {
        float wheelAxis = CinemachineCore.GetInputAxis("Mouse ScrollWheel");

        //cinemachineCamera.m_Lens.OrthographicSize -= wheelAxis * zoomSpeed * Time.deltaTime;

        targetOrthographicSize -= wheelAxis * zoomSpeed;

        cinemachineCamera.m_Lens.OrthographicSize
            = Mathf.Lerp(cinemachineCamera.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSmoothness);
    }

    private void ZoomPlayInAndroid()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began)
            {
                initialDistance = Vector2.Distance(touch0.position, touch1.position);
            }
            else if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(touch0.position, touch1.position);
                float zoomAmount = (currentDistance - initialDistance) * zoomSpeed;

                targetOrthographicSize -= zoomAmount;
                initialDistance = currentDistance;
            }
        }

        cinemachineCamera.m_Lens.OrthographicSize 
            = Mathf.Lerp(cinemachineCamera.m_Lens.OrthographicSize, targetOrthographicSize, Time.deltaTime * zoomSmoothness);
    }

    private void restrictZoomSize()
    {
        if(targetOrthographicSize < minZoomSize)
        {
            targetOrthographicSize = minZoomSize;
        }
        if (targetOrthographicSize > maxZoomSize)
        {
            targetOrthographicSize = maxZoomSize;
        }

        if (cinemachineCamera.m_Lens.OrthographicSize < minZoomSize)
        {
            cinemachineCamera.m_Lens.OrthographicSize = minZoomSize;
        }
        if (cinemachineCamera.m_Lens.OrthographicSize > maxZoomSize)
        {
            cinemachineCamera.m_Lens.OrthographicSize = maxZoomSize;
        }
    }
}
