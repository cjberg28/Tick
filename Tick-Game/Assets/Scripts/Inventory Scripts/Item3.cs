using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item3 : MonoBehaviour
{
    private GameManager gameManager;
    private OnhandItem onhandItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (gameManager.itemSelected != null && transform.childCount > 0)//If the Onhand item is selected AND Item Slot 3 has an item...
        {
            transform.GetChild(0).SetParent(onhandItem.transform, false);//Set Item 3 as the Onhand Item.
            gameManager.itemSelected.transform.SetParent(transform, false);//Set the Onhand Item as Item 3.
            gameManager.itemSelected = null;//Deselect the original Onhand Item.
            onhandItem.inventoryColor.material.color = new Color(1.0f, 0.38f, 0f, 0.29f);//Return the Onhand color to orange.
        }
    }
}
