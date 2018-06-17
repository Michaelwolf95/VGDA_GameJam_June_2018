using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Updates current checkpoint on trigger
/// 
/// Ruben Sanchez
/// 
/// </summary>

public class Checkpoint : MonoBehaviour
{
    [Tooltip("Check if player is allowed to travel to a previous level")]
    public bool IsLastCheckpointInLevel;

    public UnityEvent OnEnter;
    public AudioSource audioSource;
    public bool playsSound = true;

    private bool hasEntered;

    private void Start()
    {
        if (CheckpointManager.Instance.LoadedPreviousLevel)
        {
            CheckpointManager.Instance.LoadedPreviousLevel = false;
        }

        if(!audioSource)
            audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.CurrentCheckpoint = gameObject.transform.position;

            OnEnter.Invoke();


            if (IsLastCheckpointInLevel)
            {
                CheckpointManager.Instance.PreviousLevelCheckPoint = gameObject.transform.position;
            }

            if (!hasEntered && playsSound)
            {
                hasEntered = true;
                audioSource.Play();
            }
        }      
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            hasEntered = false;
            CheckpointManager.Instance.CurrentCheckpoint = gameObject.transform.position;
        }
    }
}
