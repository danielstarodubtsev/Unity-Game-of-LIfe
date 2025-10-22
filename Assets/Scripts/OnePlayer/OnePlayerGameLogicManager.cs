using System;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class OnePlayerGameLogicManager : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Grid grid;
    [SerializeField] public float cellSizeWithMargin;
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] private Slider delaySlider;
    private float waitRequired;
    public GameObject[][] arr;
    public bool[][] isAlive;

    void Start()
    {
        waitRequired = delaySlider.value;

        arr = new GameObject[height][];
        isAlive = new bool[height][];

        for (int i = 0; i < height; ++i)
        {
            arr[i] = new GameObject[width];
            isAlive[i] = new bool[width];
        }

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                GameObject cube = Instantiate(cellPrefab);
                cube.transform.position = new Vector2((float)(x * cellSizeWithMargin), (float)(y * cellSizeWithMargin));
                cube.transform.parent = transform;
                var cellComp = cube.AddComponent<CellAnimator>();
                cellComp.delaySlider = delaySlider;
                Destroy(cube.GetComponent<MeshCollider>());

                Renderer rend = cube.GetComponent<Renderer>();
                rend.material.color = Color.white;

                arr[y][x] = cube;
                isAlive[y][x] = false;
            }
        }
    }

    void Update()
    {
        if (grid.GetComponent<PauseManager>().paused)
        {
            return;
        }

        if (Time.deltaTime < waitRequired)
        {
            waitRequired -= Time.deltaTime;
            return;
        }

        waitRequired = delaySlider.value;

        bool[][] isAliveCopy = new bool[height][];

        for (int i = 0; i < height; ++i)
        {
            isAliveCopy[i] = new bool[width];
        }

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                int alive_neighbours = 0;

                for (int i = -1; i <= 1; ++i)
                {
                    for (int j = -1; j <= 1; ++j)
                    {
                        if ((i != 0 || j != 0) && x + i >= 0 && x + i < width && y + j >= 0 && y + j < height && isAlive[y + j][x + i])
                        {
                            ++alive_neighbours;
                        }
                    }
                }

                isAliveCopy[y][x] = alive_neighbours == 3 || (alive_neighbours == 2 && isAlive[y][x]);
            }
        }

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                CellAnimator animator = arr[y][x].GetComponent<CellAnimator>();

                if (isAlive[y][x] == isAliveCopy[y][x])
                {
                    continue; // не запускаем анимацию если цвет клетки не изменился
                }

                isAlive[y][x] = isAliveCopy[y][x];
                animator.AnimateCell(isAlive[y][x] ? Color.black : Color.white);
            }
        }
    }
}
