using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class PrologueInteract : MonoBehaviour, IInteractable
{
    [Header("Referensi Skrip")]
    public PlayerController playerMovement; // Referensi agar kita bisa mengaktifkan pergerakkan nanti

    // Variabel baru untuk menentukan titik berdiri yang aman
    [Tooltip("Masukkan objek kosong (Empty GameObject) yang ditaruh di belakang kursi")]
    public Transform standingPosition;
    private bool hasBeenClicked = false;

    // Fungsi ini terpanggil saat pemain mengklik objek ini menggunakan Raycast
    public void Interact()
    {
        // Mencegah pemain mengklik laptop berkali-kali
        if (hasBeenClicked) return;
        hasBeenClicked = true;

        // Memulai urutan adegan
        StartCoroutine(PrologueSequence());
    }

    private IEnumerator PrologueSequence()
    {
        // 1. Memunculkan Teks Batin
        UIManager.Instance.ShowMonologue("Tugas akhirnya kelar juga. Perut keroncongan, bikin mie nyemek dulu lah.", 3.5f);

        // Tunggu sebentar sampai pemain selesai membaca teks
        yield return new WaitForSeconds(3.5f);

        // 2. Layar Fade Out ke Hitam
        FadeManager.Instance.FadeOut();

        // Tunggu sampai layar benar-benar gelap total (sekitar 1.5 detik)
        yield return new WaitForSeconds(1.5f);

        // Teleportasi posisi saat layar gelap
        CharacterController cc = playerMovement.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        // Pindahkan posisi dan arah pandangan tepat ke posisi objek "StandingPosition"
        if (standingPosition != null)
        {
            playerMovement.transform.position = standingPosition.position;
            playerMovement.transform.rotation = standingPosition.rotation;
        } else
        {
            Debug.LogWarning("Titik Standing Positio belum dimasukkan di Inspector!");
        }

        // Nyalakan kembali komponen pergerakannya
        if (cc != null) cc.enabled = true;
        playerMovement.enabled = true; // Sekarang pemain bisa menggunakan WASD dan Mouse Look

        // 4. Layar kembali terang (Fade In)
        FadeManager.Instance.FadeIn();
    }

}