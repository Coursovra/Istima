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
        //_scoreText.text = "Очков: " + PlayerPrefsController.GetScore();
        var bestScore = PlayerPrefsController.GetBestScore();
        if (_scoreView.CurrentScore > bestScore)
        {
            PlayerPrefsController.SetBestScore(_scoreView.CurrentScore);
        }

        _bestScoreText.text = "Рекорд: " + PlayerPrefsController.GetBestScore();
        _scoreText.text = $"+{_scoreView.CurrentScore}";
        _scoreView.Score += _scoreView.CurrentScore;

    }
}
