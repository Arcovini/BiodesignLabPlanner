using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BiodesignLab
{
    public class NewPlanAdjustment : IDisposable
    {
        private VisualElement root;

        public NewPlanAdjustment(VisualElement root)
        {
            this.root = root;
        }

        private void SetVisualElements()
        {

        }

        private void RegisterCallbacks()
        {

        }

        public void Dispose()
        {
            
        }

        public void SetVisible(bool visible)
        {
            if(visible)
                this.root.style.display = DisplayStyle.Flex;
            else
                this.root.style.display = DisplayStyle.None;
        }
    }
}