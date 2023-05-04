using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<Bingo> bingoList = new List<Bingo>();


    //--------------------


    public GameData()
    {
        this.bingoList.Clear();
    }
}
