using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class BingoBoardManager : MonoBehaviour
{
    #region Variables
    EditBingoMenu editBingoMenu;
    DataManager dataManager;
    PrintListMenu printListMenu;

    [Header("Canvases")]
    [SerializeField] GameObject registerMenu;
    [SerializeField] GameObject bingoMenu;
    [SerializeField] GameObject bingoBoard;

    [SerializeField] GameObject editList_Menu;
    [SerializeField] GameObject printList_Menu;

    [Header("Prefabs")]
    [SerializeField] GameObject cellPrefab;

    List<GameObject> cellList = new List<GameObject>();

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI warningMessage;

    [Header("Print Stats")]
    public int boardThemeIndex;
    public int printAmount;

    [SerializeField] List<int> difficultyArrange;
    [SerializeField] List<int> difficultyCheckList = new List<int>();
    #endregion


    //--------------------


    private void Awake()
    {
        editBingoMenu = FindObjectOfType<EditBingoMenu>();
        dataManager = FindObjectOfType<DataManager>();
        printListMenu = FindObjectOfType<PrintListMenu>();

        editList_Menu.SetActive(true);
        printList_Menu.SetActive(true);
        bingoMenu.SetActive(true);
        InstantiateBoard();
        bingoMenu.SetActive(false);
        editList_Menu.SetActive(false);
        printList_Menu.SetActive(false);


        //-----


        warningMessage.text = "";
    }
    private void Update()
    {
        //Get correct boardThemeIndex
        for (int i = 0; i < printListMenu.bingoDisplayList.Count; i++)
        {
            if (printListMenu.bingoDisplayList[i].GetComponent<BingoViewPrefab>().isActive)
            {
                for (int j = 0; j < editBingoMenu.bingoNameDisplayList.Count; j++)
                {
                    if (printListMenu.bingoDisplayList[i].GetComponent<BingoViewPrefab>().bingoName.text == editBingoMenu.bingoNameDisplayList[j].GetComponent<BingoNamePrefab>().bingoName.text)
                    {
                        boardThemeIndex = j;
                    }
                }
            }
        }

        //Get correct printAmount
        printAmount = printListMenu.printAmount;

        //Get correct difficultyArrange
        difficultyArrange[0] = 0;
        for (int i = 1; i < difficultyArrange.Count; i++)
        {
            difficultyArrange[i] = printListMenu.difficulty_amount[i - 1];
        }
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
        #region Exit Print

        //Exit Print if the User have selected too few numbers of each difficulty (combined), based on the BingoBordSize
        #region
        int tempInt = 0;
        for (int i = 0; i < difficultyArrange.Count; i++)
        {
            tempInt += difficultyArrange[i];
        }

        if (tempInt < cellList.Count)
        {
            warningMessage.text = "You have not selected enough words of each difficulty to fill a Bingo board of your selected size";
            //print("difficultyArrange.Count is less than cellList.Count");

            return;
        }
        #endregion

        //Exit Print if the User have selected too few numbers of each difficulty (NOT combined), based on the BingoBordSize
        #region
        tempInt = 0;
        for (int i = 0; i < difficultyArrange.Count; i++)
        {
            tempInt = 0;

            for (int j = 0; j < dataManager.bingoList[boardThemeIndex].bingoElements.Count; j++)
            {
                if (dataManager.bingoList[boardThemeIndex].bingoElements[j].difficulty == i)
                {
                    tempInt++;
                }
            }

            if (tempInt < difficultyArrange[i])
            {
                warningMessage.text = "You have too few words of difficulty " + i + " in your \"" + dataManager.bingoList[boardThemeIndex].bingoName + " list\".  to be able to fill a Bingo board with the difficulty amount you have selected";
                
                return;
            }
        }

        #endregion

        //Exit Print if wordDatabaseList contain of too few elements, based on the selected BingoBordSize
        #region
        if (dataManager.bingoList[boardThemeIndex].bingoElements.Count < cellList.Count)
        {
            warningMessage.text = "You have too few words in your \"" + dataManager.bingoList[boardThemeIndex].bingoName + "\" list to be able to fill a Bingo board of your selected size";
            //print("Too few elements in the selected bingoTheme, based on the BingoBoardSize");

            return; 
        }
        #endregion

        #endregion

        //----- Print x amount of boards from a selected theme -----//

        //Generate a list of all boards to be printed
        #region
        List<List<string>> boardList = new List<List<string>>();
        boardList.Clear();

        List<string> temp = new List<string>();
        for (int j = 0; j < cellList.Count; j++)
            temp.Add("");

        for (int i = 0; i < printAmount; i++)
        {
            boardList.Add(temp);
        }
        #endregion


        //-----


        //----- Fill the boardList with strings from correct List -----//
        #region
        //Delegate words randomly
        for (int i = 0; i < printAmount; i++)
        {
            difficultyCheckList.Clear();
            for (int j = 0; j < difficultyArrange.Count; j++)
                difficultyCheckList.Add(0);

            //Make a new List<string>
            List<string> tempString = new List<string>();
            for (int j = 0; j < cellList.Count; j++)
                tempString.Add("");

            //Calculate random indexes to use 
            for (int j = 0; j < cellList.Count;)
            {
                int indexCheck = Random.Range(0, dataManager.bingoList[boardThemeIndex].bingoElements.Count);

                //Check if indexCheck has already been used
                if (!dataManager.bingoList[boardThemeIndex].bingoElements[indexCheck].selected)
                {
                    //Check difficulty to see if selected indexCheck can be included
                    for (int k = 1; k < difficultyArrange.Count; k++)
                    {
                        if (dataManager.bingoList[boardThemeIndex].bingoElements[indexCheck].difficulty == k
                            && difficultyCheckList[k] < difficultyArrange[k])
                        {
                            //Set selected word in correct element position in tempString(List)
                            dataManager.bingoList[boardThemeIndex].bingoElements[indexCheck].selected = true;

                            tempString[j] = dataManager.bingoList[boardThemeIndex].bingoElements[indexCheck].word;

                            j++;
                            difficultyCheckList[k]++;

                            break;
                        }
                    }
                }
            }

            //Transfer data to boardList
            boardList[i] = tempString;

            //Reset selected words to false
            for (int j = 0; j < dataManager.bingoList[boardThemeIndex].bingoElements.Count; j++)
            {
                dataManager.bingoList[boardThemeIndex].bingoElements[j].selected = false;
            }
        }
        #endregion


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

            yield return new WaitForSeconds(0.01f);

            //Print displaying board
            if (printAmount > 0 && dataManager.bingoList[boardThemeIndex].bingoName != "")
            {
                ScreenCapture.CaptureScreenshot(dataManager.bingoList[boardThemeIndex].bingoName + " - BingoSheet " + i + ".png", 1);
            }

            yield return new WaitForSeconds(0.01f);
        }
        
        bingoMenu.SetActive(false);
        registerMenu.SetActive(true);
    }


    //--------------------


    public void EditListButton()
    {
        ExitBingoElements();

        editBingoMenu.InstantiateBingoNameLists();

        editList_Menu.SetActive(true);
        printList_Menu.SetActive(false);
    }
    public void PrintListButton()
    {
        ExitBingoElements();

        printListMenu.InstantiateBingoLists();

        editList_Menu.SetActive(false);
        printList_Menu.SetActive(true);
    }
    public void ExitProgramButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }


    //--------------------

    
    public void ExitBingoElements()
    {
        for (int i = 0; i < editBingoMenu.bingoElementDisplayList.Count; i++)
            editBingoMenu.bingoElementDisplayList[i].GetComponent<BingoElementPrefab>().DestroyElementPrefab();

        editBingoMenu.bingoElementDisplayList.Clear();

        editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 10);
    }
}
