using System.Collections;
using UnityEngine;

public class StoveInteract : MonoBehaviour, IInteractable
{
    [Header("Referensi Objek")]
    public PlayerController playerMovement;

    [Tooltip("Masukkan objek kosong (Empty GameObject) yang ditaruh di kursi meja makan")]
    public Transform diningSeatPosition;

    private bool hasCooked = false;

    public void Interact()
    {
        if (hasCooked) return;
        hasCooked = true;

        StartCoroutine(CookingSequence());
    }

    private IEnumerator CookingSequence()
    {
        // Matikan kontrol pergerakan & kamera sejak awal kompor diklik
        playerMovement.enabled = false;

        // Layar fade out perlahan ke hitam
        FadeManager.Instance.FadeOut();
        yield return new WaitForSeconds(1.5f);

        // Munculkan teks time skip
        UIManager.Instance.ShowMonologue("10 Menit Kemudian...", 3.5f);
        yield return new WaitForSeconds(3.5f);

        // Teleportasi ke meja makan
        CharacterController cc = playerMovement.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        if (diningSeatPosition != null)
        {
            playerMovement.transform.position = diningSeatPosition.position;
            playerMovement.transform.rotation = diningSeatPosition.rotation;
        } else
        {
            Debug.LogWarning("Titik kursi makan belum dimasukkan!");
        }

        // Nyalakan fisik kembali, tapi biarkan playerMovement tetap mati
        // Ini akan membuat pemain diam terkunci di kursi (hanya bisa nunggu instruksi selanjutnya)
        if (cc != null) cc.enabled = true;
        playerMovement.enabled = false;

        FadeManager.Instance.FadeIn();
    }
}
