using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnhandItem : MonoBehaviour
{
    private GameManager gameManager;
    public Renderer inventoryColor;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        inventoryColor = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (transform.childCount > 0)//If an item is in the Onhand slot...
        {
            if (gameManager.itemSelected == null)//and the item is not already selected...
            {
                inventoryColor.material.color = new Color(1.0f, 1.0f, 0f, 0.29f);//Highlight the Onhand slot in yellow to show the item has been selected.
                gameManager.itemSelected = transform.GetChild(0).gameObject;//Get the actual Onhand Item!!
            }
            else if (gameManager.itemSelected != null)//and the item IS selected...
            {
                inventoryColor.material.color = new Color(1.0f, 0.38f, 0f, 0.29f);//Return to the original Onhand color to show the item has been deselected.
                gameManager.itemSelected = null;//Deselect the item.
            }
        }
    }

}
