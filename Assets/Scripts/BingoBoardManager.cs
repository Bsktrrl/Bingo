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
        print("Button Pressed");

        //----- Print x amount of boards from a selected theme -----//

        //Generate a list of all boards to be printed
        List<List<string>> boardList = new List<List<string>>();
        List<string> temp = new List<string>();

        for (int i = 0; i < printAmount; i++)
        {
            print("boardList.Add: " + i);

            boardList.Add(temp);

            for (int j = 0; j < cellList.Count; j++)
            {
                boardList[i].Add("");
            }
        }


        //-----


        //----- Fill the boardList with strings from correct List -----//

        //Delegate words according to difficulty
        for (int i = 0; i < printAmount; i++)
        {
            for (int j = 0; j < cellList.Count; j++)
            {
                print("Delegate words: " + j);

                boardList[i][j] = wordDatabase.wordDatabaseList[boardThemeIndex].bingoTheme[j].word;
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
            print("print all boards: " + i);

            //Display correct board
            for (int j = 0; j < cellList.Count; j++)
            {
                cellList[j].GetComponentInChildren<TextMeshProUGUI>().text = boardList[i][j];
            }

            yield return new WaitForSeconds(0.01f);

            //Print displaying board
            if (printAmount > 0 && boardThemeName != "")
            {
                print("ScreenCapture: " + i);

                ScreenCapture.CaptureScreenshot(boardThemeName + " - BingoSheet " + i + ".png", 1);
            }

            yield return new WaitForSeconds(0.01f);
        }
        
        bingoMenu.SetActive(false);
        registerMenu.SetActive(true);
    }
}
