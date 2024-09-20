using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace BiodesignLab.CustomVisualElements
{
    [UxmlElement]
    public partial class SliceViewer : VisualElement
    {
        private VisualElement sliceView;
        private Slider slider;

        const string styleSheetPath = "Assets/UIToolkit/USS/Light/LightSliceViewer.uss";

        public SliceViewer()
        {
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(styleSheetPath));
            AddToClassList("sliceViewer");
            
            SetVisualElements();
        }

        private void SetVisualElements()
        {
            this.sliceView = new VisualElement();
            this.slider = new Slider();

            this.sliceView.name = "SliceView";
            this.slider.name = "Slider";

            this.sliceView.AddToClassList("sliceView");
            this.slider.AddToClassList("slider");

            this.slider.lowValue = 0.0f;
            this.slider.highValue = 100.0f;

            Add(this.sliceView);
            Add(this.slider);
        }
    }
}