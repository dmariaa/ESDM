using System.Collections.Generic;
using ConversationSystem.Behaviours;
using ESDM.ScriptableObjects;
using ESDM.Utilities;
using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerSystems
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IItemEventHandler
    {
        public bool paused = false;
        [SerializeField] private float moveSpeed = 2.0f;

        private Rigidbody2D rb;
        private float inputX, inputY;
        private Animator animator;

        private bool think = false;
    
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
                
                List<IPlayerEventHandler> implementors = GameObjectFindHelper.FindGameObjectWithInterface<IPlayerEventHandler>();
                for (int i = 0, length = implementors.Count; i < length; i++)
                {
                    MonoBehaviour implementor = (MonoBehaviour) implementors[i];
                    ExecuteEvents.Execute<IPlayerEventHandler>(implementor.gameObject, null,
                        (handler, eventData) => { handler.PlayerMoved(this.transform.localPosition); } );
                }

            }
            else
            {
                inputX = 0.0f;
                inputY = 0.0f; 
            }
            
            if (!think && GlobalGameState.Instance.CurrentGameState.keyPicked 
                       && GlobalGameState.Instance.CurrentGameState.tutorialPlayed)
            {
                TextManager manager = GetComponent<TextManager>();
                manager.DisplayMessage("mmmm...", Color.white, 2.0f);
                manager.DisplayMessage("debería ir pensando como salir de aqui...", Color.white, 5.0f);
                manager.DisplayMessage("", Color.white, 10.0f);
                think = true;
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

        public void ItemPickup(ItemGameObject item)
        {
            if (GlobalGameState.Instance.CurrentGameState.inventory.InsertItem(item.Item) != -1)
            {
                GlobalGameState.Instance.CurrentGameState.keyPicked = true;
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
}
