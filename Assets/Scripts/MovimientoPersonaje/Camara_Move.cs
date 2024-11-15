using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Move : MonoBehaviour
{
    public float mouseSensitivity = 300f;
    public Transform playerBody;
    public float rotation = 0f;
    // variables para los test
    public bool useTestInput = false;
    public float testMouseX = 0f;
    public float testMouseY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        // Obtener la entrada del ratón
        float mouseX = useTestInput ? testMouseX * mouseSensitivity * Time.deltaTime : Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = useTestInput ? testMouseY * mouseSensitivity * Time.deltaTime : Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Acumular la rotación vertical y limitarla
        rotation -= mouseY;
        rotation = Mathf.Clamp(rotation, -90f, 90f);

        // Aplicar la rotación acumulada a la cámara
        transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);

        // Rotar el cuerpo del jugador en el eje Y
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

