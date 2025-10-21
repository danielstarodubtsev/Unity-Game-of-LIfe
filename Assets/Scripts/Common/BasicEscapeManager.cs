using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicEscapeManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void OnEscapeButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
