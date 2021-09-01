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
        gameManager.AddToInventory(gameManager.itemToBePickedUp);
    }

}
