using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton agar skrip ini bisa dipanggil dari mana saja tanpa ribet
    public static UIManager Instance;

    [Header("Pengaturan Teks Batin")]
    public TextMeshProUGUI monologueText;
    public CanvasGroup textCanvasGroup;
    public float fadeSpeed = 1.5f;

    void Awake()
    {
        if (Instance == null) Instance = this;
        textCanvasGroup.alpha = 0; // Pastikan teks tidak terlihat saat game baru mulai
    }

    // Fungsi ini yang akan kita panggil untuk memunculkan teks
    public void ShowMonologue(string message, float duration)
    {
        StopAllCoroutines();
        StartCoroutine(MonologueRoutine(message, duration));
    }

    private IEnumerator MonologueRoutine(string message, float duration)
    {
        monologueText.text = message;

        // Efek Fade In Teks
        while (textCanvasGroup.alpha < 1)
        {
            textCanvasGroup.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }

        // Tunggu pemain membaca
        yield return new WaitForSeconds(duration);
        
        // Efek Fade Out Teks
        while (textCanvasGroup.alpha > 0)
        {
            textCanvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
    }

}