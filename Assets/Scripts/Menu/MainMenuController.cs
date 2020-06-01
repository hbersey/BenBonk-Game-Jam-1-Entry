using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menu;
        [SerializeField] private GameObject credits;

        private void Start()
        {
            ShowMenu();
        }

        public void Play()
        {
            SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        }

        public void ShowCredits()
        {
            menu.SetActive(false);
            credits.SetActive(true);
        }
        public void Exit()
        {
            Application.Quit();
        }

        public void ShowMenu()
        {
            credits.SetActive(false);
            menu.SetActive(true);
        }
    }
}