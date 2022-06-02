using System;
using UnityEngine;

namespace _Game.Scripts{
    public class PauseMenu : MonoBehaviour{
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private CanvasGroup[] others;
        
        private bool paused;
        
        public event Action<bool> OnPause;

        public static PauseMenu Instance{ get; private set; }

        private void Awake(){
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
            pausePanel.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;
            foreach (CanvasGroup group in others){
                group.alpha = paused ? 0 : 1;
            }

            Cursor.visible = paused;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}