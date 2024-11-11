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

        public Vector3 Normal
        {
            get { return this.normal; }
            set { this.normal = value; }
        }

        public Matrix4x4 LocalToWorldMatrix
        {
            get 
            { 
                Matrix4x4 translation = Matrix4x4.Translate(Position);
                Matrix4x4 rotation = Matrix4x4.Rotate(Quaternion.LookRotation(Normal));
                Matrix4x4 scaling = Matrix4x4.Scale(new Vector3(Width, Height, 1.0f));

                return translation * rotation * scaling;
            }

            private set {}
        }

        public Matrix4x4 WorldToLocalMatrix
        {
            get { return Matrix4x4.Inverse(LocalToWorldMatrix); }
            private set {}
        }

        private float width;
        private float height;
        private Vector3 position;
        private Vector3 normal;

        public Plane(float width, float height, Vector3 position, Vector3 normal)
        {
            this.width = width;
            this.height = height;
            this.position = position;
            this.normal = normal;
        }
    }
}