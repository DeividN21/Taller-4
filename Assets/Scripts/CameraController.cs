using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // El objeto que la cámara seguirá

    void Update()
    {
        if (target == null)
            return;

        // Obtener la posición del jugador en la pantalla
        Vector3 playerScreenPos = Camera.main.WorldToViewportPoint(target.position);

        // Centrar la cámara en el jugador en el eje X
        Vector3 cameraPos = transform.position;
        cameraPos.x = target.position.x;

        // Aplicar la nueva posición a la cámara
        transform.position = cameraPos;
    }
}
