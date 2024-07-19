using UnityEngine;
using UnityEngine.XR;


[RequireComponent(typeof(XRInputData))]
public class Test_InputReader : MonoBehaviour
{
    private XRInputData _inputData;

    private void Start()
    {
        _inputData = GetComponent<XRInputData>();
    }

    void Update()
    {
        if (_inputData.leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
        }

        if (_inputData.leftController.TryGetFeatureValue(CommonUsages.gripButton, out bool grib))
        {
        }

        if (_inputData.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton))
        {
        }

        if (_inputData.rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool Bbutton))
        {
        }
    }
}