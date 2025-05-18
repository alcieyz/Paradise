using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow: MonoBehaviour
{
    public Transform target; //reference to player's transform component
    public Vector3 offset; //offset of the camera from player (optional)
    private Camera mainCamera; //reference to main camera

    // Start is called before the first frame update
    void Start()
    {
       mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target == null || mainCamera == null)
            return;

        //update camera's position to follow player's position with an offset
        mainCamera.transform.position = target.position + offset;
    }
}
