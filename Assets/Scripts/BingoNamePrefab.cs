using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BingoNamePrefab : MonoBehaviour
{
    BingoBoardManager bingoBoardManager;
    EditBingoMenu editBingoMenu;
    DataManager dataManager;

    public TextMeshProUGUI bingoName;
    [SerializeField] Button selectButton;

    public bool isActive;


    //--------------------


    private void Awake()
    {
        editBingoMenu = FindObjectOfType<EditBingoMenu>();
        dataManager = FindObjectOfType<DataManager>();
        bingoBoardManager = FindObjectOfType<BingoBoardManager>();

        bingoName.text = "-";
    }
    private void Update()
    {
        if (isActive)
        {
            selectButton.GetComponent<Image>().color = new Color(0.69f, 0.86f, 0.93f, 1);
        }
        else
        {
            selectButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }


    //--------------------

    public void AddButton()
    {
        //SelectButton();

        //Check if any Bingo is Active
        #region 
        //int activeCheck = 0;
        //for (int i = 0; i < editBingoMenu.bingoNameDisplayList.Count; i++)
        //{
        //    if (editBingoMenu.bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().isActive)
        //    {
        //        activeCheck++;
        //    }
        //}

        if (!gameObject.GetComponent<BingoNamePrefab>().isActive)
        { return; }
        #endregion

        //Add an element to the "editBingoMenu.bingoElementDisplayList"
        for (int i = 0; i < dataManager.bingoList.Count; i++)
        {
            if (dataManager.bingoList[i].bingoName == bingoName.text)
            {
                BingoElements bingoElementTemp = new BingoElements();
                dataManager.bingoList[i].bingoElements.Add(bingoElementTemp);
                dataManager.bingoList[i].bingoElements[dataManager.bingoList[i].bingoElements.Count - 1].word = "-";

                #region Element Index check
                List<long> tempCheck = new List<long>();
                bool whileCheck = false;
                
                for (int j = 0; j < dataManager.bingoList[i].bingoElements.Count; j++)
                {
                    tempCheck.Add(dataManager.bingoList[i].bingoElements[j].index);
                }

                while (!whileCheck)
                {
                    int counter = 0;
                    long randomNumber = 1000000000000000;

                    if (tempCheck.Count >= randomNumber)
                    {
                        //Warning Message that the user have used max elements in list, of 1000000000000000
                        return;
                    }

                    long randomNum = (long)Random.Range(0, randomNumber);

                    for (int j = 0; j < tempCheck.Count; j++)
                    {
                        if (tempCheck[j] == randomNum)
                        {
                            counter++;
                        }
                    }

                    if (counter <= 0)
                    {
                        dataManager.bingoList[i].bingoElements[dataManager.bingoList[i].bingoElements.Count - 1].index = randomNum;
                        whileCheck = true;
                    }
                }
                #endregion

                editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700, editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y + 110);

                editBingoMenu.bingoElementDisplayList.Add(Instantiate(editBingoMenu.bingoElementDisplay_Prefab, Vector3.zero, Quaternion.identity) as GameObject);
                editBingoMenu.bingoElementDisplayList[editBingoMenu.bingoElementDisplayList.Count - 1].transform.parent = editBingoMenu.bingoElementDisplay_Parent.transform;
                editBingoMenu.bingoElementDisplayList[editBingoMenu.bingoElementDisplayList.Count - 1].GetComponent<BingoElementPrefab>().index = dataManager.bingoList[i].bingoElements[dataManager.bingoList[i].bingoElements.Count - 1].index;

                if (editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y <= 1030)
                    editBingoMenu.BingoElementRect.GetComponent<ScrollRect>().vertical = false;
                else
                    editBingoMenu.BingoElementRect.GetComponent<ScrollRect>().vertical = true;

                break;
            }
        }
    }
    public void SelectButton()
    {
        #region Change active prefab
        if (isActive)
        {
            for (int i = 0; i < dataManager.bingoList.Count; i++)
                editBingoMenu.bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().isActive = false;

            isActive = false;

            bingoBoardManager.ExitBingoElements();

            return;
        }
        else
        {
            for (int i = 0; i < dataManager.bingoList.Count; i++)
                editBingoMenu.bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().isActive = false;

            isActive = true;

            bingoBoardManager.ExitBingoElements();
        }
        #endregion

        for (int i = 0; i < dataManager.bingoList.Count; i++)
        {
            if (dataManager.bingoList[i].bingoName == bingoName.text)
            {
                if (dataManager.bingoList[i].bingoElements.Count <= 0)
                { return; }

                for (int j = 0; j < dataManager.bingoList[i].bingoElements.Count; j++)
                {
                    editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700, editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y + 110);

                    editBingoMenu.bingoElementDisplayList.Add(Instantiate(editBingoMenu.bingoElementDisplay_Prefab, Vector3.zero, Quaternion.identity) as GameObject);
                    editBingoMenu.bingoElementDisplayList[j].transform.parent = editBingoMenu.bingoElementDisplay_Parent.transform;
                    editBingoMenu.bingoElementDisplayList[j].name = dataManager.bingoList[i].bingoElements[j].word;

                    editBingoMenu.bingoElementDisplayList[j].GetComponent<BingoElementPrefab>().bingoElementName.text = dataManager.bingoList[i].bingoElements[j].word;
                    editBingoMenu.bingoElementDisplayList[j].GetComponent<BingoElementPrefab>().difficulty = (Difficulty)dataManager.bingoList[i].bingoElements[j].difficulty;
                    editBingoMenu.bingoElementDisplayList[j].GetComponent<BingoElementPrefab>().index = dataManager.bingoList[i].bingoElements[j].index;
                }
            }
        }

        if (editBingoMenu.bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y <= 1030)
            editBingoMenu.BingoElementRect.GetComponent<ScrollRect>().vertical = false;
        else
            editBingoMenu.BingoElementRect.GetComponent<ScrollRect>().vertical = true;
    }

    public void DeleteButton()
    {
        for (int i = 0; i < dataManager.bingoList.Count; i++)
        {
            if (dataManager.bingoList[i].bingoName == bingoName.text)
            {
                dataManager.bingoList.RemoveAt(i);
                editBingoMenu.bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, editBingoMenu.bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y - 110);
                editBingoMenu.bingoNameDisplayList.RemoveAt(i);

                break;
            }
        }

        bingoBoardManager.ExitBingoElements();
        Destroy(gameObject);
    }

    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
