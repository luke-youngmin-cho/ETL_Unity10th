using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerController : MonoBehaviour
{
    [SerializeField] SplineAnimate _splineAnimate;
    private Animator _animator;

    private float _speedMax;
    private Vector3 _prevPos;
    private Vector3 _velocity;
    private Queue<Vector3> _velocityHistory = new Queue<Vector3>(10);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _speedMax = _splineAnimate.MaxSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        _velocity = transform.InverseTransformDirection((currentPos - _prevPos) / Time.fixedDeltaTime);

        if (_velocityHistory.Count > 10)
            _velocityHistory.Dequeue();

        _velocityHistory.Enqueue(_velocity);

        Vector3 averageVelocity = Vector3.zero;

        foreach (var item in _velocityHistory)
            averageVelocity += item;

        averageVelocity /= _velocityHistory.Count;
        _animator.SetFloat("horizontal", averageVelocity.x / _speedMax);
        _animator.SetFloat("vertical", averageVelocity.z /_speedMax);
        _prevPos = currentPos;
    }
}
