using Features.MainMenu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Features.MainMenu
{
    public class LoadScenesButton : MonoBehaviour
    {
        private IGameManager _GameManager;
        [SerializeField] private string _LoadScene;

        [Inject] 
        private void Construct(IGameManager gameManager)
        {
            _GameManager = gameManager;
        }

        public void LoadScene()
        {
            _GameManager.StartButton(_LoadScene);
        }

       
    }

}
