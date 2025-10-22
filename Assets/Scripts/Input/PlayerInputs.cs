using System;
using UnityEngine;
using UnityEngine.InputSystem;


public static class PlayerInputs
{
    
        public static void InvokeInputEvent(StInputEvent inputEvent, InputAction.CallbackContext ctx) {
            if (ctx.started) {
                inputEvent.StartEvent();
            }
            else if (ctx.performed) {
                inputEvent.PerformEvent();
            }
            else if (ctx.canceled) {
                inputEvent.CancelEvent();
            }
        }

    public struct StInputEvent {
        public event Action Started;
        public event Action Canceled;
        public event Action Performed;
        
        public void StartEvent() => Started?.Invoke();
        public void PerformEvent() => Performed?.Invoke();
        public void CancelEvent() => Canceled?.Invoke();
    }

    public struct BoolInput {
        public bool IsPressed;
        public bool IsReleased;
    }
}
