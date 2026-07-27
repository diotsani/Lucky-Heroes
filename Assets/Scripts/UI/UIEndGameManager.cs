using System;
using Core;
using Enums;
using Scene;
using Services;
using UnityEngine;

namespace UI
{
    public class UIEndGameManager : MonoBehaviour
    {
        [SerializeField] private UIEndGameView view;
        private GameManager _game;

        private void OnEnable()
        {
            view.OnReturnButtonClicked += OnReturn;
            view.OnRestartButtonClicked += OnRestart;
            view.OnContinueButtonClicked += OnContinue;
        }

        private void OnDisable()
        {
            view.OnReturnButtonClicked -= OnReturn;
            view.OnRestartButtonClicked -= OnRestart;
            view.OnContinueButtonClicked -= OnContinue;
        }

        private void Start()
        {
            _game = ServiceLocator.Get<GameManager>();
            _game.EndGame += Open;
        }

        private void OnReturn()
        {
            ServiceLocator.Get<SceneLoader>().Load(SceneID.MainMenu);
        }

        private void OnRestart()
        {
            ServiceLocator.Get<SceneLoader>().Reload();
        }

        private void OnContinue()
        {
            
        }

        public void Open(bool win)
        {
            view.Setup(win ? "Win" : "Lose");
        }
    }
}