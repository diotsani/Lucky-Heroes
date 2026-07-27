using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIMainMenuView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        
        public Action OnPlayButtonClicked;

        private void OnEnable()
        {
            playButton.onClick.AddListener(() => OnPlayButtonClicked?.Invoke());
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
        }
    }
}