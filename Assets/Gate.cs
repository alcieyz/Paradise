using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameManager _gm;
    [SerializeField] private GameObject _player;
    [SerializeField] private TMP_Text timer_text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (_gm._enemyRemaining <= 0)
        {
            _player.SetActive(false);
            timer_text.SetText(_gm.GetTimer().ToString());
            _winScreen.SetActive(true);
        }
        
    }
}
