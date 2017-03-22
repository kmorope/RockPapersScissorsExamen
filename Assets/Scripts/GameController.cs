using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {

	private static Player _jugador = new Player(0,PlayersType.Jugador);

	private static Player _npc = new Player(0,PlayersType.NPC);

	private List<Card> _cartas = new List<Card>();

    public static int _playerCardInGame = -1;

    public static int _npcCardInGame;

    public Text PlayerText;

    public Text NpcText;

    public AudioClip Win;

    public AudioClip Lose;

    public AudioClip Table;

    public AudioClip Error;

    public AudioSource _audioManager;

    public Image _barFill;

    public int _numTurnos;

    public int _numCartas;
  
    private bool playNext = false;

    private int times = 0;

    private float turno = 0;

    void Start()
	{ 
		RepartirCartas(_jugador);
		RepartirCartas(_npc);
	    _jugador.puntos = 0;
        _npc.puntos = 0;
        SetPoints(_jugador, PlayerText);
        SetPoints(_npc, NpcText);
    }

    void Update()
    {
        if (playNext)
        {
            times++;
            if (times == 120)
            {
                EjecutarReglasJuego();
                times = 0;
                playNext = false;
            }
        }
    }

	public void RepartirCartas(Player player){
		LlenarBolsa();
		var _playerCards = new List<Card>();
		for (int i = 0; i < _numCartas; i++)
		{ 
			int index = Random.Range(0,_cartas.Count);
			_playerCards.Add(_cartas[index]);
			_cartas.Remove(_cartas[index]);
		}		 
		player._cartas = _playerCards;
	}

	public void LlenarBolsa(){
		_cartas.Clear ();
		for (int i = 0; i < 3; i++)
		{
			_cartas.Add(new Card(CardsType.Piedra));
			_cartas.Add(new Card(CardsType.Papel));
			_cartas.Add(new Card(CardsType.Tijera));
		}
	}

    public static List<Card> VerCartasJugador()
    {
        return _jugador._cartas;
    }

    public static List<Card> VerCartasNpc()
    {
        return _npc._cartas;
    }

    public void SetPoints(Player pl, Text txtField)
    { 
        txtField.text = Convert.ToString(pl.puntos);
    }

    public void IncrementPoints(Player pl,Text txtField)
    { 
        pl.IncrementPoints(1);
        txtField.text = Convert.ToString(pl.puntos);
    }

    public void JugarCarta()
    {
        var objects = GameObject.FindGameObjectsWithTag("PlayerCard"); 
        foreach (var obj in objects)
        {
            obj.GetComponent<Button>().interactable = false;
        }
        if (_playerCardInGame != -1)
        {
            JugarCartaNpc();
            playNext = true;
        }
        else
        {
            if (PlayerPrefs.GetInt("audio") == 1)
            {
                _audioManager.clip = Error;
                _audioManager.Play();
            }
               
        }
        foreach (var obj in objects)
        {
            obj.GetComponent<Button>().interactable = true;
        }

    }

    public void JugarCartaNpc()
    {
        _npcCardInGame = Random.Range(0, _npc._cartas.Count);
        var npcCard = GameObject.FindGameObjectWithTag("NpcCard");
        if (npcCard != null)
        {
            var rt = npcCard.GetComponent(typeof(RectTransform)) as RectTransform;
            rt.sizeDelta = new Vector2(122, 137);
            var image = npcCard.GetComponent(typeof(Image)) as Image;
            var carta = _npc._cartas[_npcCardInGame].TipoCarta().ToString().ToLower();
            image.sprite = Resources.Load("Images/" + carta, typeof(Sprite)) as Sprite;
        }
    }

    public void RestaurarCartaNpc()
    { 
        var npcCard = GameObject.FindGameObjectWithTag("NpcCard");
        if (npcCard != null)
        {
            var rt = npcCard.GetComponent(typeof(RectTransform)) as RectTransform;
            rt.sizeDelta = new Vector2(89, 100);
            var image = npcCard.GetComponent(typeof(Image)) as Image; 
            image.sprite = Resources.Load("Images/giphy", typeof(Sprite)) as Sprite;
        }
    }

    public void RestaurarCartasJugador()
    {
        var playerCard = GameObject.Find("player_"+ _playerCardInGame);
        if (playerCard != null)
        {
            playerCard.SetActive(false);
        }
    }

    public void EjecutarReglasJuego()
    {
        var cartaPlayer = _jugador._cartas[_playerCardInGame].TipoCarta();
        var cartaNpc = _npc._cartas[_npcCardInGame].TipoCarta();

        if (cartaPlayer == CardsType.Piedra && cartaNpc == CardsType.Piedra)
        {
            //Empate
            _audioManager.clip = Table;
        }
        if (cartaPlayer == CardsType.Piedra && cartaNpc == CardsType.Papel)
        {
            //Gana NPC
            IncrementPoints(_npc, NpcText);
            _audioManager.clip = Lose;
        }
        if (cartaPlayer == CardsType.Piedra && cartaNpc == CardsType.Tijera)
        {
            //Gana Player
            IncrementPoints(_jugador, PlayerText);
            _audioManager.clip = Win;
        }
        if (cartaPlayer == CardsType.Papel && cartaNpc == CardsType.Piedra)
        {
            //Gana Player
            IncrementPoints(_jugador, PlayerText);
            _audioManager.clip = Win;
        }
        if (cartaPlayer == CardsType.Papel && cartaNpc == CardsType.Papel)
        {
            //Empate
            _audioManager.clip = Table;
        }
        if (cartaPlayer == CardsType.Papel && cartaNpc == CardsType.Tijera)
        {
            //Gana NPC
            IncrementPoints(_npc, NpcText);
            _audioManager.clip = Lose;
        }
        if (cartaPlayer == CardsType.Tijera && cartaNpc == CardsType.Piedra)
        {
            //Gana NPC
            IncrementPoints(_npc, NpcText);
            _audioManager.clip = Lose;
        }
        if (cartaPlayer == CardsType.Tijera && cartaNpc == CardsType.Papel)
        {
            //Gana Player
            IncrementPoints(_jugador, PlayerText);
            _audioManager.clip = Win;
        }
        if (cartaPlayer == CardsType.Tijera && cartaNpc == CardsType.Tijera)
        {
            //Empate
            _audioManager.clip = Table;
        }
       
        if(PlayerPrefs.GetInt("audio") == 1)
            _audioManager.Play();
        
        RestaurarCartaNpc();
        RestaurarCartasJugador();
        EliminarCartasJugadas();

        turno += 1;
        float fill = turno / float.Parse( _numTurnos + ".2",System.Globalization.CultureInfo.InvariantCulture);
        _barFill.fillAmount = fill;

        if (turno >= _numTurnos)
        {
            if (_jugador.puntos > _npc.puntos)
            {
                //Gana Jugador
                PlayerPrefs.SetInt("resultado",1);
                HistoryController.SetVictoria(PlayersType.Jugador);
            }
            if (_jugador.puntos < _npc.puntos)
            {
                //Pierde Jugador
                PlayerPrefs.SetInt("resultado", 2);
                HistoryController.SetVictoria(PlayersType.NPC);
            }
            if (_jugador.puntos == _npc.puntos)
            {
                //Empate
                PlayerPrefs.SetInt("resultado", 0);
            }
            SceneLoader.LoadScene("Finish");
        }
    }

    public void EliminarCartasJugadas()
    { 
        var cartaNpc = _npc._cartas[_npcCardInGame];

        _npc._cartas.Remove(cartaNpc); 

        _playerCardInGame = -1;
        _npcCardInGame = 0;
    }
     
}
