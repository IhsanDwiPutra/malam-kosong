using System.Collections;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteractable
{
    [Header("Pengaturan Pintu")]
    [Tooltip("Masukkan objek Engsel Pintu ke sini")]
    public Transform engselPintu;
    public float openAngle = -90f;
    public float animationSpeed = 5f;

    [Header("Audio")]
    public AudioSource doorSound;

    private bool isOpen = false;
    private bool isAnimating = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Menyimpan posisi rotasi awal
        closedRotation = engselPintu.rotation;
        openRotation = Quaternion.Euler(engselPintu.eulerAngles + new Vector3(0, openAngle, 0));
    }

    // Fungsi ini dipanggil otomatis saat pintu diklik oleh Raycast pemain
    public void Interact()
    {
        // Cegah pemain mengklik pintu berkal-kali saat pintunya masih bergerak
        if (isAnimating) return;

        StartCoroutine(AnimateDoor());
    }

    private IEnumerator AnimateDoor()
    {
        isAnimating = true;
        isOpen = !isOpen;

        // Mainkan suara jika ada
        if (doorSound != null) doorSound.Play();

        // Tentukan pintu harus berputar ke arah mana
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;

        // Proses animasi berputar yang mulus menggunakan Lerp
        while (Quaternion.Angle(engselPintu.rotation, targetRotation) > 0.1f)
        {
            engselPintu.rotation = Quaternion.Lerp(engselPintu.rotation, targetRotation, Time.deltaTime * animationSpeed);
            yield return null;
        }

        // Pastikan pintu berada di posisi yang pas setelah animasi selesai
        engselPintu.rotation = targetRotation;
        isAnimating = false;
    }

    // Fungsi ini yang akan dipanggil oleh trigger di lorong
    public void OpenSlightly()
    {
        // Cegah fungsi berjalan kalau pintu sudah terbuka atau sedang bergerak
        if (isAnimating || isOpen) return;

        StartCoroutine(AnimateDoorSlightly());
    }

    private IEnumerator AnimateDoorSlightly()
    {
        isAnimating = true;
        isOpen = true;

        // Mainkan suara derit pintu jika ada
        if (doorSound != null) doorSound.Play();

        // Hitung sudut terbuka sedikit
        float slightAngle = -25f;
        Quaternion slightOpenRotation = Quaternion.Euler(closedRotation.eulerAngles + new Vector3(0, slightAngle, 0));

        // Animasi pintu berputar perlahan
        while (Quaternion.Angle(engselPintu.rotation, slightOpenRotation) > 0.1f)
        {
            // Kita kalikan animationSpeed dengan 0.5f agar terbukanya LEBIH PELAN dan horor
            engselPintu.rotation = Quaternion.Lerp(engselPintu.rotation, slightOpenRotation, Time.deltaTime * (animationSpeed * 0.5f));
            yield return null;
        }

        engselPintu.rotation = slightOpenRotation;
        isAnimating = false;
    }
}