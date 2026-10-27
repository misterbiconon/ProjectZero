using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;

    [Header("Tiempos de Ataque")]
    public float attackRate = 0.5f;
    private float nextAttackTime = 0f;

    // Referencia para saber hacia dónde mira el jugador
    private bool isFacingRight = true;

    void Update()
    {
        // Detectar la dirección en función de las teclas de movimiento o escala
        DetectFacingDirection();

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void DetectFacingDirection()
    {
        // Si te estás moviendo a la izquierda o derecha, actualizamos la orientación
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput > 0)
        {
            isFacingRight = true;
        }
        else if (moveInput < 0)
        {
            isFacingRight = false;
        }

        // Ajustamos la posición del AttackPoint según la dirección
        if (attackPoint != null)
        {
            Vector3 pos = attackPoint.localPosition;
            if (isFacingRight)
            {
                pos.x = Mathf.Abs(pos.x); // Asegura que esté a la derecha (+)
            }
            else
            {
                pos.x = -Mathf.Abs(pos.x); // Asegura que esté a la izquierda (-)
            }
            attackPoint.localPosition = pos;
        }
    }

    void Attack()
    {
        Debug.Log("¡Ataque ejecutado!");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("¡Golpeaste a: " + enemy.name + "!");
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
