using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SetupLocalPlayer : NetworkBehaviour
{
    public Text namePrefab;
    public Transform namePos;
    private Text nameLable;
    [SyncVar(hook = "OnChangeName")]
    public string pName = "player";

    private void Awake()
    {
        GameObject mainCanvas = GameObject.FindWithTag("MainCanvas");

        nameLable = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLable.transform.SetParent(mainCanvas.transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<CarController>().enabled = true;
            GetComponent<Timer>().enabled = true;

            CameraController.camPos = this.transform.GetChild(2).gameObject;
            CameraController.player = this.gameObject;
        }
        else
        {
            GetComponent<CarController>().enabled = false;
            GetComponent<Timer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(this.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 &&
            screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        if (onScreen)
        {
            Vector3 nameLablePos = Camera.main.WorldToScreenPoint(namePos.position);
            nameLable.transform.position = nameLablePos;
        }
        else
        {
            nameLable.transform.position = new Vector3(-1000, -1000, 0);
        }
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        pName = newName;
        nameLable.text = pName;
    }

    void OnChangeName(string n)
    {
        pName = n;
        nameLable.text = pName;
    }

    void UpdateStates()
    {
        OnChangeName(pName);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        Invoke("UpdateStates", 1);
    }

    public void OnDestroy()
    {
        if (nameLable != null)
        {
            Destroy(nameLable.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            this.SendMessage("Finnish");
        }
    }
}
