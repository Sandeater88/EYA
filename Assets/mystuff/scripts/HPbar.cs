using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPbar : MonoBehaviour
{
    public Image fillImage;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = 0f;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
        CheckHealth();
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        fillImage.fillAmount = currentHealth / maxHealth;
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(3); // Load scene number 3
        }
    }
}
