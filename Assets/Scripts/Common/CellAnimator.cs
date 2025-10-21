using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CellAnimator : MonoBehaviour
{
    public Slider delaySlider;
    private Color nextColor;

    public void UrgentlyFinishAllAnimations()
    {
        Renderer renderer = GetComponent<Renderer>();
        StopAllCoroutines();
        renderer.material.color = nextColor;
    }

    public void AnimateCell(Color targetColor)
    {
        StopAllCoroutines();
        nextColor = targetColor;
        StartCoroutine(AnimateColorChange(targetColor));
    }

    private IEnumerator AnimateColorChange(Color targetColor)
    {
        float timer = 0f;
        Renderer renderer = GetComponent<Renderer>();
        Color startColor = renderer.material.color;

        while (timer < delaySlider.value)
        {
            timer += Time.deltaTime;
            float progress = timer / delaySlider.value;

            renderer.material.color = Color.Lerp(startColor, targetColor, progress);

            yield return null;
        }

        renderer.material.color = targetColor;
    }
}