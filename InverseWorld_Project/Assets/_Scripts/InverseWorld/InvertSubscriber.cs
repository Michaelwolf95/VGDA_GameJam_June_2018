using System;
using MichaelWolfGames;

namespace VGDA.InverseWorld
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class InvertSubscriber : SubscriberBase<InvertManager>
    {
        protected override void FetchSubscribableObject()
        {
            base.FetchSubscribableObject();
            if (SubscribableObject == null && InvertManager.Instance != null)
            {
                SubscribableObject = InvertManager.Instance;
            }
        }

        protected override void SubscribeEvents()
        {
            SubscribableObject.OnInvert += DoInvert;
            SubscribableObject.OnInvertFailed += DoInvertFailed;
        }

        protected override void UnsubscribeEvents()
        {
            SubscribableObject.OnInvert -= DoInvert;
        }

        protected abstract void DoInvert(bool isInverted);

        protected virtual void DoInvertFailed()
        {
            // Nothing by default
        }

    }
}