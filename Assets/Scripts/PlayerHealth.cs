using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena si mueres

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Método para recibir daño (lo llamará el enemigo)
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("¡Te han atacado! Vida actual: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El jugador ha muerto!");

        // Reinicia la escena actual para volver a empezar el nivel de forma rápida
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
