using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        }

        public void Credits()
        {
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}