using UnityEngine;
using UnityEngine.UIElements;

namespace BiodesignLab
{
    // TODO: temporary / delete
    public class PlannerPresenter : MonoBehaviour
    {
        private VisualElement root;
        private PlannerView view;

        private void Start()
        {
            this.root = GetComponent<UIDocument>().rootVisualElement;
            this.view = new PlannerView(root);
        }
    }
}