using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerInputs;

public class PlayerInputsManager : MonoBehaviour
{
        public StInputEvent OnMoveEvent;
        public Vector2 MoveValue;
        
        /*public StInputEvent OnZoomEvent;
        public float ZoomValue;*/

        public StInputEvent OnClickEvent;
        
        public void OnMove(InputAction.CallbackContext ctx) {
            MoveValue = ctx.ReadValue<Vector2>();
            InvokeInputEvent(OnMoveEvent, ctx);
        }
        
        /*public void OnZoom(InputAction.CallbackContext ctx) {
            ZoomValue = ctx.ReadValue<float>();
            InvokeInputEvent(OnZoomEvent, ctx);
        }*/

        public void OnClick(InputAction.CallbackContext ctx){
            InvokeInputEvent(OnClickEvent,ctx);
        }
}
