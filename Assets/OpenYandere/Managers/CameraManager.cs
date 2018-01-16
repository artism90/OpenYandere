using UnityEngine;
using OpenYandere.Characters.Player;

namespace OpenYandere.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [Header("References:")]
        public PlayerCamera PlayerCamera;
        
        public void Resume()
        {
            PlayerCamera.enabled = true;
        }
        
        public void Pause()
        {
            PlayerCamera.enabled = false;
        }
    }
}