using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Camera cam;
    [SerializeField]
    private float camMoveSpeed;
    [SerializeField]
    private float minPosX, maxPosX, minPosY, maxPosY;
    [Space]
    [SerializeField]
    private float camZoomSpeed;
    [SerializeField]
    private float minCamSize, maxCamSize;

    private bool pointerIsDown = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        CheckTouches();
    }

    private void CheckTouches()
    {
        if (pointerIsDown)
        {
            if (Input.touchCount == 1)
            {
                cam.transform.position += (Vector3)((Vector2)(
                    cam.ScreenToWorldPoint(Input.GetTouch(0).position) - 
                    cam.ScreenToWorldPoint(Input.GetTouch(0).deltaPosition + Input.GetTouch(0).position
                    )) * camMoveSpeed);

                if (cam.transform.position.x < minPosX) cam.transform.position = new Vector3(minPosX, cam.transform.position.y, cam.transform.position.z);
                else if (cam.transform.position.x > maxPosX) cam.transform.position = new Vector3(maxPosX, cam.transform.position.y, cam.transform.position.z);

                if (cam.transform.position.y < minPosY) cam.transform.position = new Vector3(cam.transform.position.x, minPosY, cam.transform.position.z);
                else if (cam.transform.position.y > maxPosY) cam.transform.position = new Vector3(cam.transform.position.x, maxPosY, cam.transform.position.z);
            }
            else if (Input.touchCount > 1)
            {
                cam.orthographicSize += (
                    Vector2.Distance(cam.ScreenToWorldPoint(Input.GetTouch(0).position),
                    cam.ScreenToWorldPoint(Input.GetTouch(1).position)) - 
                    Vector2.Distance(cam.ScreenToWorldPoint(Input.GetTouch(0).deltaPosition + Input.GetTouch(0).position),
                    cam.ScreenToWorldPoint(Input.GetTouch(1).deltaPosition + Input.GetTouch(1).position))
                    ) * camZoomSpeed;

                if (cam.orthographicSize > maxCamSize) cam.orthographicSize = maxCamSize;
                else if (cam.orthographicSize < minCamSize) cam.orthographicSize = minCamSize;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerIsDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerIsDown = false;
    }
}
