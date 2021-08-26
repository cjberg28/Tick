using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variable Declarations
    private TextMeshProUGUI gameText;
    private string[] textList;
    //private string nextText = "This is the first game I'm making over Summer 2021. I hope you enjoy!";
    private bool isWritingText;
    private IEnumerator coroutineInstance = null;
    private int currentTextIndex = 0;//This is the index for the textList, i.e., which line of text we're on.
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
        gameText = GameObject.Find("Game Text").GetComponent<TextMeshProUGUI>();
        textList = textDict["test"];
        coroutineInstance = WriteText(textList[currentTextIndex]);
        StartCoroutine(coroutineInstance);//Writes the first line immediately.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
}
