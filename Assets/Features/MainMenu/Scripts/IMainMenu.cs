using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Feature.MainMenu
{
    interface IMainMenu
    {
        void StartButton(Scene LoadScene);

        void ExitButton();
    }
}
