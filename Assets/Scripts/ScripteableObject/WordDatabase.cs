using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BingoTheme
{
    public string word;
    public int difficulty;
    public bool selected;
}

[System.Serializable]
public class WordDatabase
{
    public string themeName;
    public List<BingoTheme> bingoTheme = new List<BingoTheme>();
}
