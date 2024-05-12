using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 10f;    // Velocidad de movimiento del enemigo
    private Rigidbody rb;
    private bool movingRight = true; // Variable para rastrear la dirección del movimiento del enemigo
    private Vector3 originalPosition; // Posición original del enemigo
    private Quaternion originalRotation; // Rotación original del enemigo
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Guardar la posición y rotación original del enemigo al inicio
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        // Inicialmente, el enemigo se mueve hacia la derecha
        rb.velocity = Vector3.right * moveSpeed;
    }

   
    // Función para reiniciar todo el comportamiento del enemigo a su estado original
    public void ResetEnemy()
    {
        // Restablecer la posición y rotación del enemigo
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Reiniciar el movimiento del enemigo hacia la derecha
        rb.velocity = Vector3.right * moveSpeed;
        movingRight = true;
    }

    void FixedUpdate()
    {
        // Verificar si el enemigo está chocando con un objeto etiquetado como "Pared"
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f); // Radio de colisión pequeño para evitar falsos positivos
        bool hitWall = false;
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Pared"))
            {
                hitWall = true;
                break; // Salir del bucle una vez que se haya encontrado una "Pared"
            }
        }

        // Si el enemigo choca con una pared, cambiar la dirección de movimiento
        if (hitWall)
        {
            if (movingRight)
            {
                rb.velocity = Vector3.left * moveSpeed;
                movingRight = false;
            }
            else
            {
                rb.velocity = Vector3.right * moveSpeed;
                movingRight = true;
            }
        }
    }
}
