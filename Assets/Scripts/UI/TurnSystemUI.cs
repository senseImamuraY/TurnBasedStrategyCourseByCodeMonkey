using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnNumberText;
    [SerializeField] private Button endTurnBtn;
    [SerializeField] private GameObject enemyTurnVisualGameObject;
     
    private void Start()
    {
        //SetTrunNumber();

        endTurnBtn.onClick.AddListener(() =>
        {
            //SetTrunNumber();
            TurnSystem.Instance.NextTrun();
        });

        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisiblity();
    }
    //public void SetTrunNumber()
    //{
    //    string num = TurnSystem.Instance.GetTurnNumber().ToString();
    //    turnNumberText.text = "TURN " + num;
    //    TurnSystem.Instance.NextTrun();
    //}

    private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
    {
        UpdateTurnText();
        UpdateEnemyTurnVisual();
        UpdateEndTurnButtonVisiblity();
    }

    private void UpdateTurnText()
    {
        turnNumberText.text = "TURN " + TurnSystem.Instance.GetTurnNumber();
    }
    
    private void UpdateEnemyTurnVisual()
    {
        enemyTurnVisualGameObject.SetActive(!TurnSystem.Instance.IsPlayerTurn());
    }

    private void UpdateEndTurnButtonVisiblity()
    {
        endTurnBtn.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
    }
}
