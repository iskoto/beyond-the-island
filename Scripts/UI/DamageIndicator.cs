using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image bloodimage;
    public float flashSpeed;

    private Coroutine fadeAway;

    public void Flash()
    {
        if (fadeAway != null)
        {
            StopCoroutine(fadeAway);
        }

        bloodimage.enabled = true;
        //Sets the alpha value of the image to 1 to create a bleeding screen.
        bloodimage.color = Color.white;
        fadeAway = StartCoroutine(FadeAway());

    }

    IEnumerator FadeAway()
    {
        float alphaImage = 1.0f;

        while (alphaImage > 0)
        {
            alphaImage -= (1.0f / flashSpeed) * Time.deltaTime;
            bloodimage.color = new Color(1.0f, 1.0f,1.0f,alphaImage);
            yield return null;
            
        }
        //deactivating image
        bloodimage.enabled = false;
        
    }
}

