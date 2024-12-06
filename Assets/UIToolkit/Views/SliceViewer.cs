using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System;

namespace BiodesignLab.CustomVisualElements
{
    [UxmlElement]
    public partial class SliceViewer : VisualElement, IDisposable
    {
        private SlicingPlane slicingPlane;
        private VisualElement slice;
        private Slider slider;

        const string styleSheetPath = "Assets/UIToolkit/USS/SliceViewer.uss";

        public SliceViewer()
        {
            SetVisualElements();
            RegisterCallbacks();
        }

        public void SetSlice(SlicingPlane slicingPlane)
        {
            this.slicingPlane = slicingPlane;
            this.slicingPlane.SetPosition(this.slider.value);

            this.slice.Add(new FriedmanAxis());
            // Set the background of the slice element to the slicing plane's render texture
            this.slice.style.backgroundImage = new StyleBackground(Background.FromRenderTexture(slicingPlane.Texture));
        }

        public void Reset()
        {
            this.slider.value = 0.0f;
            this.slice.style.backgroundImage = null;
            this.slice.Clear();
            this.slicingPlane.Dispose();
        }

        private void SetVisualElements()
        {
            this.slice = new VisualElement();
            this.slider = new Slider();

            this.name = "SliceViewer";
            this.slice.name = "Slice";
            this.slider.name = "Slider";

            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheetPath));
            AddToClassList("sliceViewer");
            
            this.slice.AddToClassList("slice");
            this.slider.AddToClassList("slider");

            this.slider.lowValue = 0.0f;
            this.slider.highValue = 1.0f;

            Add(this.slice);
            Add(this.slider);
        }

        private void RegisterCallbacks()
        {
            this.slider.RegisterCallback<ChangeEvent<float>>(OnSliderValueChanged);
        }
        
        public void Dispose()
        {
            this.slider.UnregisterCallback<ChangeEvent<float>>(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(ChangeEvent<float> e)
        {
            if(this.slicingPlane is not null)
            {
                Debug.Log($"Slider value changed: {e.newValue}");

                // Calculate the current slice number
                int totalSlices = this.slicingPlane.TotalSlices;
                int currentSlice = Mathf.RoundToInt(e.newValue * (totalSlices - 1)) + 1;
                
                // Log the slice information
                Debug.Log($"Slice {currentSlice} of {totalSlices}");
                this.slicingPlane.SetPosition(e.newValue);
            }
        }
    }
}