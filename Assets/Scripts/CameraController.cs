using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    [SerializeField] bool movingCamera;
    [SerializeField] float offsetX = 0;
    [SerializeField] float OffsetY = 0;
    [SerializeField] bool movingUpCamera;

    private Vector3 cameraPos;

    private void Update()
    {
        if (movingCamera)
        {
            if (movingUpCamera)
            {
                cameraPos = new Vector3(player.transform.position.x, player.transform.position.y - OffsetY, transform.position.z);
                transform.position = cameraPos;
            }
            else
            {
                cameraPos = new Vector3(player.transform.position.x - offsetX, transform.position.y - OffsetY, transform.position.z);
                transform.position = cameraPos;

            }

        }

    }
}
