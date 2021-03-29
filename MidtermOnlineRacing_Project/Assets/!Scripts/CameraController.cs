using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    internal enum updateMethod
    {
        fixedUpdate,
        update,
        lateUpdate
    }
    [SerializeField] private updateMethod updateDemo;

    public static GameObject player;
    public static GameObject camPos;
    [Range(0,20)]public float smoothTime = 5;

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
        transform.position = Vector3.SmoothDamp(transform.position, camPos.transform.position, ref velocity, smoothTime * Time.deltaTime);
        transform.LookAt(player.transform);
    }

    public void ReturnLobby()
    {
        SceneManager.LoadScene(0);
    }
}
