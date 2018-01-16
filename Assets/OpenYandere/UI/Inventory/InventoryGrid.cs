using System.Collections;
using UnityEngine;
using OpenYandere.Managers;

namespace OpenYandere.UI.Inventory
{
    internal class InventoryGrid : MonoBehaviour
    {
        private GameManager _gameManager;
        private CameraManager _cameraManager;
        
        [Header("Settings:")]
        [Tooltip("The key to be pressed to make the it appear/disappear.")]
        [SerializeField] private KeyCode _keyCode = KeyCode.I;
		
        [Header("References:")]
        [SerializeField] private Animator _animator;
        
        [Header("Animations:")]
        [Tooltip("The animation to played when showing the inventory.")]
        [SerializeField] private AnimationClip _slideInClip;
        [Tooltip("The animation to played when hiding the inventory.")]
        [SerializeField] private AnimationClip _slideOutClip;

        private void Awake()
        {
            _gameManager = GameManager.Instance;
            _cameraManager = _gameManager.CameraManager;
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

        private IEnumerator ShowGrid()
        {
            _animator.SetBool("Visible", true);
            
            // Enter this state before the wait to prevent any possible attempts to enter multiple states.
            _gameManager.State = GameState.Inventory;
            
            // Stop the camera from moving.
            _cameraManager.Pause();
            
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
            
            _gameManager.State = GameState.Normal;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}