using UnityEngine;

public class BlackoutManager : MonoBehaviour
{
    [Header("Pengaturan Lampu Ruangan")]
    [Tooltip("Masukkan semua lampu yang ada di rumah ke sini")]
    public GameObject[] houseLights;

    [Header("Pengaturan Senter HP")]
    public GameObject phoneFlashlight;

    [Header("Audio")]
    public AudioSource powerDownAudio;

    public void TriggerBlackout()
    {
        // Matikan semua lampu rumah
        foreach (GameObject light in houseLights)
        {
            if (light != null) light.SetActive(false);
        }

        // Mainkan suara listrik mati
        if (powerDownAudio != null) powerDownAudio.Play();

        // Nyalakan HP & Senter
        if (phoneFlashlight != null) phoneFlashlight.SetActive(true);

        // Munculkan teks batin
        UIManager.Instance.ShowMonologue("Lah, mati lampu?! Harus cek meteran di teras depan nih...", 4f);

        // Ubah warna for/environment Unity jadi hitam pekat agar benar-benar gelap
        RenderSettings.ambientLight = Color.black;
    }

    public void TurnOnLights()
    {
        // Menyalakan semua lampu kembali
        foreach (GameObject light in houseLights)
        {
            if (light != null) light.SetActive(true);
        }

        // Matikan Senter HP
        if (phoneFlashlight != null) phoneFlashlight.SetActive(false);

        // Kembalikan warna ruangan agar terang lagi
        RenderSettings.ambientLight = new Color(0.2f, 0.2f, 0.2f);
    }
}