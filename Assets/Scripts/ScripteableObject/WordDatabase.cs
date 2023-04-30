using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BingoTheme
{
    public string word;
    public int difficulty;
}

[System.Serializable]
public class WordDatabase
{
    public List<BingoTheme> bingoTheme = new List<BingoTheme>();
}
