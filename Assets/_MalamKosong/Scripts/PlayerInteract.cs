using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Pengaturan Raycast")]
    public float interactDistance = 2.5f;
    public LayerMask interactLayer;

    private Camera cam;

    void Start()
    {
        // Mengambil komponen kamera tempat skrip ini dipasang
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Mengecek apakah pemain menekan tombol klik kiri mouse
        if (Input.GetMouseButtonDown(0))
        {
            ShootRaycast();
        }
    }

    private void ShootRaycast()
    {
        // Membuat titik pusat dari tengah layar
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Menembakkan Raycast ke depan
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            // Mengecek apakah objek yang tertembak memiliki interface IInteractable
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();

            if (interactableObject != null)
            {
                interactableObject.Interact();
            }
        }
    }



}
