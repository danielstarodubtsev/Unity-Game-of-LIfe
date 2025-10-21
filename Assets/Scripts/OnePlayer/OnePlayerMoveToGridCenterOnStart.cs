using UnityEngine;

public class OnePlayerMoveToGridCenterOnStart : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Grid grid;

    void Start()
    {
        OnePlayerGameLogicManager manager = grid.GetComponent<OnePlayerGameLogicManager>();
        int gridHeight = manager.height;
        int gridWidth = manager.width;
        float cellSize = manager.cellSizeWithMargin;

        cam.transform.Translate(new Vector2(gridWidth * cellSize / 2, gridHeight * cellSize / 2));
    }
}
