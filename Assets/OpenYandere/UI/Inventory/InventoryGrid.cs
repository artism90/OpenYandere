using System.Collections;
using OpenYandere.Characters.Player;
using OpenYandere.Items;
using UnityEngine;
using OpenYandere.Managers;

namespace OpenYandere.UI.Inventory
{
    internal class InventoryGrid : MonoBehaviour
    {
        private GameManager _gameManager;
        private CameraManager _cameraManager;

        private PlayerInventory _playerInventory;
        private Slot[] _slotsArray;
        
        [Header("Settings:")]
        [Tooltip("The key to be pressed to make the it appear/disappear.")]
        [SerializeField] private KeyCode _keyCode = KeyCode.I;
		
        [Header("References:")]
        [SerializeField] private Animator _animator;
        [Tooltip("The parent of the slots.")]
        [SerializeField] private GameObject _slotsParent;
        
        [Header("Animations:")]
        [Tooltip("The animation to played when showing the inventory.")]
        [SerializeField] private AnimationClip _slideInClip;
        [Tooltip("The animation to played when hiding the inventory.")]
        [SerializeField] private AnimationClip _slideOutClip;

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