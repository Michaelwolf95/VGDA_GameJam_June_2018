using UnityEngine;

namespace VGDA.InverseWorld
{
    public class InvertAudioController : InvertSubscriber
    {
        [FMODUnity.EventRef]
        public string MusicEventName = "event:/EVENT";
        public string invertParameterName = "";

        public FMOD.Studio.EventInstance musicEvent;
        public FMOD.Studio.ParameterInstance invertParameter;

        [FMODUnity.EventRef]
        public string InvertEventName = "event:/EVENT";

        protected override void Start()
        {
            base.Start();
            musicEvent = FMODUnity.RuntimeManager.CreateInstance(MusicEventName);
            musicEvent.getParameter(invertParameterName, out invertParameter);
            musicEvent.start();
            musicEvent.setParameterValue("duck_music", 1f);
            //invertParameter.setValue(1f);
        }
        protected override void DoInvert(bool isInverted)
        {
            if (isInverted)
            {
                invertParameter.setValue(1f);
            }
            else
            {
                invertParameter.setValue(0f);
            }
            musicEvent = FMODUnity.RuntimeManager.CreateInstance(InvertEventName);
            musicEvent.start();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.BackQuote))
            {
                InvertManager.Instance.ToggleInvert();
            }
        }
    }
}