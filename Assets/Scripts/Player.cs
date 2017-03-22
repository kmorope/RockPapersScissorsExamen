using System.Collections;
using System.Collections.Generic;

public class Player
{
	public List<Card> _cartas = new List<Card>();

    public PlayersType _tipoJugador;

    public int puntos { get; set; }

    public Player(int p,PlayersType pt)
    {
        this.puntos = p;
        this._tipoJugador = pt;
    }

    public void IncrementPoints(int p)
    {
        this.puntos = this.puntos + p;
    }
     
}