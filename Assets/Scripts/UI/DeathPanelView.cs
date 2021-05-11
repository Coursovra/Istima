using TMPro;
using UnityEngine;

public class DeathPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private ScoreView _scoreView;

    private void OnEnable()
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + _scoreView.CurrentScore);
        _scoreText.text = "Очков: " + PlayerPrefs.GetInt("Score");
        var bestScore = PlayerPrefs.GetInt("BestScore");
        if (_scoreView.CurrentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", _scoreView.CurrentScore);
        }
        _bestScoreText.text = "Рекорд: " + PlayerPrefs.GetInt("BestScore");
        _currentScoreText.text = $"+{_scoreView.CurrentScore}";
    }
}
