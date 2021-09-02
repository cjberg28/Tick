using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddableObject : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        gameManager.itemToBePickedUp = gameObject;//This sets the scene item as the item to be picked up. We want the inventory version! FIX-------------------------------------------------------------------
    }
}
