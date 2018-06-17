using UnityEngine;

namespace MichaelWolfGames.Effects
{
    public class ScaleWithVelocity : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Vector3 _minScale = Vector3.one;
        [SerializeField] private Vector3 _maxScale = Vector3.one;
        [SerializeField] private float _maxVertVelocity = 12f;
        //[SerializeField] private float _maxHorzVelocity = 12f;
        //[SerializeField] private float _maxVertVelocity = 12f;
        //[SerializeField] private bool _lockX = false;
        //[SerializeField] private bool _lockY = true;

        private void Start()
        {
            if (!_target) _target = this.transform;
            if (!_rigidbody) _rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_target && _rigidbody)
            {
                _target.localScale = Vector3.Lerp(_minScale, _maxScale, Mathf.Clamp(_rigidbody.velocity.y / _maxVertVelocity, 0f, 1f));
            }
        }
    }
}