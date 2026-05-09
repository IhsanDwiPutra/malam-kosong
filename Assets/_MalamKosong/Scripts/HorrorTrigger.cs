using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class HorrorTrigger : MonoBehaviour
{
    [Header("Pengaturan Trigger")]
    public bool triggerOnce = true;
    private bool hasTriggered = false;

    [Header("Event yang dijalankan")]
    // UnityEvent memungkinkan kita memasukkan fungsi apa aja langsung dari Inspector (sangat fleksibel)
    public UnityEvent onTriggerEvent;

    private void OnTriggerEnter(Collider other)
    {
        // Mengecek apakah yang menyentuh trigger adalah pemain
        if (other.CompareTag("Player"))
        {
            if (triggerOnce && hasTriggered) return; // Berhenti jika sudah pernah terpicu

            // Menjalankan semua event yang sudah disetel di inspector
            onTriggerEvent.Invoke();
            hasTriggered = true;

            Debug.Log("Trigger " + gameObject.name + " berhasil diaktifkan!");
        }
    }
}