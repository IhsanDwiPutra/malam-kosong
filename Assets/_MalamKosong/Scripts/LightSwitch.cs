using UnityEngine;

// Menambahkan "IInteractable" berarti skrip ini wajib memiliki fungsi Interact)_
public class LightSwitch : MonoBehaviour, IInteractable
{
    [Header("Pengaturan Lampu")]
    public Light roomLight;

    private bool isLightOn = true;

    // Fungsi ini otomatis dipanggil oleh skrip PlayerInteract saat saklar diklik
    public void Interact()
    {
        isLightOn = !isLightOn; // Membalikkan status

        if(roomLight != null)
        {
            roomLight.enabled = isLightOn;
        }
        Debug.Log("Saklar diklik! Status lampu sekarang: " + (isLightOn ? "Menyala" : "Mati"));
    }
}
