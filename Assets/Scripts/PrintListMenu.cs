using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintListMenu : MonoBehaviour
{
    EditBingoMenu editBingoMenu;
    DataManager dataManager;

    [Header("Dimentions")]
    [SerializeField] TextMeshProUGUI DimentionDisplay;
    [SerializeField] TextMeshProUGUI DimentionOption_x;
    [SerializeField] TextMeshProUGUI DimentionOption_y;
    public int Dimention_x;
    public int Dimention_y;

    [Header("BingoList")]
    [SerializeField] GameObject BingoScrollView;
    [SerializeField] GameObject BingoScroll_Parent;
    [SerializeField] GameObject BingoScroll_Prefab;
    public List<GameObject> bingoDisplayList = new List<GameObject>();

    [Header("Difficulty")]
    [SerializeField] List<TextMeshProUGUI> SelectedText;
    public List<int> difficulty_amount = new List<int>();

    [Header("PrintAmount")]
    [SerializeField] TextMeshProUGUI PrintAmountText;
    public int printAmount;

    [Header("Info Tab")]
    [SerializeField] GameObject bingoTab;
    [SerializeField] TextMeshProUGUI bingoName_Text;
    [SerializeField] TextMeshProUGUI wordsInBingo_Text;
    [SerializeField] TextMeshProUGUI dimention_Text;
    [SerializeField] TextMeshProUGUI printAmount_Text;

    [SerializeField] TextMeshProUGUI inBingo_Diff_0_Text;
    [SerializeField] TextMeshProUGUI inBingo_Diff_1_Text;
    [SerializeField] TextMeshProUGUI inBingo_Diff_2_Text;
    [SerializeField] TextMeshProUGUI inBingo_Diff_3_Text;
    [SerializeField] TextMeshProUGUI inBingo_Diff_4_Text;
    [SerializeField] TextMeshProUGUI inBingo_Diff_5_Text;

    [SerializeField] TextMeshProUGUI selected_Diff_0_Text;
    [SerializeField] TextMeshProUGUI selected_Diff_1_Text;
    [SerializeField] TextMeshProUGUI selected_Diff_2_Text;
    [SerializeField] TextMeshProUGUI selected_Diff_3_Text;
    [SerializeField] TextMeshProUGUI selected_Diff_4_Text;
    [SerializeField] TextMeshProUGUI selected_Diff_5_Text;

    [SerializeField] TextMeshProUGUI ready_Text;
    [SerializeField] Image ready_background;


    //--------------------


    private void Awake()
    {
        editBingoMenu = FindObjectOfType<EditBingoMenu>();
        dataManager = FindObjectOfType<DataManager>();

        DimentionDisplay.text = "-";

        printAmount = 0;
        PrintAmountText.text = printAmount.ToString();

        for (int i = 0; i < difficulty_amount.Count; i++)
        {
            difficulty_amount[i] = 0;
            SelectedText[i].text = difficulty_amount[i].ToString();
        }

        BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 10);

        InstantiateBingoLists();
    }
    private void Update()
    {
        InfoTab();
    }


    //--------------------


    public void InstantiateBingoLists()
    {
        #region Reset bingoDisplayList
        for (int i = 0; i < bingoDisplayList.Count; i++)
            bingoDisplayList[i].GetComponent<BingoViewPrefab>().DestroyPrefab();

        bingoDisplayList.Clear();

        BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 10);
        #endregion

        #region Reset PrintNumber
        printAmount = 0;
        #endregion

        #region Reset difficulty_amount
        for (int i = 0; i < difficulty_amount.Count; i++)
        {
            difficulty_amount[i] = 0;
        }
        #endregion

        for (int i = 0; i < dataManager.bingoList.Count; i++)
        {
            BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta.y + 110);

            bingoDisplayList.Add(Instantiate(BingoScroll_Prefab, Vector3.zero, Quaternion.identity) as GameObject);
            bingoDisplayList[bingoDisplayList.Count - 1].transform.parent = BingoScroll_Parent.transform;
            bingoDisplayList[bingoDisplayList.Count - 1].GetComponent<BingoViewPrefab>().bingoName.text = dataManager.bingoList[i].bingoName;

            if (BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta.y <= 1030)
                BingoScrollView.GetComponent<ScrollRect>().vertical = false;
            else
                BingoScrollView.GetComponent<ScrollRect>().vertical = true;
        }
    }


    //--------------------


    void InfoTab()
    {
        //Find selected Bingo
        Bingo bingoInDatabase = new Bingo();
        for (int i = 0; i < bingoDisplayList.Count; i++)
        {
            if (bingoDisplayList[i].GetComponent<BingoViewPrefab>().isActive)
            {
                for (int j = 0; j < dataManager.bingoList.Count; j++)
                {
                    if (dataManager.bingoList[j].bingoName == bingoDisplayList[i].GetComponent<BingoViewPrefab>().bingoName.text)
                    {
                        bingoInDatabase = dataManager.bingoList[j];
                    }
                }
            }
        }

        //Selected Bingo Name
        bingoName_Text.text = "Bingo Name: " + bingoInDatabase.bingoName;

        //Words in selected Bingo
        wordsInBingo_Text.text = "Words in Bingo: " + bingoInDatabase.bingoElements.Count;

        //Selected Dimention
        dimention_Text.text = "Dimention: " + Dimention_x + "x" + Dimention_y + " = " + (Dimention_x * Dimention_y) + " words required";

        //Selected Amount of Prints
        printAmount_Text.text = "Print Amount: " + printAmount.ToString();

        //Amount of elements of each Difficulty
        #region
        int B_diff_0 = 0;
        int B_diff_1 = 0;
        int B_diff_2 = 0;
        int B_diff_3 = 0;
        int B_diff_4 = 0;
        int B_diff_5 = 0;

        for (int i = 0; i < bingoInDatabase.bingoElements.Count; i++)
        {
            if (bingoInDatabase.bingoElements[i].difficulty <= 0)
                B_diff_0++;
            else if (bingoInDatabase.bingoElements[i].difficulty == 1)
                B_diff_1++;
            else if (bingoInDatabase.bingoElements[i].difficulty == 2)
                B_diff_2++;
            else if (bingoInDatabase.bingoElements[i].difficulty == 3)
                B_diff_3++;
            else if (bingoInDatabase.bingoElements[i].difficulty == 4)
                B_diff_4++;
            else if (bingoInDatabase.bingoElements[i].difficulty == 5)
                B_diff_5++;
        }

        inBingo_Diff_0_Text.text = B_diff_0.ToString();
        inBingo_Diff_1_Text.text = B_diff_1.ToString();
        inBingo_Diff_2_Text.text = B_diff_2.ToString();
        inBingo_Diff_3_Text.text = B_diff_3.ToString();
        inBingo_Diff_4_Text.text = B_diff_4.ToString();
        inBingo_Diff_5_Text.text = B_diff_5.ToString();
        #endregion

        //Amount of Difficulties selected
        selected_Diff_1_Text.text = difficulty_amount[0].ToString();
        selected_Diff_2_Text.text = difficulty_amount[1].ToString();
        selected_Diff_3_Text.text = difficulty_amount[2].ToString();
        selected_Diff_4_Text.text = difficulty_amount[3].ToString();
        selected_Diff_5_Text.text = difficulty_amount[4].ToString();

        //Check if Bingo is "Ready" for Print
        #region
        bool readyCheck = false;

        //A Bingo is selected
        int isActiveCounter = 0;
        for (int i = 0; i < bingoDisplayList.Count; i++)
        {
            if (bingoDisplayList[i].GetComponent<BingoViewPrefab>().isActive)
                isActiveCounter++;

            if (isActiveCounter > 0)
            {
                readyCheck = true;
                bingoTab.SetActive(true);
            }
            else
            {
                readyCheck = false;
                bingoTab.SetActive(false);
            }
        }

        if (readyCheck)
        {
            //There is enough words in the selected Bingo
            if (bingoInDatabase.bingoElements.Count >= (Dimention_x * Dimention_y))
                readyCheck = true;
            else
                readyCheck = false;
        }

        if (readyCheck)
        {
            //A Valid Dimention is selected
            if (Dimention_x > 0 && Dimention_y > 0 && Dimention_x <= 10 && Dimention_y <= 10)
                readyCheck = true;
            else
                readyCheck = false;
        }

        if (readyCheck)
        {
            //Print Amount is larger than 0
            if (printAmount > 0)
                readyCheck = true;
            else
                readyCheck = false;
        }

        if (readyCheck)
        {
            //There is selected enough amount of Difficulty
            if ((difficulty_amount[0] + difficulty_amount[1] + difficulty_amount[2] + difficulty_amount[3] + difficulty_amount[4]) >= (Dimention_x * Dimention_y))
                readyCheck = true;
            else
                readyCheck = false;
        }

        if (readyCheck)
        {
            //There is enough amount of words of each difficulty in the selected BingoList
            if (B_diff_1 >= difficulty_amount[0] && B_diff_2 >= difficulty_amount[1] && B_diff_3 >= difficulty_amount[2] && B_diff_4 >= difficulty_amount[3] && B_diff_5 >= difficulty_amount[4])
                readyCheck = true;
            else
                readyCheck = false;
        }


        //-----


        if (readyCheck)
        {
            ready_Text.text = "Ready!";
            ready_background.color = new Color(0.69f, 0.86f, 0.93f, 1);
        }
        else
        {
            ready_Text.text = "Not Ready for Printing";
            ready_background.color = new Color(0.55f, 0.41f, 0.41f, 1);
        }
        #endregion
    }


    //--------------------


    public void Dimention_4x4_Button()
    {
        DimentionDisplay.text = "4x4";

        Dimention_x = 4;
        Dimention_y = 4;
    }
    public void Dimention_5x5_Button()
    {
        DimentionDisplay.text = "5x5";

        Dimention_x = 5;
        Dimention_y = 5;
    }
    public void Dimention_Confirm_Button()
    {
        print(DimentionOption_x.text);
        print(DimentionOption_y.text);

        if (DimentionOption_x.text == "1")
            Dimention_x = 1;
        else if (DimentionOption_x.text == "2")
            Dimention_x = 2;
        else if (DimentionOption_x.text == "3")
            Dimention_x = 3;
        else if (DimentionOption_x.text == "4")
            Dimention_x = 4;
        else if (DimentionOption_x.text == "5")
            Dimention_x = 5;
        else if (DimentionOption_x.text == "6")
            Dimention_x = 6;
        else if (DimentionOption_x.text == "7")
            Dimention_x = 7;
        else if (DimentionOption_x.text == "8")
            Dimention_x = 8;
        else if (DimentionOption_x.text == "9")
            Dimention_x = 9;
        else if (DimentionOption_x.text == "10")
            Dimention_x = 10;

        if (DimentionOption_y.text == "1")
            Dimention_y = 1;
        else if (DimentionOption_y.text == "2")
            Dimention_y = 2;
        else if (DimentionOption_y.text == "3")
            Dimention_y = 3;
        else if (DimentionOption_y.text == "4")
            Dimention_y = 4;
        else if (DimentionOption_y.text == "5")
            Dimention_y = 5;
        else if (DimentionOption_y.text == "6")
            Dimention_y = 6;
        else if (DimentionOption_y.text == "7")
            Dimention_y = 7;
        else if (DimentionOption_y.text == "8")
            Dimention_y = 8;
        else if (DimentionOption_y.text == "9")
            Dimention_y = 9;
        else if (DimentionOption_y.text == "10")
            Dimention_y = 10;

        DimentionDisplay.text = Dimention_x.ToString() + "x" + Dimention_y.ToString();
    }


    //--------------------


    public void Dimention_Up_1_Button()
    {
        difficulty_amount[0] += 1;
        SelectedText[0].text = difficulty_amount[0].ToString();
    }
    public void Dimention_Up_2_Button()
    {
        difficulty_amount[1] += 1;
        SelectedText[1].text = difficulty_amount[1].ToString();
    }
    public void Dimention_Up_3_Button()
    {
        difficulty_amount[2] += 1;
        SelectedText[2].text = difficulty_amount[2].ToString();
    }
    public void Dimention_Up_4_Button()
    {
        difficulty_amount[3] += 1;
        SelectedText[3].text = difficulty_amount[3].ToString();
    }
    public void Dimention_Up_5_Button()
    {
        difficulty_amount[4] += 1;
        SelectedText[4].text = difficulty_amount[4].ToString();
    }

    public void Dimention_Down_1_Button()
    {
        difficulty_amount[0] -= 1;

        if (difficulty_amount[0] <= 0)
            difficulty_amount[0] = 0;

        SelectedText[0].text = difficulty_amount[0].ToString();
    }
    public void Dimention_Down_2_Button()
    {
        difficulty_amount[1] -= 1;

        if (difficulty_amount[1] <= 0)
            difficulty_amount[1] = 0;

        SelectedText[1].text = difficulty_amount[1].ToString();
    }
    public void Dimention_Down_3_Button()
    {
        difficulty_amount[2] -= 1;

        if (difficulty_amount[2] <= 0)
            difficulty_amount[2] = 0;

        SelectedText[2].text = difficulty_amount[2].ToString();
    }
    public void Dimention_Down_4_Button()
    {
        difficulty_amount[3] -= 1;

        if (difficulty_amount[3] <= 0)
            difficulty_amount[3] = 0;

        SelectedText[3].text = difficulty_amount[3].ToString();
    }
    public void Dimention_Down_5_Button()
    {
        difficulty_amount[4] -= 1;

        if (difficulty_amount[4] <= 0)
            difficulty_amount[4] = 0;

        SelectedText[4].text = difficulty_amount[4].ToString();
    }


    //--------------------


    public void Up_1_Print_Button()
    {
        printAmount += 1;
        PrintAmountText.text = printAmount.ToString();
    }
    public void Up_10_Print_Button()
    {
        printAmount += 10;
        PrintAmountText.text = printAmount.ToString();
    }
    public void Down_1_Print_Button()
    {
        printAmount -= 1;
        if (printAmount <= 0)
            printAmount = 0;

        PrintAmountText.text = printAmount.ToString();
    }
    public void Down_10_Print_Button()
    {
        printAmount -= 10;
        if (printAmount <= 0)
            printAmount = 0;

        PrintAmountText.text = printAmount.ToString();
    }
}
