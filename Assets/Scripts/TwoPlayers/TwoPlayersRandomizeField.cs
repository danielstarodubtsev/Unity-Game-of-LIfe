using UnityEngine;

public class TwoPlayersRandomizeField : MonoBehaviour
{
    [SerializeField] Grid grid;

    public void OnRandomizeFieldButtonDown()
    {
        TwoPlayersGameLogicManager gameLogicManager = grid.GetComponent<TwoPlayersGameLogicManager>();

        for (int x = 0; x < gameLogicManager.width; ++x)
        {
            for (int y = 0; y < gameLogicManager.height; ++y)
            {
                float random_num = UnityEngine.Random.value;
                GameObject cube = gameLogicManager.arr[y][x];
                CellAnimator animator = cube.GetComponent<CellAnimator>();

                if (random_num < 0.5)
                {
                    gameLogicManager.states[y][x] = 0;
                    animator.AnimateCell(Color.white);
                }
                else
                {
                    gameLogicManager.states[y][x] = x < gameLogicManager.width / 2 ? 1 : 2;
                    animator.AnimateCell(x < gameLogicManager.width / 2 ? Color.red : Color.green);
                }
            }
        }
    }
}