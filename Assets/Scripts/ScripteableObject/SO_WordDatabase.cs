using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordDatabase", menuName = "ScriptableObjects/WordDatabase", order = 1)]
public class SO_WordDatabase : ScriptableObject
{
    public List<WordDatabase> wordDatabaseList;
}