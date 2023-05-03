using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BingoElementPrefab : MonoBehaviour
{
    EditBingoMenu editBingoMenu;
    DataManager dataManager;

    public TextMeshProUGUI bingoElementName;
    public TextMeshProUGUI bingoElementInputFeld;

    public Difficulty difficulty;
    public List<Button> DifficultyButtons = new List<Button>();

    public long index;


    //--------------------


    private void Awake()
    {
        editBingoMenu = FindObjectOfType<EditBingoMenu>();
        dataManager = FindObjectOfType<DataManager>();

        bingoElementInputFeld.text = "-";

        difficulty = Difficulty.difficulty_0;
    }
    private void Update()
    {
        //Set DifficultyButton colors based on which is active
        for (int i = 0; i < DifficultyButtons.Count; i++)
        {
            switch (difficulty)
            {
                case Difficulty.difficulty_0:
                    DifficultyButtons[0].GetComponent<Image>().color = new Color(0.67f, 0.88f, 0.68f, 1);
                    DifficultyButtons[1].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[2].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[3].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[4].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[5].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
                case Difficulty.difficulty_1:
                    DifficultyButtons[0].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[1].GetComponent<Image>().color = new Color(0.67f, 0.88f, 0.68f, 1);
                    DifficultyButtons[2].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[3].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[4].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[5].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
                case Difficulty.difficulty_2:
                    DifficultyButtons[0].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[1].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[2].GetComponent<Image>().color = new Color(0.67f, 0.88f, 0.68f, 1);
                    DifficultyButtons[3].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[4].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[5].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
                case Difficulty.difficulty_3:
                    DifficultyButtons[0].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[1].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[2].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[3].GetComponent<Image>().color = new Color(0.67f, 0.88f, 0.68f, 1);
                    DifficultyButtons[4].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[5].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
                case Difficulty.difficulty_4:
                    DifficultyButtons[0].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[1].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[2].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[3].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[4].GetComponent<Image>().color = new Color(0.67f, 0.88f, 0.68f, 1);
                    DifficultyButtons[5].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    break;
                case Difficulty.difficulty_5:
                    DifficultyButtons[0].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[1].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[2].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[3].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[4].GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f, 1);
                    DifficultyButtons[5].GetComponent<Image>().color = new Color(0.67f, 0.88f, 0.68f, 1);
                    break;
                default:
                    break;
            }
        }
    }


    //--------------------


    public void Difficulty_0_Button_isPressed()
    {
        difficulty = Difficulty.difficulty_0;

        ChangeDifficultyInDatabase();
    }
    public void Difficulty_1_Button_isPressed()
    {
        difficulty = Difficulty.difficulty_1;

        ChangeDifficultyInDatabase();
    }
    public void Difficulty_2_Button_isPressed()
    {
        difficulty = Difficulty.difficulty_2;

        ChangeDifficultyInDatabase();
    }
    public void Difficulty_3_Button_isPressed()
    {
        difficulty = Difficulty.difficulty_3;

        ChangeDifficultyInDatabase();
    }
    public void Difficulty_4_Button_isPressed()
    {
        difficulty = Difficulty.difficulty_4;

        ChangeDifficultyInDatabase();
    }
    public void Difficulty_5_Button_isPressed()
    {
        difficulty = Difficulty.difficulty_5;

        ChangeDifficultyInDatabase();
    }

    void ChangeDifficultyInDatabase()
    {
        for (int i = 0; i < editBingoMenu.bingoNameDisplayList.Count; i++)
        {
            if (editBingoMenu.bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().isActive)
            {
                for (int j = 0; j < editBingoMenu.bingoElementDisplayList.Count; j++)
                {
                    dataManager.bingoList[i].bingoElements[j].difficulty = (int)editBingoMenu.bingoElementDisplayList[j].GetComponent<BingoElementPrefab>().difficulty;
                }
            }
        }
    }


    //--------------------


    public void AddButton()
    {
        bingoElementName.text = bingoElementInputFeld.text;

        for (int i = 0; i < editBingoMenu.bingoNameDisplayList.Count; i++)
        {
            if (editBingoMenu.bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().isActive)
            {
                for (int j = 0; j < editBingoMenu.bingoElementDisplayList.Count; j++)
                {
                    dataManager.bingoList[i].bingoElements[j].word = editBingoMenu.bingoElementDisplayList[j].GetComponent<BingoElementPrefab>().bingoElementName.text;
                    editBingoMenu.bingoElementDisplayList[j].name = editBingoMenu.bingoElementDisplayList[j].GetComponent<BingoElementPrefab>().bingoElementName.text;
                }
            }
        }
    }
    public void DeleteButton()
    {
        for (int i = 0; i < dataManager.bingoList.Count; i++)
        {
            for (int j = 0; j < dataManager.bingoList[i].bingoElements.Count; j++)
            {
                if (dataManager.bingoList[i].bingoElements[j].index == index)
                {
                    editBingoMenu.bingoElementDisplayList.RemoveAt(j);
                    editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700, editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y - 110);

                    dataManager.bingoList[i].bingoElements.RemoveAt(j);

                    DestroyElementPrefab();

                    i = dataManager.bingoList.Count;
                    break;
                }
            }
        }
    }

    public void DestroyElementPrefab()
    {
        Destroy(gameObject);
    }
}
