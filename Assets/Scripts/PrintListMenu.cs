using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrintListMenu : MonoBehaviour
{
    EditBingoMenu editBingoMenu;

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
    [SerializeField] TextMeshProUGUI PrintNumberText;
    [SerializeField] int PrintNumber;


    //--------------------


    private void Awake()
    {
        editBingoMenu = FindObjectOfType<EditBingoMenu>();

        DimentionDisplay.text = "-";

        PrintNumber = 0;
        PrintNumberText.text = PrintNumber.ToString();

        for (int i = 0; i < difficulty_amount.Count; i++)
        {
            difficulty_amount[i] = 0;
            SelectedText[i].text = difficulty_amount[i].ToString();
        }

        BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 10);

        InstantiateBingoLists();
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
        PrintNumber = 0;
        #endregion

        #region Reset difficulty_amount
        for (int i = 0; i < difficulty_amount.Count; i++)
        {
            difficulty_amount[i] = 0;
        }
        #endregion

        for (int i = 0; i < editBingoMenu.bingoNameDisplayList.Count; i++)
        {
            BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta.y + 110);

            bingoDisplayList.Add(Instantiate(BingoScroll_Prefab, Vector3.zero, Quaternion.identity) as GameObject);
            bingoDisplayList[bingoDisplayList.Count - 1].transform.parent = BingoScroll_Parent.transform;
            bingoDisplayList[bingoDisplayList.Count - 1].GetComponent<BingoViewPrefab>().bingoName.text = editBingoMenu.bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().bingoName.text;

            if (BingoScroll_Parent.GetComponent<RectTransform>().sizeDelta.y <= 1030)
                BingoScrollView.GetComponent<ScrollRect>().vertical = false;
            else
                BingoScrollView.GetComponent<ScrollRect>().vertical = true;
        }
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
        PrintNumber += 1;
        PrintNumberText.text = PrintNumber.ToString();
    }
    public void Up_10_Print_Button()
    {
        PrintNumber += 10;
        PrintNumberText.text = PrintNumber.ToString();
    }
    public void Down_1_Print_Button()
    {
        PrintNumber -= 1;
        if (PrintNumber <= 0)
            PrintNumber = 0;

        PrintNumberText.text = PrintNumber.ToString();
    }
    public void Down_10_Print_Button()
    {
        PrintNumber -= 10;
        if (PrintNumber <= 0)
            PrintNumber = 0;

        PrintNumberText.text = PrintNumber.ToString();
    }
}
