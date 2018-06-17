using UnityEngine.Events;

namespace VGDA.InverseWorld
{
    public class Invert_UnityEvents : InvertSubscriber
    {
        public UnityEvent OnBecomeInverted;
        public UnityEvent OnBecomeNormal;

        protected override void DoInvert(bool isInverted)
        {
            if (isInverted)
            {
                OnBecomeInverted.Invoke();
            }
            else
            {
                OnBecomeNormal.Invoke();
            }
        }
    }
}