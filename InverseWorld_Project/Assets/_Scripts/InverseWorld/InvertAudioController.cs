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

        protected override void Start()
        {
            base.Start();
            musicEvent = FMODUnity.RuntimeManager.CreateInstance(MusicEventName);
            musicEvent.getParameter(invertParameterName, out invertParameter);
            musicEvent.start();
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