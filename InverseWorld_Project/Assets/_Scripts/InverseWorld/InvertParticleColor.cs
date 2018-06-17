using UnityEngine;

namespace VGDA.InverseWorld
{
    public class InvertParticleColor : InvertSubscriber
    {
        public ParticleSystem pSystem;
        public Color normalColor = Color.white;
        public Color invertColor = Color.black;

        protected override void Start()
        {
            base.Start();
            if (!pSystem)
            {
                pSystem = GetComponent<ParticleSystem>();
            }
            DoInvert(false);
        }

        protected override void DoInvert(bool isInverted)
        {
            Color newColor = (isInverted) ? invertColor : normalColor;
            var psRenderer = pSystem.GetComponent<ParticleSystemRenderer>();
            psRenderer.material.SetColor("_TintColor", newColor);
            psRenderer.material.SetColor("_Color", newColor);

            var main = pSystem.main;
            main.startColor = newColor;

            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[pSystem.particleCount];
            pSystem.GetParticles(particles);
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].startColor = newColor;
            }
            pSystem.SetParticles(particles, pSystem.particleCount);
        }
    }
}