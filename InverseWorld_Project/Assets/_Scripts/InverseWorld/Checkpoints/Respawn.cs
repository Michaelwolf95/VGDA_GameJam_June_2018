using System.Collections;
using Cinemachine.Examples;
using MichaelWolfGames.DamageSystem;
using UnityEngine;

/// <summary>
/// Respawns the player after death
/// 
/// Ruben Sanchez
/// 
/// </summary>

public class Respawn : HealthManagerEventListenerBase
{
    [Tooltip("Delay between death event and respawning")]
    [SerializeField] private float spawnDelay;

    private Coroutine delayCoroutine;
    private Rigidbody rigidB;
    private CharacterMovement2D characterMovement;



    protected override void Awake()
    {
        base.Awake();

        rigidB = GetComponent<Rigidbody>();
        characterMovement = GetComponent<CharacterMovement2D>();
    }

    public void RespawnToCheckpoint()
    {
        if(delayCoroutine == null)
            delayCoroutine = StartCoroutine(WaitForRespawn());
    }

    IEnumerator WaitForRespawn()
    {
        characterMovement.enabled = false;
        rigidB.velocity = Vector3.zero;
        rigidB.isKinematic = true;

        yield return new WaitForSeconds(spawnDelay);

        rigidB.isKinematic = false;
        characterMovement.enabled = true;
        transform.position = CheckpointManager.Instance.CurrentCheckpoint;
        delayCoroutine = null;
    }

    protected override void DoOnTakeDamage(object sender, Damage.DamageEventArgs damageEventArgs)
    {
    }

    protected override void DoOnDeath()
    {
        HealthManager.Revive();
    }

    protected override void DoOnRevive()
    {
        RespawnToCheckpoint();

    }
}
