using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Pengaturan Pergerakan")]
    public float speed = 3.0f;
    public float mouseSensitivity = 100f;

    private CharacterController controller;
    private Transform cameraTransform;
    private float xRotation;

    void Start()
    {
        // Mengambil komponen yang dibutuhkan secara otomatis
        controller = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;

        // Mengunci dan menyembunyikan kursor mouse di tengah layar saat main
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. Logika Melihat Sekeliling (Mouse Look)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        // Membatasi agar pemain tidak bisa melihat sampai ke belakang kepalanya sendiri (kayang)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // 2. Logika Berjalan (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

}