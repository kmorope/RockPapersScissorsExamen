using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishController : MonoBehaviour
{

    public Text ResulText;
	 
	void Start ()
	{
	    var resultado = PlayerPrefs.GetInt("resultado");
	    if (resultado == 0)
	    {
	        ResulText.text = "Empate ¯\\_(ツ)_/¯";
	    }
        if (resultado == 1)
        {
            ResulText.text = "Ganaste!!! (•‿•)";
        }
        if (resultado == 2)
        {
            ResulText.text = "Perdiste ノಠ_ಠノ";
        }
    }

    public void BackToMenu()
    {
        SceneLoader.LoadScene("Welcome");
    }
	 
}
