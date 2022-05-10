using NaughtyAttributes;
using UnityEngine;
using Cinemachine;
using System.Collections;
using UPQP.Managers;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace UPQP.Features.SliceView
{
    public class SliceView_References : MonoBehaviour
    {
        public CinemachineVirtualCamera virtualCamera;
        public Transform cameraCenter;
        public Transform levelRoot;
    }
}
