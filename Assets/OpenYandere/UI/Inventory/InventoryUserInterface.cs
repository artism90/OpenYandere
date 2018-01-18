using System.Collections;
using OpenYandere.Characters.Player;
using OpenYandere.Items;
using UnityEngine;
using OpenYandere.Managers;
using TMPro;

namespace OpenYandere.UI.Inventory
{
    internal class InventoryUserInterface : MonoBehaviour
    {
        private GameManager _gameManager;
        private CameraManager _cameraManager;

        private PlayerInventory _playerInventory;
        private Slot[] _slotsArray;
        
        [Header("Settings:")]
        [Tooltip("The key to be pressed to make the it appear/disappear.")]
        [SerializeField] private KeyCode _keyCode = KeyCode.I;
		
        [Header("UI References:")]
        [SerializeField] private Animator _animator;
        [Tooltip("The parent of the slots.")]
        [SerializeField] private GameObject _slotsParent;
        
        [Header("Details References:")]
        [SerializeField] private TextMeshProUGUI _itemNameBackground;
        [SerializeField] private TextMeshProUGUI _itemNameForeground;
        [SerializeField] private TextMeshProUGUI _itemDescriptionBackground;
        [SerializeField] private TextMeshProUGUI _itemDescriptionForeground;
        
        [Header("Animations:")]
        [Tooltip("The animation to played when showing the inventory.")]
        [SerializeField] private AnimationClip _slideInClip;
        [Tooltip("The animation to played when hiding the inventory.")]
        [SerializeField] private AnimationClip _slideOutClip;
        
        // TODO: Learn how to write a custom editor, so all the classes taking in references can be collapsed in the Editor.
        
        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _cameraManager = _gameManager.CameraManager;

            _playerInventory = _gameManager.PlayerManager.PlayerInventory;
            _playerInventory.OnInventoryChanged += HandleInventoryChanged;
            
            _slotsArray = _slotsParent.GetComponentsInChildren<Slot>();
        }
        
        private void Update()
        {
            // Ignore input unless it's the key code.
            if (!Input.GetKeyDown(_keyCode)) return;

            if (_gameManager.State == GameState.Normal)
            {
                StartCoroutine(ShowGrid());
            }
            else if (_gameManager.State == GameState.Inventory)
            {
                StartCoroutine(HideGrid());
            }
        }

        public void SetItemDetails(Item item)
        {
            if (item == null)
            {
                _itemNameBackground.text = string.Empty;
                _itemNameForeground.text = string.Empty;
                _itemDescriptionBackground.text = string.Empty;
                _itemDescriptionForeground.text = string.Empty;
                return;
            }

            _itemNameBackground.text = item.Name;
            _itemNameForeground.text = item.Name;
            _itemDescriptionBackground.text = item.Description;
            _itemDescriptionForeground.text = item.Description;
        }

        private void HandleInventoryChanged()
        {
            Debug.Log("Updating the inventory UI.");

            Item[] playerItems = _playerInventory.GetItems();
            
            for (int i = 0; i < _slotsArray.Length; i++)
            {
                Slot inventorySlot = _slotsArray[i];

                if (i < playerItems.Length)
                {
                    inventorySlot.Set(i, playerItems[i]);
                }
                else
                {
                    inventorySlot.Empty();
                }
            }
        }

        private IEnumerator ShowGrid()
        {
            _animator.SetBool("Visible", true);
            
            // Enter this state before the wait to prevent any possible attempts to enter multiple states.
            _gameManager.State = GameState.Inventory;
            
            // Stop the camera from moving.
            _cameraManager.Pause();
            
            // TODO: Stop the player from moving.
            
            // Wait for the animation to finish playing.
            yield return new WaitForSeconds(_slideInClip.length);
            
            Cursor.lockState = CursorLockMode.None;
        }
        
        private IEnumerator HideGrid()
        {
            _animator.SetBool("Visible", false);
			
            // Wait for the animation to finish playing.
            yield return new WaitForSeconds(_slideOutClip.length);
            
            // Allow the camera to move again.
            _cameraManager.Resume();
            
            // TODO: Allow the player to move.
            
            _gameManager.State = GameState.Normal;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}