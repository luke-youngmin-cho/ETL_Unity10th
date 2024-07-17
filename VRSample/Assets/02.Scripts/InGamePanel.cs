using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private ActionBasedController _left;
    [SerializeField] private ActionBasedController _right;
    [SerializeField] TMP_Text _leftPos;
    [SerializeField] TMP_Text _rightPos;

    private void Update()
    {
        _leftPos.text = _left.positionAction.action.ReadValue<Vector3>().ToString();
        _rightPos.text = _right.positionAction.action.ReadValue<Vector3>().ToString();
    }
}
