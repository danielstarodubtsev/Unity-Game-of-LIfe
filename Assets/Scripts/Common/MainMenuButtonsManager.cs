using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonsManager : MonoBehaviour
{
    public void OnOnePlayerButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnTwoPlayersButtonClick()
    {
        SceneManager.LoadScene(2);
    }

    public void OnHowToPlayButtonClick()
    {
        SceneManager.LoadScene(3);
    }

    public void OnAdditionInfoButtonClick()
    {
        SceneManager.LoadScene(4);
    }
}