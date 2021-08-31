using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
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
        if (gameManager.itemSelected != null && transform.childCount > 0)//If the Onhand item is selected AND Item Slot 2 has an item...
        {
            transform.GetChild(0).SetParent(onhandItem.transform, false);//Set Item 2 as the Onhand Item.
            gameManager.itemSelected.transform.SetParent(transform, false);//Set the Onhand Item as Item 2.
            gameManager.itemSelected = null;//Deselect the original Onhand item.
            onhandItem.inventoryColor.material.color = new Color(1.0f, 0.38f, 0f, 0.29f);//Return the Onhand color to orange.
        }
    }

}
