using System.Collections;
using UnityEngine;

/// <summary>
/// Activates/Deactivates objects based on the game inversion state
/// 
/// Ruben Sanchez
/// 6/16/18
/// </summary>
/// 

namespace VGDA.InverseWorld
{
    public class InvertibleObject : InvertSubscriber
    {
        [SerializeField] private GameObject normalObj;
        [SerializeField] private GameObject invertedObj;

        [SerializeField] private string normalLayerName = "Default";
        [SerializeField] private string invertedLayerName = "Inverted";

        public bool invertedIsNormal;

        private Collider mCollider;
        private Coroutine overlapCoroutine;

        protected override void Start()
        {
            base.Start();
            normalObj.SetActive(true);
            invertedObj.SetActive(false);
        }

        protected override void DoInvert(bool isInverted)
        {
            invertedObj.SetActive(isInverted);
            normalObj.SetActive(!isInverted);
            bool invert = isInverted;
            if (invertedIsNormal)
            {
                invert = !invert;
            }

            var value = invert ? LayerMask.NameToLayer(invertedLayerName) : LayerMask.NameToLayer(normalLayerName);
            //Debug.Log(isInverted + " , " + value + ", " + gameObject.name);
            if (value >= 0)
            {
                gameObject.layer = value;
            }

        }
    }
}
