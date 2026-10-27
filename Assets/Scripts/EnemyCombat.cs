using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [Header("Configuración de Ataque del Enemigo")]
    public Transform enemyAttackPoint;
    public float attackRange = 0.8f;
    public LayerMask playerLayer;
    public int attackDamage = 20;

    // Método para voltear el punto de ataque del enemigo hacia donde está el jugador
    public void FlipAttackPoint(bool targetIsRight)
    {
        if (enemyAttackPoint == null) return;

        Vector3 pos = enemyAttackPoint.localPosition;
        if (targetIsRight)
        {
            pos.x = Mathf.Abs(pos.x); // A la derecha
        }
        else
        {
            pos.x = -Mathf.Abs(pos.x); // A la izquierda
        }
        enemyAttackPoint.localPosition = pos;
    }

    public void AttackPlayer()
    {
        if (enemyAttackPoint != null)
        {
            Collider2D hitPlayer = Physics2D.OverlapCircle(enemyAttackPoint.position, attackRange, playerLayer);

            if (hitPlayer != null)
            {
                PlayerHealth playerHealth = hitPlayer.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                    Debug.Log("¡El enemigo te ha golpeado!");
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (enemyAttackPoint != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(enemyAttackPoint.position, attackRange);
        }
    }
}
