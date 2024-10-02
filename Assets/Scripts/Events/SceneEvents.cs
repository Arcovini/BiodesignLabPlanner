using UnityEngine;
using System;

namespace BiodesignLab
{
    public static class SceneEvents
    {
        public static Action<Scene> LoadScene;
        public static Action UnloadScene;

        public static Action QuitApplication;
    }
}