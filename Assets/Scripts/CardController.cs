using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour {

    public void OnSelected(string player)
    {
        var objects = GameObject.FindGameObjectsWithTag(player);
        var objectCount = objects.Length;
        foreach (var obj in objects)
        {
            obj.transform.SetAsFirstSibling();
            var tempRt = obj.GetComponent(typeof(RectTransform)) as RectTransform;
            var tempImage = obj.GetComponent(typeof(Image)) as Image;
            if (tempRt != null && tempRt.sizeDelta == new Vector2(122, 137))
            {
                tempRt.sizeDelta = new Vector2(89, 100);
                tempImage.sprite = Resources.Load("Images/giphy", typeof(Sprite)) as Sprite;
            }
        }
        var rt = GetComponent(typeof(RectTransform)) as RectTransform;
        rt.sizeDelta = new Vector2(122, 137); 
        this.transform.SetSiblingIndex(-1); 
        var image = this.GetComponent(typeof(Image)) as Image;
        var cartas =   GameController.VerCartasJugador();
        var cartaImagen = string.Empty;
        if (player == "PlayerCard")
        {
            cartaImagen = cartas[Convert.ToInt32(name.Split('_')[1])].TipoCarta().ToString().ToLower();
        }
        image.sprite = Resources.Load("Images/" + cartaImagen, typeof(Sprite)) as Sprite;
        var soundClip = GetComponent(typeof(AudioSource)) as AudioSource;

        if (PlayerPrefs.GetInt("audio") == 1)
        {
            soundClip.Play();
        }
        GameController._playerCardInGame = Convert.ToInt32(name.Split('_')[1]);
    }
}
