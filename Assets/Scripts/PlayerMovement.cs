using System;
using System.Collections;
using System.Collections.Generic;
using ESDM.Items;
using Inventory;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IItemPickupHandler
{
    public bool paused = false;
    [SerializeField] private float moveSpeed = 2.0f;

    private Rigidbody2D rb;
    private float inputX, inputY;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("MoveY", -1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
        }
        else
        {
            inputX = 0.0f;
            inputY = 0.0f;
        }
        

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX, inputY).normalized * moveSpeed;

        if(inputX != 0 || inputY != 0)
        {
            animator.SetFloat("MoveX", inputX);
            animator.SetFloat("MoveY", inputY);
        }
        
        animator.SetBool("Walking", (inputX != 0 || inputY != 0));
    }

    public void ItemPickup(GameObject item, InventoryItemTypes itemType)
    {
        GameObject.Destroy(item);
        Inventory.Inventory inventory = GameObject.FindObjectOfType<Inventory.Inventory>();
        inventory.ItemAdd(itemType);
        Debug.Log("ITEM PICKED UP");
    }

    public void ItemEnter(GameObject item, InventoryItemTypes inventoryItemType)
    {
    }

    public void ItemExit(GameObject item, InventoryItemTypes inventoryItemType)
    {
    }
}
