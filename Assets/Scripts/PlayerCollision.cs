using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private Vector3 originalPosition; // Posición original del jugador
    private Rigidbody rb;
    private Vector3 originalVelocity; // Velocidad original del jugador

    void Start()
    {
        // Guardar la posición original y la velocidad original del jugador al inicio
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        originalVelocity = rb.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador colisiona con un objeto etiquetado como "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colisión con enemigo detectada"); // Mensaje de depuración

            // Guardar la posición y la velocidad actual del jugador
            Vector3 currentPlayerPosition = transform.position;
            Vector3 currentPlayerVelocity = rb.velocity;

            // Reiniciar la escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Restaurar la posición y la velocidad del jugador después del reinicio
            transform.position = originalPosition;
            rb.velocity = originalVelocity;

            // También podrías restaurar otros estados del jugador aquí, como la orientación, etc.
        }
    }
}
