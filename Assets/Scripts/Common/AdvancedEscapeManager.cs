using UnityEditor;
using UnityEngine;

public class AdvancedEscapeManager : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;

    void Start()
    {
        menuPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
