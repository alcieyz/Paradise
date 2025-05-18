using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeArea : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    private bool _bridge1Ready;
    private bool _bridge2Ready;
    [SerializeField] private Rigidbody2D _bridge1RB;
    [SerializeField] private Rigidbody2D _bridge2RB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BridgeReady();
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Bridge1Collider"))
        {
            _bridge1Ready = true;
        }
        else if (collider.CompareTag("Bridge2Collider"))
        {
            _bridge2Ready = true;
        }

    }
    private void BridgeReady()
    {
        if (_bridge1Ready && _bridge2Ready && _gameManager._enemyRemaining <= 0)
        {
            Destroy(GameObject.FindWithTag("Bridge1Collider"));

            Destroy(GameObject.FindWithTag("Bridge2Collider"));

            _bridge1RB.freezeRotation = true;
            _bridge1RB.constraints = RigidbodyConstraints2D.FreezePosition;
            _bridge2RB.freezeRotation = true;
            _bridge2RB.constraints = RigidbodyConstraints2D.FreezePosition;
            Destroy(GameObject.FindWithTag("BridgeArea"));
        }
    }
}
