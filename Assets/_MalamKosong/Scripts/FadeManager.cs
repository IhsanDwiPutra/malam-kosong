using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("Pengaturan Layar Hitam")]
    public Image blackScreen;
    public float fadeSpeed = 1.0f;

    void Awake()
    {
        if (Instance == null) Instance = this;

        // Pastikan layar hitam total game pertama kali dijalankan (untuk efek Fade In awal scene)
        Color startColor = blackScreen.color;
        startColor.a = 1f;
        blackScreen.color = startColor;
    }

    void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeRoutine(0f)); // Layar memudar menjadi terang
    }

    public void FadeOut()
    {
        StartCoroutine(FadeRoutine(1f)); // Layar menggelap menjadi hitam
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        Color currentColor = blackScreen.color;
        while (Mathf.Abs(currentColor.a - targetAlpha) > 0.01f)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            blackScreen.color = currentColor;
            yield return null;
        }

        currentColor.a = targetAlpha;
        blackScreen.color = currentColor;
    }
}