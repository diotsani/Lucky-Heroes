using System;
using Enums;
using Scene;
using Services;
using UI;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private UIMainMenuView view;

        private void OnEnable()
        {
            view.OnPlayButtonClicked += OnPlay;
        }

        private void OnDisable()
        {
            view.OnPlayButtonClicked -= OnPlay;
        }

        private void OnPlay()
        {
            ServiceLocator.Get<SceneLoader>().Load(SceneID.Gameplay);
        }
    }
}