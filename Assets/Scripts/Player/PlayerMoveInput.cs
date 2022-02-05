using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMoveInput : MonoBehaviour
{

    private PlayerAction ActionScript;
    private void Start() {
        ActionScript=gameObject.GetComponent<PlayerAction>();    
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        ActionScript.Move(inputMovement);
        
        //player.SendMessage("Move", inputMovement, SendMessageOptions.DontRequireReceiver);
    }

    public void OnLooking(InputAction.CallbackContext value)
    {
        Vector2 inputLooking = value.ReadValue<Vector2>();
        ActionScript.LookingAround(inputLooking);
        //player.SendMessage("LookingAround",inputLooking, SendMessageOptions.DontRequireReceiver);
    }


    public void OnHeight(InputAction.CallbackContext value)
    {

        var height = value.ReadValue<float>();
        if (value.started){
            ActionScript.Jump(height);
        }
        //player.SendMessage("Height", height, SendMessageOptions.DontRequireReceiver);
    }

    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.started){
            ActionScript.Shoot();
        }
    }

    public void GetInScope(InputAction.CallbackContext value){
        if (value.started)
        {
            ActionScript.GetInScope(1);
        }else if(value.canceled){
            ActionScript.GetInScope(2);
        }
    }
    void DebugValue(InputAction.CallbackContext value)
    {
        Debug.Log(value.started);
        Debug.Log(value.performed);
        Debug.Log(value.canceled);
    }
}

