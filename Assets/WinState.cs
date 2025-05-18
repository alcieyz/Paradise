using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinState : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameManager _gm;
    [SerializeField] private GameObject _player;
    [SerializeField] private TMP_Text timer_text;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            _player.SetActive(false);
            timer_text.SetText(_gm.GetTimer().ToString());
            _winScreen.SetActive(true);
        }
        
    }
}
