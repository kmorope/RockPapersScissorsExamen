﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{

    public GameObject _PnlPanel;
    public Text _txtPlayer;
    public Text _txtJugador;

    
    // Use this for initialization
    void Start()
    {
        LoadVictorias();
        HidePanel();
    }

    public void ShowPanel()
    {
        _PnlPanel.SetActive(true);
    } 

    public void HidePanel()
    {
        _PnlPanel.SetActive(false);
    }

    public static void SetVictoria(PlayersType jugador)
    {
        if (jugador == PlayersType.Jugador)
        {
            var vP = PlayerPrefs.GetInt("V_PLAYER");
            vP += 1;
            PlayerPrefs.SetInt("V_PLAYER", vP);
        }
        else
        {
            var vN = PlayerPrefs.GetInt("V_NPC");
            vN += 1;
            PlayerPrefs.SetInt("V_NPC", vN);
        } 
    }

    public void LoadVictorias()
    {
        _txtPlayer.text =  PlayerPrefs.HasKey("V_PLAYER")? PlayerPrefs.GetInt("V_PLAYER").ToString() : "0";
        _txtJugador.text =  PlayerPrefs.HasKey("V_NPC")? PlayerPrefs.GetInt("V_NPC").ToString() : "0";
    }

    public void RestartHistorico()
    {
        PlayerPrefs.SetInt("V_PLAYER", 0);
        PlayerPrefs.SetInt("V_NPC", 0);
    }
}
