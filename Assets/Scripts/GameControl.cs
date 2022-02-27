using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{    
    private Canvas _canvas;
    private GameObject _gameBoard;
    private TextMeshPro _questionText;

    private GameObject _objQuestionText;

    [SerializeField] private TextMeshPro _questionT;
        
    void Start()
    {
        initGameboard();
        refreshGamepanel();
    }

    void initGameboard() 
    {
        _gameBoard = GameObject.Find("Gameboard");
        _canvas = _gameBoard.GetComponentInChildren<Canvas>();
       
        //_canvas.GetComponentInChildren<TextMeshPro>().text = "WOOOOOOOOOP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        
    private void refreshGamepanel() 
    {
        
    }
}
