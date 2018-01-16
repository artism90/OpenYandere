using UnityEngine;
using OpenYandere.Managers.Traits;

namespace OpenYandere.Managers
{
    internal enum GameState
    {
        Normal,
        Paused,
        Cutscene,
        Dialogue,
        Inventory
    }
    
    internal class GameManager : Singleton<GameManager>
    {
        public GameState State { get; set; } = GameState.Normal;
        
        [Header("Managers:")]
        public UIManager UIManager;
        public CameraManager CameraManager;
        public PlayerManager PlayerManager;
        public ObjectPoolManager ObjectPoolManager;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Resume()
        {
            
        }

        public void Pause()
        {
            
        }
    }
}