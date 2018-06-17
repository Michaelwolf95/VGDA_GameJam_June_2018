using UnityEngine;
using System.Collections;

/// <summary>
/// Singleton to keep a reference of the current level checkpoint as well as the previous level
/// 
/// Ruben Sanchez
/// 4/4/18
/// </summary>
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;

    public Vector3 CurrentCheckpoint;
    public Vector3 PreviousLevelCheckPoint;
    public bool LoadedPreviousLevel;

    private GameObject _player;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
