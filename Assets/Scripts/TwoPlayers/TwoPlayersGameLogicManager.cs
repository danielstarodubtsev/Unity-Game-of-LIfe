using System;
using System.Transactions;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.UI;

public class TwoPlayersGameLogicManager : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Grid grid;
    [SerializeField] public float cellSizeWithMargin;
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] private Slider delaySlider;
    private float waitRequired;
    public GameObject[][] arr;
    public int[][] states;

    private void CreateDividingLine()
    {
        GameObject line = new GameObject("DividingLine");
        line.transform.parent = transform;
        LineRenderer lineRenderer = line.AddComponent<LineRenderer>();

        float middleX = width * cellSizeWithMargin / 2f - cellSizeWithMargin / 2;
        float topY = height * cellSizeWithMargin;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(middleX, -cellSizeWithMargin / 2, -1));
        lineRenderer.SetPosition(1, new Vector3(middleX, topY - cellSizeWithMargin / 2, -1));

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
    }

    void Start()
    {
        waitRequired = delaySlider.value;

        arr = new GameObject[height][];
        states = new int[height][];

        for (int i = 0; i < height; ++i)
        {
            arr[i] = new GameObject[width];
            states[i] = new int[width];
        }

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                GameObject cube = Instantiate(cellPrefab);
                cube.transform.position = new Vector2((float)(x * cellSizeWithMargin), (float)(y * cellSizeWithMargin));
                cube.transform.parent = transform;
                cube.AddComponent<CellAnimator>();
                cube.GetComponent<CellAnimator>().delaySlider = delaySlider;
                Destroy(cube.GetComponent<MeshCollider>());

                Renderer rend = cube.GetComponent<Renderer>();
                rend.material.color = Color.white;

                arr[y][x] = cube;
                states[y][x] = 0;
            }
        }

        CreateDividingLine();
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

        int[][] statesCopy = new int[height][];

        for (int i = 0; i < height; ++i)
        {
            statesCopy[i] = new int[width];
        }

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                int red_neighbours = 0;
                int green_neighbours = 0;

                for (int i = -1; i <= 1; ++i)
                {
                    for (int j = -1; j <= 1; ++j)
                    {
                        if ((i != 0 || j != 0) && x + i >= 0 && x + i < width && y + j >= 0 && y + j < height)
                        {
                            int cur_state = states[y + j][x + i];

                            if (cur_state == 1)
                            {
                                ++red_neighbours;
                            }
                            else if (cur_state == 2)
                            {
                                ++green_neighbours;
                            }
                        }
                    }
                }

                int total_neighbours = green_neighbours + red_neighbours;

                if (states[y][x] == 0)
                {
                    if (total_neighbours == 3)
                    {
                        statesCopy[y][x] = red_neighbours >= 2 ? 1 : 2;
                    }
                    else
                    {
                        statesCopy[y][x] = 0;
                    }
                }
                else if (total_neighbours == 2 || total_neighbours == 3)
                {
                    statesCopy[y][x] = states[y][x];
                }
                else
                {
                    statesCopy[y][x] = 0;
                }
            }
        }

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                CellAnimator animator = arr[y][x].GetComponent<CellAnimator>();

                if (states[y][x] == statesCopy[y][x])
                {
                    continue; // не запускаем анимацию если цвет клетки не изменился
                }

                states[y][x] = statesCopy[y][x];
                animator.AnimateCell(states[y][x] == 0 ? Color.white : states[y][x] == 1 ? Color.red : Color.green);
            }
        }
    }
}
