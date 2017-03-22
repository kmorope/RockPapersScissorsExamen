using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card{

    private CardsType _tipoCarta;

    public Card(CardsType tipo){
        _tipoCarta = tipo;
    }

    public CardsType TipoCarta()
    {
        return _tipoCarta;
    }

}