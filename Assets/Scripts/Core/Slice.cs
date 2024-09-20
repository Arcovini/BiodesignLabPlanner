using UnityEngine;

namespace BiodesignLab
{
    public class Slice : MonoBehaviour
    {
        public Plane Plane
        {
            get { return this.plane; }
            set { this.plane = value; }
        }

        private Plane plane;
    }
}