using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    [SerializeField] bool movingCamera;

    private Vector3 cameraPos;

    private void Update()
    {   
        if (movingCamera)
        {
            cameraPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

            transform.position = cameraPos;
        }
        
    }
}
