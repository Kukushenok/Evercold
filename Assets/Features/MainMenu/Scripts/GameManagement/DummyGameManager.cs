using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Feature.MainMenu
{
    public class DummyGameManager : IGameManager
    {
        public void ExitButton()
        {
            Application.Quit();
        }

        public void StartButton(string LoadScene)
        {
            Debug.Log("New Scene!");
        }
    }
}
