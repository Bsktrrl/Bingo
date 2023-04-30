using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BingoBoardManager : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField] SO_WordDatabase wordDatabase;

    [Header("Canvases")]
    [SerializeField] GameObject registerMenu;
    [SerializeField] GameObject bingoMenu;
    [SerializeField] GameObject bingoBoard;

    [Header("Prefabs")]
    [SerializeField] GameObject cellPrefab;

    List<GameObject> cellList = new List<GameObject>();

    [Header("Print Stats")]
    public string boardThemeName;
    public int boardThemeIndex;
    public int printAmount;


    //--------------------


    private void Start()
    {
        bingoMenu.SetActive(true);

        InstantiateBoard();

        bingoMenu.SetActive(false);
    }


    //--------------------


    void InstantiateBoard()
    {
        for (int i = 0; i < 25; i++)
        {
            cellList.Add(Instantiate(cellPrefab, Vector3.zero, Quaternion.identity) as GameObject);
        }

        for (int i = 0; i < cellList.Count; i++)
        {
            cellList[i].transform.parent = bingoBoard.transform;
            cellList[i].name = "Cell " + (i + 1);
        }
    }


    //--------------------


    public void PrintBoardButton()
    {
        if (wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme.Count < cellList.Count) 
        { return; }

        //----- Print x amount of boards from a selected theme -----//

        //Generate a list of all boards to be printed
        List<List<string>> boardList = new List<List<string>>();
        boardList.Clear();

        List<string> temp = new List<string>();
        for (int j = 0; j < cellList.Count; j++)
        {
            temp.Add("");
        }

        for (int i = 0; i < printAmount; i++)
        {
            boardList.Add(temp);
        }


        //-----


        //----- Fill the boardList with strings from correct List -----//

        //Delegate words randomly
        for (int i = 0; i < boardList.Count; i++)
        {
            for (int k = 0; k < boardList[i].Count;)
            {
                int indexCheck = Random.Range(0, wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme.Count);

                if (!wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme[indexCheck].selected)
                {
                    wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme[indexCheck].selected = true;

                    //print("indexCheck A: i: " + i + " | k: " + k + " = " + (indexCheck + 1));
                    boardList[i][k] += wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme[indexCheck].word;
                    //print("indexCheck B: i: " + i + " | k: " + k + " = " + boardList[i][k]);

                    k++;
                }
            }

            //Reset selected words to false
            for (int j = 0; j < wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme.Count; j++)
            {
                wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme[j].selected = false;
            }
        }

        //Temp
        for (int i = 0; i < boardList.Count; i++)
        {
            for (int k = 0; k < boardList[i].Count; k++)
            {
                //print("Delegate words | i: " + i + " | j: " + k + " = " + boardList[i][k]);
            }
        }


        //-----


        //print all boards
        StartCoroutine(PrintScreen(boardList));
    }

    IEnumerator PrintScreen(List<List<string>> boardList)
    {
        bingoMenu.SetActive(true);
        registerMenu.SetActive(false);

        for (int i = 0; i < boardList.Count; i++)
        {
            //Display correct board
            for (int j = 0; j < cellList.Count; j++)
            {
                cellList[j].GetComponentInChildren<TextMeshProUGUI>().text = boardList[i][j];
            }

            yield return new WaitForSeconds(0.31f);

            //Print displaying board
            if (printAmount > 0 && boardThemeName != "")
            {
                ScreenCapture.CaptureScreenshot(boardThemeName + " - BingoSheet " + i + ".png", 1);
            }

            yield return new WaitForSeconds(0.31f);
        }
        
        bingoMenu.SetActive(false);
        registerMenu.SetActive(true);
    }
}
