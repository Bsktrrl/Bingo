using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BingoViewPrefab : MonoBehaviour
{
    PrintListMenu printListMenu;

    public TextMeshProUGUI bingoName;
    public Button button;
    public bool isActive;


    //--------------------


    private void Awake()
    {
        printListMenu = FindObjectOfType<PrintListMenu>();
    }
    private void Update()
    {
        if (isActive)
        {
            button.GetComponent<Image>().color = new Color(0.69f, 0.86f, 0.93f, 1);
        }
        else
        {
            button.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }


    //--------------------


    public void ButtonPressed()
    {
        for (int i = 0; i < printListMenu.bingoDisplayList.Count; i++)
            printListMenu.bingoDisplayList[i].GetComponent<BingoViewPrefab>().isActive = false;

        isActive = true;
    }

    public void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
