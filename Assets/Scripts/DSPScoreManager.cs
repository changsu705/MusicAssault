using TMPro;
using UnityEngine;

public class DSPScoreManager : MonoBehaviour
{
    public static DSPScoreManager Instance;

    private double totalScore = 0;
    private int comboCount = 0;
    private int lastScore = 0; // 마지막 판정 점수 (누적 아님)

    public TextMeshProUGUI scoreText;       // 현재 판정 점수
    public TextMeshProUGUI totalScoreText;  // 누적 점수
    public TextMeshProUGUI comboText;       // 콤보 표시

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 점수와 MISS 여부를 받아 점수 상태를 갱신합니다.
    /// </summary>
    public void AddScore(double amount, bool isMiss)
    {
        if (isMiss)
            comboCount = 0;
        else
            comboCount++;

        // 100 단위 절삭
        int cutScore = Mathf.FloorToInt((float)(amount / 100)) * 100;

        // totalScore는 누적
        totalScore += cutScore;

        // lastScore는 이번에 받은 점수만 저장 (표시용)
        lastScore = cutScore;

        // 점수 UI 업데이트
        scoreText.text = $"{lastScore:D4}";
        totalScoreText.text = ((int)totalScore).ToString();

        // 콤보 UI 업데이트
        comboText.text = comboCount > 0 ? $"{comboCount} COMBO" : "";
    }

    public double GetTotalScore() => totalScore;
    public int GetCombo() => comboCount;
} 