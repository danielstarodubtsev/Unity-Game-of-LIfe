using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;
using System.Collections.Generic;

public class OnePlayerCellColorManager : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private Toggle erasorToggle;
    [SerializeField] private Slider brushSizeSlider;
    [SerializeField] private TMP_Dropdown figureDropdown;
    [SerializeField] private TMP_Dropdown figureRotationDropdown;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        bool lmb = Input.GetMouseButton(0);
        bool rmb = Input.GetMouseButton(1);

        if (!lmb && !rmb)
        {
            return;
        }

        OnePlayerGameLogicManager manager = grid.GetComponent<OnePlayerGameLogicManager>();
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) / manager.cellSizeWithMargin;
        int mouseX = (int)Math.Round(mousePosition.x);
        int mouseY = (int)Math.Round(mousePosition.y);

        if (erasorToggle.isOn)
        {
            for (int deltaX = -(int)brushSizeSlider.value; deltaX <= (int)brushSizeSlider.value; ++deltaX)
            {
                for (int deltaY = -(int)brushSizeSlider.value; deltaY <= (int)brushSizeSlider.value; ++deltaY)
                {
                    int x = mouseX + deltaX;
                    int y = mouseY + deltaY;

                    if (deltaX * deltaX + deltaY * deltaY <= brushSizeSlider.value * brushSizeSlider.value && x >= 0 && x < manager.width && y >= 0 && y < manager.height)
                    {
                        manager.arr[y][x].GetComponent<CellAnimator>().UrgentlyFinishAllAnimations();
                        manager.arr[y][x].GetComponent<Renderer>().material.color = Color.white;
                        manager.isAlive[y][x] = false;
                    }
                }
            }

            return;
        }

        if (figureDropdown.value == (int)FigureCodes.SingleCell)
        {
            for (int deltaX = -(int)brushSizeSlider.value; deltaX <= (int)brushSizeSlider.value; ++deltaX)
            {
                for (int deltaY = -(int)brushSizeSlider.value; deltaY <= (int)brushSizeSlider.value; ++deltaY)
                {
                    int x = mouseX + deltaX;
                    int y = mouseY + deltaY;

                    if (deltaX * deltaX + deltaY * deltaY <= brushSizeSlider.value * brushSizeSlider.value && x >= 0 && x < manager.width && y >= 0 && y < manager.height)
                    {
                        manager.arr[y][x].GetComponent<CellAnimator>().UrgentlyFinishAllAnimations();
                        manager.arr[y][x].GetComponent<Renderer>().material.color = Color.black;
                        manager.isAlive[y][x] = true;
                    }
                }
            }

            return;
        }

        List<(int, int)> deltas = FiguresPresets.GetFigureDeltas(figureDropdown.value);

        foreach (var (deltaX, deltaY) in deltas)
        {
            var rotated = Utils.RotateDelta(deltaX, deltaY, figureRotationDropdown.value);
            int x = mouseX + rotated.rotatedDeltaX;
            int y = mouseY + rotated.rotatedDeltaY;

            if (x >= 0 && x < manager.width && y >= 0 && y < manager.height) {
                manager.arr[y][x].GetComponent<CellAnimator>().UrgentlyFinishAllAnimations();
                manager.arr[y][x].GetComponent<Renderer>().material.color = Color.black;
                manager.isAlive[y][x] = true;
            }                
        }
    }
}
