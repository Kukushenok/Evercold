using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Features.MainMenu
{
    interface IGameManager
    {
        void StartButton(string LoadScene);

        void ExitButton();
    }
}
