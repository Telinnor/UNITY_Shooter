using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    //PUBLIC VARIABLES:
    public float RotationSpeed;
    public float MovingSpeed;
    public float HeightSpeed;
    public GameObject Head;
    public GameObject bullet;
    public enum MovingMethodType { Translate, SimpleMove,Move }
    public MovingMethodType MovingMethod;
    public float Gravity;
    public float JumpStrength;
    public Camera myCamera;
    public float ScopeStrength;


    //PRIVATE VARIABLES:
    private CharacterController _characterController;
    private float currentGravity=0f;


    void Start()
    {
        gameObject.transform.Rotate(0, 0, 0, Space.World);
        _characterController=GetComponent<CharacterController>();
        //InvokeRepeating("OutputTime", 1f, 0.5f);  //1s delay, repeat every 1s
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLooking();
        //Debug.Log(gameObject.transform.forward);


        if(MovingMethod.ToString() == "Translate"){
            UpdatePositionTranslate();
        }else if(MovingMethod.ToString() == "SimpleMove"){
            UpdatePositionSimpleMove();
        }else if(MovingMethod.ToString() == "Move"){
            UpdatePositionMove();
        }

    }

    private Vector3 rawInputMovement;
    public void Move(Vector2 value)
    {
        //Debug.Log("Move Event");
        rawInputMovement.x = value.x;
        rawInputMovement.z = value.y;
        //rawInputMovement = new Vector3(value.x, 0,value.y);
        //UpdatePosition();
        //DebugValue(value);
    }

    private Vector3 LookingInputData;
    public void LookingAround(Vector2 value)
    {
        //Debug.Log("OnLooking Event");

        LookingInputData = new Vector3(-value.y, value.x, 0);
    }

    private float currentFOV;
    public void GetInScope(int value){ 
        if(value==1){   //1 = Get in Scope
            currentFOV=myCamera.fieldOfView;
            myCamera.fieldOfView = currentFOV * ScopeStrength;
            Debug.Log("Get in Scope");
        }else if (value==2){ //2 = Get out of Scope
            myCamera.fieldOfView = currentFOV;
            Debug.Log("Get out of Scope");
        }
    }
    public void Jump(float value)
    {
        Debug.Log("Jump");
        //rawInputMovement.y = value;
        if(_characterController.isGrounded){
            currentGravity=-JumpStrength;   
        }
    }

    public void Shoot(){
        Debug.Log("Shoot fired");
        var HeadPos=gameObject.transform.GetChild(1).transform;
        Instantiate(bullet, HeadPos.position, HeadPos.rotation);
    }


    // private void OnCollisionEnter(Collision other)
    // {
    //     MovingSpeed = MovingSpeed / 2;
    //     //Debug.Log("Collision Start");
    // }
    // private void OnCollisionExit(Collision other)
    // {
    //     MovingSpeed = MovingSpeed * 2;
    // }

    void UpdateLooking()
    {

        //Head.transform.Rotate(LookingInputData.x * Time.deltaTime * RotationSpeed,0,0);
        gameObject.transform.Rotate(0, LookingInputData.y * Time.deltaTime * RotationSpeed, 0);

        float XturnSpeed = LookingInputData.x * Time.deltaTime * RotationSpeed;
        Vector3 currentAngles = Head.transform.eulerAngles;
        if (currentAngles.x + XturnSpeed > 360 - 89 || currentAngles.x + XturnSpeed < 89)
        {
            Head.transform.Rotate(XturnSpeed, 0, 0);
        }

    }
    void UpdatePositionTranslate()
    {
        Vector3 AnglesDEG = Head.transform.eulerAngles;
        Vector3 AnglesRAD = AnglesDEG * Mathf.Deg2Rad;
        Vector3 Translate;
        Translate.x = rawInputMovement.z * Time.deltaTime * MovingSpeed * Mathf.Sin(AnglesRAD.y) + rawInputMovement.x * Time.deltaTime * MovingSpeed * Mathf.Sin(AnglesRAD.y + Mathf.PI / 2);
        Translate.z = rawInputMovement.z * Time.deltaTime * MovingSpeed * Mathf.Cos(AnglesRAD.y) + rawInputMovement.x * Time.deltaTime * MovingSpeed * Mathf.Cos(AnglesRAD.y + Mathf.PI / 2);

        Translate.y = rawInputMovement.y * Time.deltaTime * HeightSpeed;

        //gameObject.transform.Translate(Translate.x,0,Translate.z);
        gameObject.transform.Translate(Translate, Space.World);

    }

    void UpdatePositionSimpleMove(){

        /*
        Vector3 AnglesDEG = Head.transform.eulerAngles;
        Vector3 AnglesRAD = AnglesDEG * Mathf.Deg2Rad;
        Vector3 Translate;
        Translate.x = rawInputMovement.z * MovingSpeed * Mathf.Sin(AnglesRAD.y) + rawInputMovement.x * MovingSpeed * Mathf.Sin(AnglesRAD.y + Mathf.PI / 2);
        Translate.z = rawInputMovement.z * MovingSpeed * Mathf.Cos(AnglesRAD.y) + rawInputMovement.x * MovingSpeed * Mathf.Cos(AnglesRAD.y + Mathf.PI / 2);
        Translate.y = rawInputMovement.y * Time.deltaTime * HeightSpeed;
        _characterController.SimpleMove(Translate);
        */


        Vector3 Move=Vector3.zero;
        Move+=rawInputMovement.z *gameObject.transform.forward*MovingSpeed;
        Move+=rawInputMovement.x*gameObject.transform.right*MovingSpeed;
        //Move+=rawInputMovement.y*gameObject.transform.up*HeightSpeed;


        _characterController.SimpleMove(Move);
    }

    void UpdatePositionMove(){
        _characterController.Move(Movement()*Time.deltaTime);
    }

    Vector3 Movement(){
        Vector3 moveVector=Vector3.zero;

        moveVector+=gameObject.transform.forward*rawInputMovement.z;
        moveVector+=gameObject.transform.right*rawInputMovement.x;
        moveVector*=MovingSpeed;

        //moveVector+=gameObject.transform.up*HeightSpeed*rawInputMovement.y;
        moveVector+=ApplyGravity();
        return moveVector;
    }

    Vector3 ApplyGravity(){
        Vector3 GravityMovement=new Vector3(0,-currentGravity,0);
        currentGravity+=Gravity*Time.deltaTime;

        if(_characterController.isGrounded && currentGravity>1f){
            currentGravity=1f;
        }

        //Debug.Log(currentGravity);
        return GravityMovement;
    }

    private void OutputTime(){
        Debug.Log(currentGravity);
    }
}
