using UnityEngine;

public class TwoPlayersMoveToGridCenterOnStart : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Grid grid;

    void Start()
    {
        TwoPlayersGameLogicManager manager = grid.GetComponent<TwoPlayersGameLogicManager>();
        int gridHeight = manager.height;
        int gridWidth = manager.width;
        float cellSize = manager.cellSizeWithMargin;

        cam.transform.Translate(new Vector2(gridWidth * cellSize / 2, gridHeight * cellSize / 2));
    }
}
