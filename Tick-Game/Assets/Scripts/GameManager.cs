using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Variable Declarations
    private TextMeshProUGUI gameText;
    private string[] textList = { "Hello World!", "This is the first game I'm making over Summer 2021. I hope you enjoy!", "Line 3333333333333333333333333333333333333333333", "Line 4444444444444444444444444444", "Line 55555555555555555555555555555555", "Line 66666666666666666666666666666666", "LAST LINEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE" };
    //private string nextText = "This is the first game I'm making over Summer 2021. I hope you enjoy!";
    private bool isWritingText;
    private IEnumerator coroutineInstance = null;
    private int currentTextIndex = 0;//This is the index for the textList, i.e., which line of text we're on.
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gameText = GameObject.Find("Game Text").GetComponent<TextMeshProUGUI>();
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

    #region Text Writing Functions
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
        currentTextIndex++;
        coroutineInstance = WriteText(textList[currentTextIndex]);
        StartCoroutine(coroutineInstance);
    }

    void StopWriting()
    {
        StopCoroutine(coroutineInstance);
        isWritingText = false;
    }
    #endregion
}
