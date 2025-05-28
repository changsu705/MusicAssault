using TMPro;
using UnityEngine;

public class DSPScoreManager : MonoBehaviour
{
    public static DSPScoreManager Instance;

    private double totalScore = 0;
    private int comboCount = 0;
    private int lastScore = 0; // ������ ���� ���� (���� �ƴ�)

    public TextMeshProUGUI scoreText;       // ���� ���� ����
    public TextMeshProUGUI totalScoreText;  // ���� ����
    public TextMeshProUGUI comboText;       // �޺� ǥ��

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// ������ MISS ���θ� �޾� ���� ���¸� �����մϴ�.
    /// </summary>
    public void AddScore(double amount, bool isMiss)
    {
        if (isMiss)
            comboCount = 0;
        else
            comboCount++;

        // 100 ���� ����
        int cutScore = Mathf.FloorToInt((float)(amount / 100)) * 100;

        // totalScore�� ����
        totalScore += cutScore;

        // lastScore�� �̹��� ���� ������ ���� (ǥ�ÿ�)
        lastScore = cutScore;

        // ���� UI ������Ʈ
        scoreText.text = $"{lastScore:D4}";
        totalScoreText.text = ((int)totalScore).ToString();

        // �޺� UI ������Ʈ
        comboText.text = comboCount > 0 ? $"{comboCount} COMBO" : "";
    }

    public double GetTotalScore() => totalScore;
    public int GetCombo() => comboCount;
} 