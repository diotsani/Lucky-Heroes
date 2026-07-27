using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class SceneLoader
    {
        private SceneID _currentScene;
        public SceneID CurrentScene => _currentScene;
        
        public void Load(SceneID id)
        {
            _currentScene = id;
            SceneManager.LoadScene(id.ToString());
        }

        public void Reload()
        {
            Load(_currentScene);
        }
    }
}