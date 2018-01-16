using UnityEngine;
using OpenYandere.Items;
using OpenYandere.Managers;

namespace OpenYandere.Interactions.Prefabs.Items
{
    internal class PickupItem : Interactable
    {
        private string _droppedId;
        [SerializeField] private Item _item;

        protected override void Awake()
        {
            base.Awake();
            
            PromptText = _item.Name;
            PromptKeyCode = KeyCode.E;
            OffsetFromObject = new Vector3(0, 0.25f, 0);
        }

        public void Initialise(string droppedId, Item item, Renderer itemRenderer)
        {
            _droppedId = droppedId;
            _item = item;
            Renderer = itemRenderer;
        }
        
        protected override void OnPromptTriggered()
        {
            GameManager gameManager = GameManager.Instance;
            
            if (gameManager.PlayerManager.PlayerInventory.Add(_item))
            {
                HideRadialPrompt();

                // The dropped ID will be equal to null, if the item is "hard-coded£ into the scene.
                // Thus we cannot remove it as a "dropped item" due to it being hard-coded and not
                // dropped by the player.

                if (_droppedId != null)
                {
                    // TODO: Remove the dropped item from an item manager or something.
                }

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Your inventory is ful1!");
                // TODO: "Your inventory is full!"
            }
        }
    }
}