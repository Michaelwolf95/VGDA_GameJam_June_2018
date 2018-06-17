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

    [FMODUnity.EventRef]
    public string DeathAudioEvent = "event:/EVENT";
    [FMODUnity.EventRef]
    public string RespawnAudioEvent = "event:/EVENT";

    public Animator anim;
    public ParticleSystem deathParticles;

    protected override void Awake()
    {
        base.Awake();

        rigidB = GetComponent<Rigidbody>();
        characterMovement = GetComponent<CharacterMovement2D>();
        if (!anim) anim = GetComponent<Animator>();
    }

    public void DelayRevive()
    {
        if(delayCoroutine == null)
            delayCoroutine = StartCoroutine(WaitForRevive());
    }

    IEnumerator WaitForRevive()
    {
        characterMovement.enabled = false;
        rigidB.velocity = Vector3.zero;
        rigidB.isKinematic = true;

        yield return new WaitForSeconds(spawnDelay);
        HealthManager.Revive();
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
        var deathEvent = FMODUnity.RuntimeManager.CreateInstance(DeathAudioEvent);
        deathEvent.start();
        if (anim)
        {
            anim.Play("Die");
        }
        if (deathParticles)
        {
            deathParticles.Play(true);
        }
        PlayMakerFSM.BroadcastEvent("FADE_OUT");
        DelayRevive();
    }

    protected override void DoOnRevive()
    {
        if (anim)
        {
            anim.Play("Entry");
        }
        if (deathParticles)
        {
            deathParticles.Stop(true);
        }
        PlayMakerFSM.BroadcastEvent("FADE_IN");
        //transform.position = CheckpointManager.Instance.CurrentCheckpoint;
    }
}
