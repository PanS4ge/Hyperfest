using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hyperfest.Mechanics
{
    public class MoveScene : MonoBehaviour
    {
        public void NextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void BackScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void AddScene(int add)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + add);
        }

        public void SetScene(int scene)
        {
            Debug.Log("Calling " + scene.ToString() + " scene");
            SceneManager.LoadScene(scene);
        }

        public void doExitGame() {
            Application.Quit();
        }
    }
}