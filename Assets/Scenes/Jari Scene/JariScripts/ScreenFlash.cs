using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScreenFlash : MonoBehaviour
{
    public Image screenFlashImage;

    public IEnumerator FlashColorRoutine(Color color, float duration)
    {
        screenFlashImage.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.3f, 0.0f, elapsedTime / duration);

            screenFlashImage.color = new Color(color.r, color.g, color.b, alpha);

            yield return null; // Wait for the next frame
        }

        screenFlashImage.color = new Color(color.r, color.g, color.b, 0f);
    }

    public void FlashColor(Color color, float duration)
    {
        StartCoroutine(FlashColorRoutine(color, duration));
    }
}
