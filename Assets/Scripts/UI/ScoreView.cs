using TMPro;
using UnityEngine;

/// <summary>
/// Счет игрока
/// </summary>
public class ScoreView : MonoBehaviour
{
    /// <summary>
    /// Общий счет
    /// </summary>
    public int Score
    {
        set
        {
            _score = value;
            _scoreText.text = $"Счет: {value}";
            PlayerPrefsController.SetScore(_score);

        }
        get
        {
            //_scoreText.text = _score == 0 ? "Счет: 0" : $"Счет: {_score}";
            return _score;
        } 
    }
    
    /// <summary>
    /// Текущий счет в игре
    /// </summary>
    public int CurrentScore
    {
        set
        {
            _currentScore = value;
            _scoreText.text = $"Счет: {value}";
        }
        get
        {
            //_scoreText.text = _currentScore == 0 ? "Счет: 0" : $"Счет: {_currentScore}";
            return _currentScore;
        } 
    }
    
    [SerializeField] private TMP_Text _scoreText;
    
    private int _score;
    private int _currentScore;


    private void Start()
    {
        CurrentScore = 0;
        Score = PlayerPrefs.GetInt("Score");
    }
}
