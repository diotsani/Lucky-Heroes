using System;
using Enums;
using Pool;
using Scene;
using UnityEngine;

namespace Services
{
    public class Bootstrap : MonoBehaviour
    {
        private SceneLoader _sceneLoader;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _sceneLoader = new SceneLoader();
            ServiceLocator.Register(_sceneLoader);
        }

        private void Start()
        {
            _sceneLoader.Load(SceneID.MainMenu);
        }
    }
}