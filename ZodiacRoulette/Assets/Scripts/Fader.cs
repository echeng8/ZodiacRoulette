using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public static Fader instance;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeOut(float time)
    {
        while (canvasGroup.alpha < 1) //alpha is not 1
        {
            canvasGroup.alpha += Time.deltaTime / time;
            yield return null; // run this on the next opportunity of the next frame
        }
    }

    public void FadeOutImmediate()
    {
        canvasGroup.alpha = 1;
    }

    public IEnumerator FadeIn(float time)
    {
        while (canvasGroup.alpha > 0) //alpha is not 1
        {
            canvasGroup.alpha -= Time.deltaTime / time;
            yield return null; // run this on the next opportunity of the next frame
        }
    }
}
