using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditBingoMenu : MonoBehaviour
{
    BingoBoardManager bingoBoardManager;
    DataManager dataManager;

    [Header("BingoNameView")]
    [SerializeField] TextMeshProUGUI bingoName_Field;

    public GameObject bingoNameDisplay_Parent;
    [SerializeField] GameObject bingoNameDisplay_Prefab;
    [SerializeField] ScrollRect BingoViewRect;

    public List<GameObject> bingoNameDisplayList = new List<GameObject>();
    public List<GameObject> bingoElementDisplayList = new List<GameObject>();

    [Header("BingoElementView")]
    public GameObject bingoElementDisplay_Parent;
    public GameObject bingoElementDisplay_Prefab;
    public ScrollRect BingoElementRect;

    [Header("Other UI Elements")]
    [SerializeField] GameObject warningMessage;


    //--------------------


    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        bingoBoardManager = FindObjectOfType<BingoBoardManager>();

        bingoName_Field.text = "";
        warningMessage.gameObject.SetActive(false);

        bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 10);
        bingoElementDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(700, 10);
    }


    //--------------------


    public void MakeNewBingoButton_isPressed()
    {
        warningMessage.SetActive(false);
        bingoBoardManager.ExitBingoElements();


        //-----


        if (bingoName_Field.text != "")
        {
            #region Exit Making of new List
            for (int i = 0; i < dataManager.bingoList.Count; i++)
            {
                if (dataManager.bingoList[i].bingoName == bingoName_Field.text)
                {
                    warningMessage.SetActive(true);
                    warningMessage.GetComponentInChildren<TextMeshProUGUI>().text = "You already have a Bingo named \"" + bingoName_Field.text + "\"";
                   
                    return;
                }
            }
            #endregion

            bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y + 110);

            if (bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y <= 1030)
                BingoViewRect.GetComponent<ScrollRect>().vertical = false;
            else
                BingoViewRect.GetComponent<ScrollRect>().vertical = true;

            Bingo bingo = new Bingo();
            dataManager.bingoList.Add(bingo);

            dataManager.bingoList[dataManager.bingoList.Count - 1].bingoName = bingoName_Field.text;

            //Instantiate Bingo Name List
            bingoNameDisplayList.Add(Instantiate(bingoNameDisplay_Prefab, Vector3.zero, Quaternion.identity) as GameObject);
            bingoNameDisplayList[dataManager.bingoList.Count - 1].transform.parent = bingoNameDisplay_Parent.transform;
            bingoNameDisplayList[dataManager.bingoList.Count - 1].name = dataManager.bingoList[dataManager.bingoList.Count - 1].bingoName;
            bingoNameDisplayList[dataManager.bingoList.Count - 1].GetComponentInChildren<BingoNamePrefab>().bingoName.text = dataManager.bingoList[dataManager.bingoList.Count - 1].bingoName;

            bingoName_Field.text = "";

            //Set new List active
            for (int i = 0; i < dataManager.bingoList.Count; i++)
                bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().isActive = false;

            bingoNameDisplayList[dataManager.bingoList.Count - 1].GetComponentInChildren<BingoNamePrefab>().isActive = true;
        }
        else
        {
            warningMessage.SetActive(true);
            warningMessage.GetComponentInChildren<TextMeshProUGUI>().text = "You must give your new Bingo a name";
            
            return;
        }
    }

    public void InstantiateBingoNameLists()
    {
        #region Reset bingoNameDisplayList
        for (int i = 0; i < bingoNameDisplayList.Count; i++)
            bingoNameDisplayList[i].GetComponent<BingoNamePrefab>().DestroyPrefab();

        bingoNameDisplayList.Clear();

        bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, 10);
        #endregion

        for (int i = 0; i < dataManager.bingoList.Count; i++)
        {
            bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2(400, bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y + 110);

            bingoNameDisplayList.Add(Instantiate(bingoNameDisplay_Prefab, Vector3.zero, Quaternion.identity) as GameObject);
            bingoNameDisplayList[bingoNameDisplayList.Count - 1].transform.parent = bingoNameDisplay_Parent.transform;
            bingoNameDisplayList[bingoNameDisplayList.Count - 1].GetComponent<BingoNamePrefab>().bingoName.text = dataManager.bingoList[i].bingoName;
            bingoNameDisplayList[bingoNameDisplayList.Count - 1].name = dataManager.bingoList[i].bingoName;

            if (bingoNameDisplay_Parent.GetComponent<RectTransform>().sizeDelta.y <= 1030)
                BingoViewRect.GetComponent<ScrollRect>().vertical = false;
            else
                BingoViewRect.GetComponent<ScrollRect>().vertical = true;
        }
    }
}
