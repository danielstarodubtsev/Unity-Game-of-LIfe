using UnityEngine;

public class CameraPositionManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float moveSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cam.transform.Translate(new Vector3(1, 0, 0) * moveSpeed * cam.orthographicSize);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cam.transform.Translate(new Vector3(-1, 0, 0) * moveSpeed * cam.orthographicSize);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cam.transform.Translate(new Vector3(0, 1, 0) * moveSpeed * cam.orthographicSize);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cam.transform.Translate(new Vector3(0, -1, 0) * moveSpeed * cam.orthographicSize);
        }
    }
}
