using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

namespace BiodesignLab
{
    public enum Scene
    {
        MainMenu,
        NewPlan,
        Planner
    };

    public class SequenceManager : MonoBehaviour
    {
        private Stack<Scene> scenes = new Stack<Scene>();

        private void OnEnable()
        {
            SceneEvents.LoadScene += LoadScene;
            SceneEvents.UnloadScene += UnloadScene;
            SceneEvents.QuitApplication += QuitApplication;
        }

        private void OnDisable()
        {
            SceneEvents.LoadScene -= LoadScene;
            SceneEvents.UnloadScene -= UnloadScene;
            SceneEvents.QuitApplication -= QuitApplication;
        }

        private void Start()
        {
            LoadScene(Scene.MainMenu);
        }

        private void LoadScene(Scene scene)
        {
            this.scenes.Push(scene);
            SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);
        }

        private void UnloadScene()
        {
            if(this.scenes.Count == 0)
                return;

            Scene scene = this.scenes.Peek();
            SceneManager.UnloadSceneAsync(scene.ToString());
            this.scenes.Pop();
        }

        // TODO: Shutdown gracefully before quitting (save docs, settings, etc)
        private void QuitApplication()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}