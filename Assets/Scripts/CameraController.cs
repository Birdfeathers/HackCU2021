using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera thisCamera;
    private const float SCROLL_SCALE = 1.5f;
    private Vector2 lastMouse;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
        lastMouse = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        bool mouseIsInBounds =
            !(
            Input.mousePosition.x <= 0 ||
            Input.mousePosition.y <= 0 ||
            Input.mousePosition.x >= Screen.width ||
            Input.mousePosition.y >= Screen.height
            );
        if (mouseIsInBounds)
        { 
            thisCamera.orthographicSize -= SCROLL_SCALE * Input.GetAxis("Mouse ScrollWheel") * thisCamera.orthographicSize; 
        }
        Vector2 newMouse = thisCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButton("LeftClick"))
        {
            transform.Translate(lastMouse - newMouse);
            newMouse = thisCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        lastMouse = newMouse;
    }
}
