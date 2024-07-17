using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AlignTransform : MonoBehaviour
{
    [SerializeField] private ActionBasedController _target;

    private void Update()
    {
        transform.localPosition = _target.positionAction.action.ReadValue<Vector3>();
        transform.localRotation = _target.rotationAction.action.ReadValue<Quaternion>();
    }
}
