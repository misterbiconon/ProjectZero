using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movimiento y Patrulla")]
    public float speed = 3f;
    public float walkDistance = 3f;
    private Vector2 startPos;
    private int direction = 1;

    [Header("Detección y Rangos")]
    public Transform player;
    public float aggroRange = 5f;
    public float attackRange = 1.2f;

    [Header("Tiempos de Ataque")]
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    private Rigidbody2D rb;
    private EnemyCombat enemyCombat; // Referencia al script independiente de combate

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        enemyCombat = GetComponent<EnemyCombat>(); // Obtiene el componente de combate del mismo objeto
        startPos = transform.position;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. Si está en rango de ataque: se detiene y llama al script de combate
        if (distanceToPlayer <= attackRange)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            if (Time.time >= lastAttackTime + attackCooldown)
            {
                if (enemyCombat != null)
                {
                    enemyCombat.AttackPlayer();
                }
                lastAttackTime = Time.time;
            }
            return;
        }

        // 2. Si el jugador está en rango de persecución (Aggro)
        if (distanceToPlayer <= aggroRange)
        {
            if (player.position.x > transform.position.x)
                direction = 1;
            else
                direction = -1;

            rb.linearVelocity = new Vector2(direction * (speed * 1.3f), rb.linearVelocity.y);
            return;
        }

        // 3. Patrulla normal de un lado a otro
        float distanceMoved = transform.position.x - startPos.x;
        if (Mathf.Abs(distanceMoved) >= walkDistance)
        {
            direction *= -1;
        }

        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }
}
