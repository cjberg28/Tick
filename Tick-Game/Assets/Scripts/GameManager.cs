using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variable Declarations
    private TextMeshProUGUI gameText;
    private GameObject gameOverText;
    private string[] textList;
    //private string nextText = "This is the first game I'm making over Summer 2021. I hope you enjoy!";
    private bool isWritingText;
    public bool isReplacing;
    private IEnumerator coroutineInstance = null;
    private int currentTextIndex = 0;//This is the index for the textList, i.e., which line of text we're on.
    public GameObject itemSelected = null;//This variable is used for switching inventory items around.
    public GameObject itemToBePickedUp = null;//This variable is used for adding new items to the inventory.
    private GameObject inventoryOnhand;
    private GameObject inventory2;
    private GameObject inventory3;
    public Button yesButton;
    public Button noButton;
    public Button onhandButton;
    public Button slot1Button;
    public Button slot2Button;
    #endregion

    #region Text Dictionary
    private Dictionary<string, string[]> textDict = new Dictionary<string, string[]>
    {
        { "test", new[] { "Hello World!", "This is the first game I'm making over Summer 2021. I hope you enjoy!", "Line 3333333333333333333333333333333333333333333", "Line 4444444444444444444444444444", "Line 55555555555555555555555555555555", "Line 66666666666666666666666666666666", "LAST LINEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE" } },
        {"test1", new[] {"This is the start of Test 1.", "ur bad"} },
        {"test2", new[] {"This is the start of Test 2!!", "but are you really bad if you can get this to work??? hehe"} }



    };
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        inventoryOnhand = GameObject.Find("Item 1 (Onhand)");
        inventory2 = GameObject.Find("Item 2");
        inventory3 = GameObject.Find("Item 3");
        gameText = GameObject.Find("Game Text").GetComponent<TextMeshProUGUI>();
        gameOverText = GameObject.Find("Game Over Text");
        textList = textDict["test"];
        coroutineInstance = WriteText(textList[currentTextIndex]);
        StartCoroutine(coroutineInstance);//Writes the first line immediately.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//And is in text mode???
        {
            if (isWritingText)
            {
                StopWriting();
                gameText.text = textList[currentTextIndex];
            }
            else
            {
                NextLine();
            }//If text is currently being written onto the screen, skip to the end of the line. Otherwise, start the next line.
        }
    }

    #region Text Writing Functions and Branch if Statements
    IEnumerator WriteText(string text)
    {
        isWritingText = true;
        gameText.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(.05f);
            gameText.text += text[i];
        }
        isWritingText = false;
    }//This should add a typewriter effect to any text appearing on screen.

    void NextLine()
    {
        currentTextIndex++;//As of right now, this makes the index increase on each click, but it throws no error. Reset every time you change text lists!
        if (currentTextIndex < textList.Length)//If the last line read is not the final line in the "paragraph", or text list...
        {
            coroutineInstance = WriteText(textList[currentTextIndex]);
            StartCoroutine(coroutineInstance);
        }
        else//This is where you throw all of the text list switches, depending on what route you've taken.
        {
            StopCoroutine(coroutineInstance);//Stops the previous Coroutine from breaking everything.
            if (textList == textDict["test"])
            {
                ChangeText("test1");
            }
            else if (textList == textDict["test1"])
            {
                ChangeText("test2");
            }
            else if (textList == textDict["test2"])
            {
                ChangeText("test");
            }
        }
    }

    void StopWriting()
    {
        StopCoroutine(coroutineInstance);
        isWritingText = false;
    }

    void ChangeText(string dictKey)
    {
        textList = textDict[dictKey];
        currentTextIndex = 0;
        coroutineInstance = WriteText(textList[currentTextIndex]);
        StartCoroutine(coroutineInstance);
    }
    #endregion

    public void AddToInventory(GameObject item)//Note that this item will be the inventory version of the Scene Item, which will be properly attached in each Scene Item's OnMouseDown script.
    {
        if (inventoryOnhand.transform.childCount == 0)//If the Onhand slot is empty...
        {
            item.SetActive(true);//Make the item visible.
            item.transform.SetParent(inventoryOnhand.transform, false);
            itemToBePickedUp = null;//The item has been picked up.
        }
        else if (inventory2.transform.childCount == 0)//Otherwise, if item slot 2 is empty...
        {
            item.SetActive(true);//Make the item visible.
            item.transform.SetParent(inventory2.transform, false);
            itemToBePickedUp = null;//The item has been picked up.
        }
        else if (inventory3.transform.childCount == 0)//Otherwise, if item slot 3 is empty...
        {
            item.SetActive(true);//Make the item visible.
            item.transform.SetParent(inventory3.transform, false);
            itemToBePickedUp = null;//The item has been picked up.
        }
        else//Otherwise, all inventory slots are taken...
        {
            //Would you like to replace an item? Text
            isReplacing = true;
            yesButton.gameObject.SetActive(true);
            noButton.gameObject.SetActive(true);//Yes/No option.
        }
    }

    public void PutItemBack()
    {
        if (isReplacing)//For the case when the player is replacing an item and clicks "No".
        {
            isReplacing = false;
            noButton.gameObject.SetActive(false);
            yesButton.gameObject.SetActive(false);
        }
        itemToBePickedUp = null;
    }

    public void ReplaceItem(int slot)//When this is called, isReplacing will be true, and the inventory buttons will be active.
    {
        onhandButton.gameObject.SetActive(false);
        slot1Button.gameObject.SetActive(false);
        slot2Button.gameObject.SetActive(false);
        isReplacing = false;

        if (slot == 0)
        {
            ClearItems(0);
            AddToInventory(itemToBePickedUp);//This works because the only empty inventory slot (at this very moment) is the slot the item is going to, replacing the previous item.
        }
        else if (slot == 1)
        {
            ClearItems(1);
            AddToInventory(itemToBePickedUp);
        }
        else if (slot == 2)
        {
            ClearItems(2);
            AddToInventory(itemToBePickedUp);
        }
    }

    void ClearItems()
    {
        if (inventoryOnhand.transform.childCount > 0)
        {
            foreach (Transform child in inventoryOnhand.transform)
            {
                child.parent = null;//Detaches the child from the inventory slot, and any other children (there shouldn't be any others).
                child.gameObject.SetActive(false);
            }
        }
        if (inventory2.transform.childCount > 0)
        {
            foreach (Transform child in inventory2.transform)
            {
                child.parent = null;//Detaches the child from the inventory slot, and any other children (there shouldn't be any others).
                child.gameObject.SetActive(false);
            }
        }
        if (inventory3.transform.childCount > 0)
        {
            foreach (Transform child in inventory3.transform)
            {
                child.parent = null;//Detaches the child from the inventory slot, and any other children (there shouldn't be any others).
                child.gameObject.SetActive(false);
            }
        }
    }

    void ClearItems(int slot)
    {
        if (slot == 0)
        {
            if (inventoryOnhand.transform.childCount > 0)
            {
                foreach (Transform child in inventoryOnhand.transform)
                {
                    child.parent = null;//Detaches the child from the inventory slot, and any other children (there shouldn't be any others).
                    child.gameObject.SetActive(false);
                }
            }
        }
        else if (slot == 1)
        {
            if (inventory2.transform.childCount > 0)
            {
                foreach (Transform child in inventory2.transform)
                {
                    child.parent = null;//Detaches the child from the inventory slot, and any other children (there shouldn't be any others).
                    child.gameObject.SetActive(false);
                }
            }
        }
        else if (slot == 2)
        {
            if (inventory3.transform.childCount > 0)
            {
                foreach (Transform child in inventory3.transform)
                {
                    child.parent = null;//Detaches the child from the inventory slot, and any other children (there shouldn't be any others).
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    void GameOver(string type)
    {
        if (type == "Captured")
        {
            //Set the jail cell image.
        }
        else if (type == "Killed")
        {
            //Set the tombstone/skull image.
        }
        else if (type == "Escaped")
        {
            //Set the run away image.
        }

        gameOverText.SetActive(true);
        //Restart and Quit buttons?
    }

}
