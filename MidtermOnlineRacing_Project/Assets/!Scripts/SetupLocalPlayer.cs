using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<CarController>().enabled = true;
            CameraController.camPos = this.transform.GetChild(2).gameObject;
            CameraController.player = this.gameObject;
        }
        else
        {
            GetComponent<CarController>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
