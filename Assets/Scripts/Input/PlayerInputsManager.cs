using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputs;

public class PlayerInputsManager : MonoBehaviour
{
        public StInputEvent OnMoveEvent;
        public Vector2 MoveValue;
        public Vector2 LookValue;
        
        public StInputEvent OnJumpEvent;
        
        public StInputEvent OnSprintEvent;

        public StInputEvent OnClickEvent;
        
        public StInputEvent OnLookEvent;
        
        public void OnMove(InputAction.CallbackContext ctx) {
            MoveValue = ctx.ReadValue<Vector2>();
            InvokeInputEvent(OnMoveEvent, ctx);
        }
        
        public void OnJump(InputAction.CallbackContext ctx) {
            InvokeInputEvent(OnJumpEvent,ctx);
        }
        
        public void OnSprint(InputAction.CallbackContext ctx) {
            InvokeInputEvent(OnSprintEvent,ctx);
        }

        public void OnClick(InputAction.CallbackContext ctx){
            InvokeInputEvent(OnClickEvent,ctx);
        }
        
        public void OnLook(InputAction.CallbackContext ctx) {
            LookValue = ctx.ReadValue<Vector2>();
            InvokeInputEvent(OnMoveEvent, ctx);
        }
}
