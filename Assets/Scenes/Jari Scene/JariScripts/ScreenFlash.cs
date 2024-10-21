using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ScreenFlash : MonoBehaviour
{

    public Image Image;
    public float fadeDuration = 1.0f;  
    private void Start()
    {   
        Image = FindAnyObjectByType<Image>();
    }

   public IEnumerator SetColorAlpha()
    {
        Debug.Log("Setting starts");
        Color color = Color.green;
        Image.color = color; 

        float elapsedTime = 0f; 

        while (elapsedTime < fadeDuration)
        {
            
            elapsedTime += Time.deltaTime; 
            float alpha = Mathf.Lerp(0.3f, 0.0f, elapsedTime / fadeDuration); 

            Image.color = new Color(color.r, color.g, color.b, alpha); 

            yield return null; // Wait for the next frame
        }

        Image.color = new Color(color.r, color.g, color.b, 0f);
    }
    public IEnumerator SetColorToRed()
    {
        Color color = Color.red;
        Image.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0.3f, 0.0f, elapsedTime / fadeDuration);

            Image.color = new Color(color.r, color.g, color.b, alpha);

            yield return null; // Wait for the next frame
        }

        Image.color = new Color(color.r, color.g, color.b, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(SetColorToRed());
        }
    }
}
