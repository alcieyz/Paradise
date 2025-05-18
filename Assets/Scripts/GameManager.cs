using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;

public class GameManager: MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private int _lifeCounter = 3;
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private float timer = 500f;
    public int _swordCount = 0;
    [SerializeField] private TMP_Text _swordText;
    public int _spearCount = 0;
    [SerializeField] private TMP_Text _spearText;
    [SerializeField] private TMP_Text _enemyRemainingText;
    public int _enemyRemaining = 17;
    [SerializeField] private TMP_Text _swordRemainingText;
    [SerializeField] private TMP_Text _spearRemainingText;
    public int _swordRemaining = 4;
    public int _spearRemaining = 4;



    [SerializeField] private GameObject _Player;
    [SerializeField] private GameObject _gameOver;
    private bool isGameOver;

    [SerializeField] private GameObject _pauseMenu;
    private bool _isPaused;

    // Start is called before the first frame update
    void Start()
    {
        _lifeText.text = "Life: " + _lifeCounter.ToString();
        _swordText.text = _swordCount.ToString();
        _spearText.text = _swordCount.ToString();
        _enemyRemainingText.text = _enemyRemaining.ToString();
        _swordRemainingText.text = "Remaining: " + _swordRemaining.ToString();
        _spearRemainingText.text = "Remaining: " + _spearRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            TimerCount();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void GameOver()
    {
        isGameOver = true;
        _gameOver.SetActive(true);
        _Player.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LostLife()
    {

        --_lifeCounter; //subtract 1
        _lifeText.text = "Life: " + _lifeCounter.ToString();

        if(_lifeCounter <= 0)
        {
            GameOver();
        }
    }

    public void AddLife()
    {
        ++_lifeCounter; //add 1
        _lifeText.text = "Life: " + _lifeCounter.ToString();
    }

    private void TimerCount()
    {
        timer -= Time.deltaTime;
        int intTime = Mathf.CeilToInt(timer);
        _timerText.text = intTime.ToString();

        if(timer < 0)
        {
            GameOver();
        }
    }
    public void ObtainSword()
    {
        _swordCount += 5;
        _swordText.text = _swordCount.ToString();

    }
    public void UseSword()
    {
        --_swordCount;
        _swordText.text = _swordCount.ToString();

    }
    public void ObtainSpear()
    {
        _spearCount += 3;
        _spearText.text = _spearCount.ToString();

    }
    public void UseSpear()
    {
        --_spearCount;
        _spearText.text = _spearCount.ToString();
        
    }

    public void EnemyKill()
    {
        --_enemyRemaining;
        _enemyRemainingText.text = _enemyRemaining.ToString();
    }

    public float GetTimer()
    {
        return Mathf.CeilToInt(timer);
    }

    public void CollectSword()
    {
        --_swordRemaining;
        _swordRemainingText.text = "Remaining: " + _swordRemaining.ToString();
    }
    public void CollectSpear()
    {
        --_spearRemaining;
        _spearRemainingText.text = "Remaining: " + _spearRemaining.ToString();
    }
}
