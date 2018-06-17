using UnityEngine;

namespace MichaelWolfGames.Effects
{
    public class SkewSpriteWithVelocity : MonoBehaviour
    {
        [SerializeField] private SpriteSkewController _skewController;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _maxHorzVelocity = 12f;
        [SerializeField] private float _maxVertVelocity = 12f;
        [SerializeField] private bool _lockX = false;
        [SerializeField] private bool _lockY = true;

        private void Start()
        {
            if (!_skewController) _skewController = GetComponent<SpriteSkewController>();
            if (!_rigidbody) _rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_skewController && _rigidbody)
            {
                var xSkew = (_lockX)? 0f :Mathf.Clamp(_rigidbody.velocity.x/_maxHorzVelocity, -1f, 1f);
                var ySkew = (_lockY) ? 0f : Mathf.Clamp(_rigidbody.velocity.y / _maxVertVelocity, -1f, 1f);
                _skewController.SetSkew(xSkew, ySkew);
            }
        }
    }
}