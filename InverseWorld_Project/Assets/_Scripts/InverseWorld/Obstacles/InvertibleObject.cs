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
        [SerializeField] private string PlayerLayerName = "Player";

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

            // switch to inverted gameObject
            if (isInverted)
            {
                invertedObj.SetActive(true);
                normalObj.SetActive(false);
                gameObject.layer = LayerMask.NameToLayer(invertedLayerName);
            }

            // if switching to nonInverted, wait until the collider is not overlapping the player
            else
            {
                if(overlapCoroutine == null)
                    overlapCoroutine = StartCoroutine(WaitOnOverlap());
            }
        }

        IEnumerator WaitOnOverlap()
        {
            Collider[] overlapped = Physics.OverlapBox(transform.position, transform.localScale / 2);

            bool overlappingPlayer = false;

            foreach (var col in overlapped)
            {
                if(col.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
                    overlappingPlayer = true;
            }

            
            while (overlappingPlayer)
            {
                overlappingPlayer = false;

                foreach (var col in overlapped)
                {
                    if (col.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
                        overlappingPlayer = true;
                }

                yield return null;
            }

            overlapCoroutine = null;
            gameObject.layer = LayerMask.NameToLayer(normalLayerName);
            normalObj.SetActive(true);
            invertedObj.SetActive(false);
        }
    }
}
