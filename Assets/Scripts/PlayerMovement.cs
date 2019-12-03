using InventorySystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IItemEventHandler
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
            inputY = 0.0f; }
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

    public void ItemPickup(ItemGameObject item)
    {
        if (InventorySystem.Inventory.Instance.InsertItem(item.Item) != -1)
        {
            GameObject.Destroy(item.gameObject);    
        }
    }

    public void ItemEnter(ItemGameObject item)
    {
    }

    public void ItemExit(ItemGameObject item)
    {
    }
}
