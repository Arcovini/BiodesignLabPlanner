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
            this.slice.style.backgroundImage = new StyleBackground(Background.FromRenderTexture(slicingPlane.Texture));
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
                this.slicingPlane.SetPosition(e.newValue);
        }
    }
}