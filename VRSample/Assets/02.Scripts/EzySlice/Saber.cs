using EzySlice;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Saber : MonoBehaviour
{
    public Transform plane;
    private Quaternion _prevRotation;
    public Material slicedSurface;
    private LayerMask _noteMask;
    private Vector3 _prevPos;
    [SerializeField] private float _speedRequiredToSlice = 2f;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _noteMask = LayerMask.GetMask("Slicible");
        _prevRotation = transform.rotation;
        _prevPos = transform.position;
    }


    private void FixedUpdate()
    {
        RotatePlane();

        _speed = (transform.position - _prevPos).magnitude / Time.fixedDeltaTime;
        _prevPos = transform.position;
    }

    private void Slice(GameObject target)
    {
        SlicedHull hull = target.Slice(plane.position, plane.up);

        if (hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, slicedSurface);
            upperHull.AddComponent<UpperHullBehaviour>().velocity = plane.TransformDirection(Vector3.right + Vector3.up) * 1f;
            GameObject lowerHull = hull.CreateLowerHull(target, slicedSurface);
            lowerHull.AddComponent<LowerHullBehaviour>().velocity = plane.TransformDirection(Vector3.left + Vector3.up) * 1f;
            Destroy(target);
        }
    }

    private void RotatePlane()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion delta = Quaternion.Inverse(_prevRotation) * currentRotation;
        Vector3 deltaEuler = delta.eulerAngles;
        Vector3 planeLocalEulerAngles = plane.localEulerAngles;
        planeLocalEulerAngles.z += deltaEuler.z;
        plane.localEulerAngles = planeLocalEulerAngles;
        _prevRotation = currentRotation;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & _noteMask) > 0)
        {
            if (_speed >= _speedRequiredToSlice)
            {
                Debug.Log("Slice!");
                Slice(other.gameObject);
            }
        }
    }
}