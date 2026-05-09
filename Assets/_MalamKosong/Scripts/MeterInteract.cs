using UnityEngine;

public class MeterInteract : MonoBehaviour, IInteractable
{
    [Header("Referensi Sistem")]
    public BlackoutManager blackoutManager;

    [Header("Kejutan Manekin (Fase 6)")]
    [Tooltip("Masukkan objek Manekin yang ada di ruang tengah")]
    public GameObject manekin;
    public AudioSource tenseAudio;

    private bool hasTurnedOn = false;

    public void Interact()
    {
        // Supaya meteran cuma bisa diklik sekali
        if (hasTurnedOn) return;
        hasTurnedOn = true;

        // Panggil fungsi menyalakan lampu dari BlackoutManager
        if (blackoutManager != null)
        {
            blackoutManager.TurnOnLights();
        }

        // Munculkan Manekin secara diam-diam di ruang tengah
        if (manekin != null)
        {
            manekin.SetActive(true);
        }

        // Mainkan efek suara tegang
        if (tenseAudio != null) tenseAudio.Play();

        // Munculkan teks batin
        UIManager.Instance.ShowMonologue("Nah nyala lagi. Balik ke kamar sekarang.", 4f);
    }
}