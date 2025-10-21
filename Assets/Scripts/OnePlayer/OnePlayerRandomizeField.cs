using UnityEngine;

public class OnePlayerRandomizeField : MonoBehaviour
{
    [SerializeField] Grid grid;

    public void OnRandomizeFieldButtonDown()
    {
        OnePlayerGameLogicManager gameLogicManager = grid.GetComponent<OnePlayerGameLogicManager>();

        for (int x = 0; x < gameLogicManager.width; ++x)
        {
            for (int y = 0; y < gameLogicManager.height; ++y)
            {
                float random_num = UnityEngine.Random.value;
                GameObject cube = gameLogicManager.arr[y][x];
                CellAnimator animator = cube.GetComponent<CellAnimator>();

                if (random_num < 0.5)
                {
                    gameLogicManager.isAlive[y][x] = false;
                    animator.AnimateCell(Color.white);
                }
                else
                {
                    gameLogicManager.isAlive[y][x] = true;
                    animator.AnimateCell(Color.black);
                }
            }
        }
    }
}