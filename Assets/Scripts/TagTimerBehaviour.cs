using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagTimerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private float m_playerOneRemainingTime;
    
    [SerializeField] private float m_playerTwoRemainingTime;

    [SerializeField] private TextMeshProUGUI _victoryText;

    private string _currentPlayerName;

    private bool _player1Tagged = true;

    private bool _player2Tagged = false;

    private void Start()
    {
        _currentPlayerName = _text.text;
    }
    private void Update()
    {
        if (m_playerOneRemainingTime <= 0.0f)
        {
            _text.text = "";
            _victoryText.text = "Player 2 wins!";
        }

        else if (m_playerTwoRemainingTime <= 0.0f)
        {
            _text.text = "";
            _victoryText.text = "Player 1 wins!";
        }

        if(_player1Tagged && m_playerOneRemainingTime > 0.0f)
        {
            _text.text = _currentPlayerName + m_playerOneRemainingTime;
            m_playerOneRemainingTime -= Time.deltaTime;
        }
        else if(_player2Tagged && m_playerTwoRemainingTime > 0.0f)
        {
            _text.text = _currentPlayerName + m_playerTwoRemainingTime;
            m_playerTwoRemainingTime -= Time.deltaTime;
        }

    }
    public void SetPlayer1Tagged()
    {
        _player1Tagged = true;
        _player2Tagged = false;
    }
    public void SetPlayer2Tagged()
    {
        _player2Tagged = true;
        _player1Tagged = false;
    }
}
