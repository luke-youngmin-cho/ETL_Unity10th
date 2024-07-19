using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// XR 기기 정보 캐싱 클래스
/// </summary>
public class XRInputData : MonoBehaviour
{
    public InputDevice rightController => _rightController;
    public InputDevice leftController => _leftController;
    public InputDevice HMD => _hmd;

    private InputDevice _rightController;
    private InputDevice _leftController;
    private InputDevice _hmd;

    public Vector3 leftControllerPositionOffset;
    public Vector3 rightControllerPositionOffset;
    public Quaternion leftControllerRotationOffset;
    public Quaternion rightControllerRotationOffset;

    public struct DevicesState
    {
        public bool leftControllerIsValid;
        public bool rightControllerIsValid;
        public bool OffsetIsValid;
    }

    IEnumerator C_SetControllerOffset(Action<DevicesState> completed)
    {
        while (true)
        {
            if (leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool leftTriggerButton) &&
                leftController.TryGetFeatureValue(CommonUsages.triggerButton, out bool rightTriggerButton))
            {
                float timer = 1.0f;
                while (leftTriggerButton && rightTriggerButton)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 0.0f)
                    {
                        if (leftController.TryGetFeatureValue(CommonUsages.devicePosition, out leftControllerPositionOffset) &&
                            rightController.TryGetFeatureValue(CommonUsages.devicePosition, out rightControllerPositionOffset) &&
                            leftController.TryGetFeatureValue(CommonUsages.deviceRotation, out leftControllerRotationOffset) &&
                            rightController.TryGetFeatureValue(CommonUsages.deviceRotation, out leftControllerRotationOffset))
                        {
                            completed?.Invoke(new DevicesState
                            {
                                leftControllerIsValid = true,
                                rightControllerIsValid = true,
                                OffsetIsValid = true,
                            });
                        }
                    }

                    yield return null;
                }
            }
            yield return null;
        }
    }

    void Update()
    {
        if (!rightController.isValid || !leftController.isValid || !HMD.isValid)
            InitializeInputDevices();
    }

    private void InitializeInputDevices()
    {
        if (!rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);

        if (!leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);

        if (!HMD.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _hmd);
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        if (devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }
}