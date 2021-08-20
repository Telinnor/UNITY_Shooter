using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class config : MonoBehaviour
{

    public bool LockMouse;
    public int PreciseFPS;
    void Awake()
    {
        if(LockMouse==true)
            Cursor.lockState = CursorLockMode.Locked;
        if(PreciseFPS>0)
            Application.targetFrameRate = PreciseFPS;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
