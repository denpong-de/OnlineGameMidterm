using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    internal enum updateMethod
    {
        fixedUpdate,
        update,
        lateUpdate
    }
    [SerializeField] private updateMethod updateDemo;

    Vector3 offset = new Vector3(0f, 5f, -10f);
    public static GameObject player;
    public GameObject camPos;
    [Range(0,20)]public float smoothTime = 5;

    private void Start()
    {
        //camPos = player.transform.Find("Cam Pos").gameObject;
    }

    private void FixedUpdate()
    {
        if(updateDemo == updateMethod.fixedUpdate && player != null)
        {
            cameraBehavior();
        }
    }

    private void Update()
    {
        if (updateDemo == updateMethod.update && player != null)
        {
            cameraBehavior();
        }
    }

    private void LateUpdate()
    {
        if (updateDemo == updateMethod.lateUpdate && player != null)
        {
            cameraBehavior();
        }
    }

    private void cameraBehavior()
    {
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, smoothTime * Time.deltaTime);
        transform.LookAt(player.transform);
    }
}
