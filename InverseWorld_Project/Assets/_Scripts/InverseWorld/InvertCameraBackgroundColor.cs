using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VGDA.InverseWorld
{
    public class InvertCameraBackgroundColor : InvertSubscriber
    {
        public Camera camera;
        public Color normalColor = Color.white;
        public Color invertColor = Color.black;

        protected override void Start()
        {
            base.Start();
            if (!camera)
            {
                camera = Camera.main;
            }
            DoInvert(false);
        }

        protected override void DoInvert(bool isInverted)
        {
            if (isInverted)
            {
                camera.backgroundColor = invertColor;
            }
            else
            {
                camera.backgroundColor = normalColor;
            }
        }
    }
}
