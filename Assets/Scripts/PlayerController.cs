using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento del jugador
    public float jumpForce = 10f;   // Fuerza de salto del jugador
    private Rigidbody rb;
    private bool isGrounded;
    private bool justJump = false;
    private float originalMoveSpeed;
    private Vector3 originalPosition;
    private Vector3 originalGravity;
    private int gravityMultiplier = 1;
    public bool isStomping = false;
    private Vector3 lastValidPosition; // Variable para almacenar la última posición válida del jugador
    private EnemyController[] enemies; // Array para almacenar todos los enemigos

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        originalMoveSpeed = moveSpeed;
        originalPosition = transform.position;
        lastValidPosition = originalPosition; // Inicializar la última posición válida
        Physics.gravity = originalGravity * 2f; // Duplicar la gravedad
        enemies = FindObjectsOfType<EnemyController>(); // Buscar todos los objetos con el script EnemyController
    }

    void FixedUpdate()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.fixedDeltaTime;

        // Aplicar movimiento al jugador
        rb.MovePosition(transform.position + movement);
        
        // Rotar el jugador para que mire en la dirección del movimiento
        if (movement != Vector3.zero)
        {
            // Obtener el ángulo de rotación en radianes
            float angle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;

            // Crear una rotación basada en el ángulo
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

            // Rotar gradualmente hacia la nueva rotación
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, Time.deltaTime * 1000f));
        }
    }

    void Update()
    {
        // Saltar
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (!isGrounded && rb.velocity.y < 0)
            {
                gravityMultiplier = 3;
                isStomping = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            justJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && justJump)  // Doble salto
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            justJump = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) // Correr
        {
            moveSpeed *= 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))   // Retornar a velocidad normal
        {
            moveSpeed = originalMoveSpeed;
        }

        if (!isGrounded && rb.velocity.y < 0)
        {
            Physics.gravity = originalGravity * 4f * gravityMultiplier; // Aumentar la gravedad
        }

        if (transform.position.y < -6)
        {
            transform.position = originalPosition; // Volver a la posición original
            lastValidPosition = originalPosition; // Restablecer la última posición válida
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        // Verificar si el jugador colisiona con un objeto etiquetado como "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = originalPosition; // Volver a la posición original
            lastValidPosition = originalPosition; // Restablecer la última posición válida
            // Reiniciar el movimiento de todos los enemigos
            foreach (EnemyController enemy in enemies)
            {
                enemy.ResetEnemy();
            }
        }

        
    }

    void OnCollisionStay(Collision collision)
    {
        // Verificar si el jugador colisiona con un obstáculo
        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            // Mover al jugador a la última posición válida
            rb.MovePosition(lastValidPosition);
        }
        else
        {
            // Actualizar la última posición válida del jugador mientras permanece en colisión con un objeto
            lastValidPosition = transform.position;
        }
    }

}
