using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickYes : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void onClick()
    {
        if (!gameManager.isReplacing)//This tells the Yes button to either replace the item or add the item to inventory.
        {
            gameManager.AddToInventory(gameManager.itemToBePickedUp);
        }
        else if (gameManager.isReplacing)
        {
            //"Which item do you want to replace?"
            gameManager.noButton.gameObject.SetActive(false);
            gameManager.onhandButton.gameObject.SetActive(true);
            gameManager.slot1Button.gameObject.SetActive(true);
            gameManager.slot2Button.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
