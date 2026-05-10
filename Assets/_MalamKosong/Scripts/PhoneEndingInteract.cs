using System.Collections;
using UnityEngine;

public class PhoneEndingInteract : MonoBehaviour
{
    [Header("Pengaturan Audio")]
    public AudioSource phoneVibrateAudio; // Suara HP getar sebelum diklik
    public AudioSource voiceNoteAudio; // Suara voice note temannya
    public AudioSource footstepsAudio; // Suara derap langkah kaki lari kencang
    public AudioSource doorBangAudio; // Suara pintu dibanting


    [Header("Pengaturan Visual")]
    public PlayerController playerMovement;
    public Transform bedroomDoorHinge; // Engsel pintu kamarmu (untuk dibanting terbuka)
    public GameObject titleScreenUI; // Objek Canvas berisi teks judul "MALAM KOSONG"
    public GameObject phoneCanvasUI; // Pop-up di layar

    private bool hasClicked = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasClicked)
        {
            StartCoroutine(EndingSequence());
        }
    }

    private IEnumerator EndingSequence()
    {
        // Kunci pergerakan player agar dia diam mendengarkan
        if (playerMovement != null) playerMovement.enabled = false;

        // HP Bergetar & Muncul di layar
        if (phoneVibrateAudio != null) phoneVibrateAudio.Play();
        if (phoneCanvasUI != null) phoneCanvasUI.SetActive(true);

        yield return new WaitForSeconds(2f); // Biarkan bergetar sebentar
        if (phoneVibrateAudio != null) phoneVibrateAudio.Stop();

        // Putar voice note
        if (voiceNoteAudio != null)
        {
            voiceNoteAudio.Play();

            // Tunggu secara otomatis sampai durasi audio VN selesai
            yield return new WaitForSeconds(voiceNoteAudio.clip.length);
        }

        // Jumpscare langkah kaki berlari (tunggu sejenak biar hening & tegang)
        yield return new WaitForSeconds(0.5f);

        if (footstepsAudio != null) footstepsAudio.Play();

        // Tungguu sampai suara langkah terasa dekat (Misal 2 detik)
        yield return new WaitForSeconds(2f);

        // Pintu dibanting BUKA!
        if (doorBangAudio != null) doorBangAudio.Play();
        if (bedroomDoorHinge != null)
        {
            // Memaksa engsel pintu berputar 90 derajat seketika (dibanting)
            bedroomDoorHinge.localRotation = Quaternion.Euler(0, 90, 0);
        }

        // Layar langsung hitam total (Cut to Black)
        FadeManager.Instance.FadeOut();

        // Jeda sebentar dalam kegelapan
        yield return new WaitForSeconds(2f);

        // Munculkan Judul game
        if (titleScreenUI != null) titleScreenUI.SetActive(true);


    }
}