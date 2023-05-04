using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BingoElements
{
    public string word;
    public int difficulty;
    public bool selected;
    public long index;
}

[System.Serializable]
public class Bingo
{
    public string bingoName;
    public List<BingoElements> bingoElements = new List<BingoElements>();
}

[System.Serializable]
public class DataManager : MonoBehaviour, IDataPersistance
{
    public List<Bingo> bingoList = new List<Bingo>();


    //--------------------


    public void LoadData(GameData gameData)
    {
        this.bingoList = gameData.bingoList;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.bingoList = this.bingoList;
    }
}
