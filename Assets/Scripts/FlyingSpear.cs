using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlyingSpear : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _blood;
    [SerializeField] private GameObject _wallPieces;

    private void Awake()
    {
        var gameManager = FindAnyObjectByType<GameManager>();
        _gameManager = gameManager;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Destroyable"))
        {
            Instantiate(_wallPieces, transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("EnemyBack"))
        {
            _gameManager.EnemyKill();
            Instantiate(_blood, transform.position, Quaternion.identity);
            Destroy(collider.gameObject);
        }
        Destroy(gameObject);
    }
}
