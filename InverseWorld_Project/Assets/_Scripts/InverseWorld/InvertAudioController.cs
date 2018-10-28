using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace VGDA.InverseWorld
{
    public class InvertAudioController : InvertSubscriber
    {
        [FMODUnity.EventRef]
        public string MusicEventName = "event:/EVENT";
        public string invertParameterName = "";

        //public StudioEventEmitter musicEventEmitter;

        public FMOD.Studio.EventInstance musicEvent;
        public FMOD.Studio.EventInstance invertEvent;
        public FMOD.Studio.EventInstance invertFailedEvent;

        [FMODUnity.EventRef]
        public string InvertEventName = "event:/EVENT";

        [FMODUnity.EventRef]
        public string InvertFailedEventName = "event:/EVENT";

        private const string DUCK_PARAM_NAME = "duck_music";

        protected override void Start()
        {
            base.Start();

            musicEvent = FMODUnity.RuntimeManager.CreateInstance(MusicEventName);
            musicEvent.start();

            //musicEvent.setParameterValue("duck_music", 1f);

            //invertParameter.setValue(1f);
            invertEvent = FMODUnity.RuntimeManager.CreateInstance(InvertEventName);
            invertFailedEvent = FMODUnity.RuntimeManager.CreateInstance(InvertFailedEventName);
        }

        protected override void DoInvert(bool isInverted)
        {
            if (isInverted)
            {
                musicEvent.setParameterValue(invertParameterName, 1f);
            }
            else
            {
                musicEvent.setParameterValue(invertParameterName, 0f);
                //invertParameter.setValue(0f);
            }
            if (invertEvent.isValid())
            {
                invertEvent.start();
            }
        }

        protected override void DoInvertFailed()
        {
            Debug.Log("Failed to Invert!");
            if (invertFailedEvent.isValid())
            {
                invertFailedEvent.start();
            }
        }

        public void SetDuck(float value)
        {
            musicEvent.setParameterValue(DUCK_PARAM_NAME, Mathf.Clamp(value, 0f, 1f));
        }

        public void SetDuckOverTime(float duration)
        {
            StopAllCoroutines();
            StartCoroutine(CoDuckDownOverTime(duration));
        }

        private IEnumerator CoDuckDownOverTime(float duration)
        {
            float timer = 0f;
            while (timer < duration)
            {
                timer += Time.deltaTime;
                SetDuck(Mathf.Lerp(0f, 1f, timer/duration));
                yield return null;
            }
        }

        private void OnDestroy()
        {
            musicEvent.stop(STOP_MODE.IMMEDIATE);
            if (musicEvent.isValid())
            {
                RuntimeManager.DetachInstanceFromGameObject(musicEvent);
            }

            invertEvent.stop(STOP_MODE.IMMEDIATE);
            if (invertEvent.isValid())
            {
                RuntimeManager.DetachInstanceFromGameObject(invertEvent);
            }
        }
    }
}