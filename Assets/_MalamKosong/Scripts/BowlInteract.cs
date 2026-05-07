using System.Collections;
using UnityEngine;

public class BowlInteract : MonoBehaviour, IInteractable
{
    [Header("Referensi Objek")]
    public PlayerController playerMovement;

    [Tooltip("Titik berdiri setelah kaget mendengar suara")]
    public Transform standingPosition;

    [Header("Pengaturan Jumpscare")]
    public AudioSource gayungJatuhAudio; // Referensi ke suara di kamar mandi

    private bool hasEaten = false;

    public void Interact()
    {
        if (hasEaten) return;
        hasEaten = true;

        StartCoroutine(EatAndScareSequence());
    }

    private IEnumerator EatAndScareSequence()
    {
        // Munculkan teks batin sebentar
        UIManager.Instance.ShowMonologue("Akhirnya mateng juga, mari mak-", 3f);
        yield return new WaitForSeconds(3f);

        // Trigger suara gubrak!
        if (gayungJatuhAudio != null)
        {
            gayungJatuhAudio.Play();
        }
        yield return new WaitForSeconds(3f);


        // Teks batin kaget
        UIManager.Instance.ShowMonologue("Suara apaan tuh dari kamar mandi?!", 3.5f);
        yield return new WaitForSeconds(1.5f); // Jeda panik sebentar sebelum berdiri

        // Teleportasi berdiri
        CharacterController cc = playerMovement.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        if (standingPosition != null)
        {
            playerMovement.transform.position = standingPosition.position;
            playerMovement.transform.rotation = standingPosition.rotation;
        }

        // Nyalakan kembali kontrol agar pemain bisa jalan mengecek kamar mandi
        if (cc != null) cc.enabled = true;
        playerMovement.enabled = true;
    }
}
