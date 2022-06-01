using System;
using UnityEngine;

namespace _Game.Scripts{
    public class PauseMenu : MonoBehaviour{
        private Canvas pauseCanvas;
        private bool paused;
        
        public event Action<bool> OnPause;

        public static PauseMenu Instance{ get; private set; }

        private void Awake(){
            pauseCanvas = GetComponent<Canvas>();
            TogglePause(false);
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.Escape)){
                TogglePause(!paused);
            }
        }

        public void TogglePause(bool value){
            paused = value;
            OnPause?.Invoke(paused);
            pauseCanvas.enabled = value;
            Time.timeScale = value ? 0 : 1;
            if (paused){
                Cursor.lockState = CursorLockMode.None;
            }
            else{
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}