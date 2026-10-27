using System.Collections;
using UnityEngine;

public class ZeroShift : MonoBehaviour
{
    [Header("Configuración de Zero Shift (Dash)")]
    public float dashSpeed = 20f;       // Velocidad a la que se impulsa
    public float dashDuration = 0.2f;   // Cuánto dura el dash en segundos
    public float dashCooldown = 1f;     // Tiempo de espera para volver a usarlo

    private bool canDash = true;
    private bool isDashing;
    private Rigidbody2D rb;
    private float originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            originalGravity = rb.gravityScale;
        }
    }

    void Update()
    {
        if (isDashing) return;

        // Activamos el Zero Shift con la tecla Left Shift o el botón derecho del ratón (puedes cambiarlo)
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1)) && canDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        canDash = false;
        isDashing = true;

        // Guardamos la gravedad y se la quitamos para que el dash sea recto y limpio
        if (rb != null)
        {
            originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;

            // Determinamos la dirección en función de la escala local del personaje (hacia dónde mira)
            float facingDirection = Mathf.Sign(transform.localScale.x);
            if (facingDirection == 0) facingDirection = 1f;

            // Aplicamos la velocidad instantánea del dash
            rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0f);
        }

        // Esperamos lo que dura el dash
        yield return new WaitForSeconds(dashDuration);

        // Restauramos la gravedad y el estado normal
        if (rb != null)
        {
            rb.gravityScale = originalGravity;
        }

        isDashing = false;

        // Esperamos el tiempo de cooldown antes de poder volver a usarlo
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
