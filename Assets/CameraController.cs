using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private new Camera camera;
    private const float SCROLL_SCALE = 3f;
    private Vector2 lastMouse;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        lastMouse = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize -= SCROLL_SCALE * Input.GetAxis("Mouse ScrollWheel");
        Vector2 newMouse = camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButton("LeftClick"))
        {
            transform.Translate(lastMouse - newMouse);
            newMouse = camera.ScreenToWorldPoint(Input.mousePosition);
        }
        lastMouse = newMouse;
    }
}
