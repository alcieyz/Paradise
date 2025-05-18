using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge: MonoBehaviour
{
    [SerializeField] private Transform _resetBridge;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            transform.position = _resetBridge.position;
        }
    }
}
