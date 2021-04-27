using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public int Score
    {
        set
        {
            _score = value;
            _text.text = value == 0 ? "Счет: 0" : $"Счет: {value}";
            PlayerPrefs.SetInt("CurrentScore", _score);
        }
        get => _score;
    }
    private int _score;


    private void Start()
    {
        Score = PlayerPrefs.GetInt("CurrentScore");
    }
}
