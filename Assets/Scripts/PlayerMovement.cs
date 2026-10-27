using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 8f;
    private float horizontalInput;

    [Header("Salto")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;

    void Start()
    {
        // Obtenemos el componente Rigidbody2D automáticamente
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Capturamos la entrada del teclado (A/D o Flechas Izquierda/Derecha)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 2. Detección del suelo para poder saltar
        // Asegúrate de crear un objeto vacío llamado 'GroundCheck' en los pies del personaje y asignarlo aquí
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 3. Orden de salto al pulsar Espacio
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // 4. Aplicamos el movimiento horizontal manteniendo la velocidad vertical de las físicas
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
    }
}