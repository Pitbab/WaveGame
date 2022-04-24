using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{

    #region Components

    [SerializeField] private Slider hpSlider;

    [SerializeField] private TMP_Text currentRound;
    [SerializeField] private TMP_Text numberAlive;
    [SerializeField] private TMP_Text cash;
    [SerializeField] private TMP_Text info;
    [SerializeField] private TMP_Text mag;
    [SerializeField] private TMP_Text reserve;

    private Dictionary<EventID, TMP_Text> UiText = new Dictionary<EventID, TMP_Text>();

    #endregion

    #region Actions

    public Action<float> OnHpChange;
    public Action<EventID, int> OnCashChange;
    public Action<EventID, int> OnNumberAliveChange;
    public Action<EventID, int> OnRoundChange;
    //public Action<EventID, int> OnMagChange;

    #endregion

    #region Unity Events

    private void Start()
    {

        UiText.Add(EventID.Round, currentRound);
        UiText.Add(EventID.Alive, numberAlive);
        UiText.Add(EventID.Cash, cash);
        //UiText.Add(EventID.reload, mag);

        OnRoundChange += SetText;
        OnCashChange += SetText;
        OnNumberAliveChange += SetText;
        
        OnHpChange += SetHpSlider;
        hpSlider.maxValue = 100f;
        hpSlider.value = 100f;
        
        SetText(EventID.Round, 1);
    }
    
    private void OnDestroy()
    {
        OnHpChange -= SetHpSlider;
        
        OnRoundChange -= SetText;
        OnCashChange -= SetText;
        OnNumberAliveChange -= SetText;
    }

    #endregion

    #region Components Setter

    public void SetGunMag(int current, int maxCap)
    {
        mag.text = current.ToString();
        reserve.text = "/ " + maxCap;
    }
    
    private void SetText(EventID key, int value)
    {
        UiText[key].text = key + " : " + value;
    }

    public void SetInfo(string infoText)
    {
        info.text = infoText;
    }
    
    private void SetHpSlider(float value)
    {
        hpSlider.value = value;
    }

    #endregion



    



    
    
    
}
