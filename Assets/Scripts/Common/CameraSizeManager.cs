using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minSize, maxSize);
        }
    }
}
