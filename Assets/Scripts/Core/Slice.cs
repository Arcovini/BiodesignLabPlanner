using UnityEngine;
using UnityEditor;

namespace BiodesignLab
{
    public class Slice : MonoBehaviour
    {
        public float Width
        {
            get { return this.transform.localScale.x; }
            set { this.transform.localScale = new Vector3(value, this.transform.localScale.y, 1.0f); }
        }

        public float Height
        {
            get { return this.transform.localScale.y; }
            set { this.transform.localScale = new Vector3(this.transform.localScale.x, value, 1.0f); }
        }

        public Vector3 Position
        {
            get { return this.transform.position; }
            set { this.transform.position = value; }
        }

        public Quaternion Rotation
        {
            get { return this.transform.rotation; }
            set { this.transform.rotation = value; }
        }

        public void Init(DicomVolume volume, Vector3 rotation = new Vector3())
        {
            Width = volume.PhysicalWidth;
            Height = volume.PhysicalHeight;
            Position = volume.Position;
            Rotation = Quaternion.Euler(rotation);

            SetTexture(volume);
        }

        public void Init(float width = 1.0f, float height = 1.0f, Vector3 position = new Vector3(), Vector3 rotation = new Vector3())
        {
            Width = width;
            Height = height;
            Position = position;
            Rotation = Quaternion.Euler(rotation);
        }

        public void SetTexture(DicomVolume volume)
        {
            var material = GetComponent<Renderer>().material;

            material.SetTexture("_VolumeTex", volume.Texture);
            material.SetMatrix("_WorldToVolume", volume.Transform.worldToLocalMatrix);
        }
    }
}