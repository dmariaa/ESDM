using System;
using InventorySystem;
using UnityEngine;

namespace ESDM.TutorialSystem
{
    [CreateAssetMenu(menuName = "ESDM/TutorialSystem/Actions/PickupObject")]
    public class PickupObject : TutorialAction, IItemEventHandler
    {
        [NonSerialized] private bool itemPickedUp;
        
        public override void InitAction(TutorialController controller)
        {
            base.InitAction(controller);
            
            itemPickedUp = false;
            controller.Object.GetComponent<ItemGameObject>().RegisterListener(this);
        }

        public override void ExitAction()
        {
            base.ExitAction();
            controller.Object.GetComponent<ItemGameObject>().UnRegisterListener(this);
        }

        public override bool PlayAction()
        {
            return itemPickedUp;
        }

        public void ItemPickup(ItemGameObject item)
        {
            HidePanel("PickupObject");
            PauseCharacter(false);
            itemPickedUp = true;
        }

        public void ItemEnter(ItemGameObject item)
        {
            ShowPanel("PickupObject");
            PauseCharacter(true);
            itemPickedUp = false;
        }

        public void ItemExit(ItemGameObject item)
        {
        }
    }
}