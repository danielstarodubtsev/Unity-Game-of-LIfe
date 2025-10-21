using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Collections;

public class WinManager : MonoBehaviour
{
    [SerializeField] Grid grid;
    [SerializeField] GameObject resultsPanel;
    [SerializeField] TMP_Text text;

    void Start()
    {
        resultsPanel.SetActive(false);
    }

    void Update()
    {
        TwoPlayersGameLogicManager manager = grid.GetComponent<TwoPlayersGameLogicManager>();

        for (int y = 0; y < manager.height; ++y)
        {
            if (manager.states[y][0] == 2)
            {
                PlayerWins(2);
            }

            if (manager.states[y][manager.width - 1] == 1)
            {
                PlayerWins(1);
            }
        }
    }

    void PlayerWins(int player)
    {
        resultsPanel.SetActive(true);
        text.SetText((player == 1 ? "Красный" : "Зеленый") + " игрок победил");
    }

    public void PlayAgain()
    {
        TwoPlayersGameLogicManager manager = grid.GetComponent<TwoPlayersGameLogicManager>();

        for (int x = 0; x < manager.width; ++x)
        {
            for (int y = 0; y < manager.height; ++y)
            {
                manager.arr[y][x].GetComponent<Renderer>().material.color = Color.white;
                manager.states[y][x] = 0;
            }
        }

        resultsPanel.SetActive(false);
    }
    
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
