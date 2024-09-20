using UnityEngine;

namespace BiodesignLab
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        public void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}