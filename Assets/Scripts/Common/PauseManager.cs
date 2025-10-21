using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool paused = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            paused = !paused;
        }
    }
}
