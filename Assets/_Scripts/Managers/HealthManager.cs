using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private float maxHealth;
    public float currentHealth;

    [SerializeField] GameObject lifeBar;

    public delegate void NoHealthAction();
    public event NoHealthAction NoHealth;


    public void SetUp(float health)
    {
        maxHealth = health;
        currentHealth = health;
        lifeBar.transform.localScale = new Vector3(1, 1, 1);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        lifeBar.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        if (currentHealth == 0 && NoHealth != null)
        {
            NoHealth.Invoke();
        }
    }

    public void Heal(float healValue)
    {
        currentHealth += healValue;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public bool CanHeal()
    {
        return currentHealth < maxHealth;
    }
}
