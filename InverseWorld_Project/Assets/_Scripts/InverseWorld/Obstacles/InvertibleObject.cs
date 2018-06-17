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

        private Collider mCollider;
        private Coroutine overlapCoroutine;

        private bool inverted;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                inverted = !inverted;
                DoInvert(inverted);
            }
        }

        private void Start()
        {
            normalObj.SetActive(true);
            invertedObj.SetActive(false);
        }

        protected override void DoInvert(bool isInverted)
        {
            inverted = isInverted;
  
            invertedObj.SetActive(isInverted);
            normalObj.SetActive(!isInverted);
            gameObject.layer = isInverted ? LayerMask.NameToLayer(invertedLayerName) : LayerMask.NameToLayer(normalLayerName);         
        }
    }
}
