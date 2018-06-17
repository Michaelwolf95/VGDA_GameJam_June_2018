using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MichaelWolfGames.Effects
{

    /// <summary>
    /// Simple Controller for SprikeSkew shader.
    /// 
    /// Michael Wolf
    /// May, 2018
    /// </summary>
    public class SpriteSkewController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] [Range(0f, 3f)] private float _maxHorizontalSkew = 1f;
        [SerializeField] [Range(0f, 3f)] private float _maxVerticalSkew = 1f;

        public float HorizontalSkew
        {
            get
            {
                if (_renderer)
                {
                    _renderer.material.GetFloat("_HorizontalSkew");
                }

                return 0f;
            }
        }
        public float VerticalSkew
        {
            get
            {
                if (_renderer)
                {
                    _renderer.material.GetFloat("_VerticalSkew");
                }

                return 0f;
            }
        }

        private void Awake()
        {
            if (!_renderer) _renderer = GetComponent<SpriteRenderer>();
        }

        #region Public Methods

        public void SetSkew(float horz, float vert)
        {
            SetHorizontalSkew(horz);
            SetVerticalSkew(vert);
        }
        public void SetHorizontalSkew(float horz)
        {
            if (_renderer.transform.lossyScale.x < 0)
                horz *= -1;
            _renderer.material.SetFloat("_HorizontalSkew", Mathf.Clamp(horz, -_maxHorizontalSkew, _maxHorizontalSkew));
        }
        public void SetVerticalSkew(float vert)
        {
            if (_renderer.transform.lossyScale.y < 0)
                vert *= -1;
            _renderer.material.SetFloat("_VerticalSkew", Mathf.Clamp(vert, -_maxVerticalSkew, _maxVerticalSkew));
        }

        #endregion
    }

}

