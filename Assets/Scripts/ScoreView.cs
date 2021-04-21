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
            _text.text = value == 0 ? "Score: 0" : $"Score: {value}";
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
