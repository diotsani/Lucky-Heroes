using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIEndGameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text endGameText;
        [SerializeField] private Button returnButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button continueButton;
        
        public Action OnReturnButtonClicked;
        public Action OnRestartButtonClicked;
        public Action OnContinueButtonClicked;

        private void OnEnable()
        {
            returnButton.onClick.AddListener(() => OnReturnButtonClicked?.Invoke());
            restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
            continueButton.onClick.AddListener(() => OnContinueButtonClicked?.Invoke());
        }

        private void OnDisable()
        {
            returnButton.onClick.RemoveAllListeners();
            restartButton.onClick.RemoveAllListeners();
            continueButton.onClick.RemoveAllListeners();
        }

        public void Setup(string text)
        {
            gameObject.SetActive(true);
            endGameText.text = text;
        }
        
        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}