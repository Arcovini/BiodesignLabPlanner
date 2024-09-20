using UnityEngine;

namespace BiodesignLab
{
    public class Plane
    {
        public float Width
        {
            get { return this.width; }
            set { this.width = value; }
        }

        public float Height
        {
            get { return this.height; }
            set { this.height = value; }
        }

        public Vector3 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Quaternion Rotation
        {
            get { return this.rotation; }
            set { this.rotation = value; }
        }

        private float width;
        private float height;
        private Vector3 position;
        private Quaternion rotation;

        public Plane(float width = 1.0f, float height = 1.0f, Vector3 position = new Vector3(), Vector3 rotation = new Vector3())
        {
            this.width = width;
            this.height = height;
            this.position = position;
            this.rotation = Quaternion.Euler(rotation);
        }
    }   
}