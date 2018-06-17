
using UnityEngine;

namespace VGDA.InverseWorld
{
    public class InvertMaterialSwap : InvertSubscriber
    {
        public Renderer renderer;
        public Material normalMaterial;
        public Material invertedMaterial;

        protected override void Start()
        {
            base.Start();
            if (!renderer)
            {
                renderer = GetComponent<Renderer>();
            }
            DoInvert(false);
        }

        protected override void DoInvert(bool isInverted)
        {
            if (isInverted)
            {
                renderer.material = invertedMaterial;
            }
            else
            {
                renderer.material = normalMaterial;
            }
        }
    }
}