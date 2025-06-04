using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DSPHealthManager : MonoBehaviour
{
    public static DSPHealthManager Instance;

    public int maxHealth = 120;
    private int currentHealth;

    public TextMeshProUGUI healthText;

    private bool isDead = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void ApplyMissDamage(int amount = 40)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            isDead = true;
            healthText.text = "체력 0! 게임 오버 처리 필요";
            SceneManager.LoadScene("GameOver");

        }
        else
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (isDead)
            return;

        healthText.text = $"HP: {currentHealth} / {maxHealth}";
    }

    public int GetHealth() => currentHealth;
}
