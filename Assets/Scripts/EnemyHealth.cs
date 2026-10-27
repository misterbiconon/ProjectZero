using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Configuración de Vida")]
    public int maxHealth = 40; // Vida total del enemigo
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Método para que el enemigo reciba daño (llamado por PlayerCombat)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("¡Enemigo herido! Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El enemigo ha sido derrotado!");

        // Destruye el objeto del enemigo de la escena
        Destroy(gameObject);
    }
}
