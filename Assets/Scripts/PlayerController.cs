using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _catMeow;
    [SerializeField] private AudioClip _slashSound;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Transform _resetPos;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D _spearPrefab;
    [SerializeField] private float _moveSpeed = 5.0f, _spearSpeed, shootSpeed = 0.2f;
    [SerializeField] private GameObject _wallPieces;
    [SerializeField] private GameObject _blood;
    [SerializeField] private GameObject _swordHighlight;
    [SerializeField] private GameObject _spearHighlight;


    private bool _hasWeapon;
    private bool _hasSword;
    private bool _usingSword;
    private bool _hasSpear;
    private bool _usingSpear;
    private bool _isattacking;
    private float _moveConstant = 1f;
    private float _shootTimer;
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootController();
        Attack();
        Stance();
        NoMoreWeapons();
    }
    private void MovePlayer()
    {
        Vector2 movementDirection = new Vector2();

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection.y += _moveConstant;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movementDirection.y -= _moveConstant;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x += _moveConstant;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x -= _moveConstant;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (!(movementDirection.Equals(Vector2.zero)))
            _animator.SetBool("isWalking", true);
        else
            _animator.SetBool("isWalking", false);

        _rb.velocity = (movementDirection.normalized * _moveSpeed);
    }
   
    private void ShootController()
    {
        _shootTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && _shootTimer > shootSpeed && _usingSpear && _gameManager._spearCount > 0)
        {
            _gameManager.UseSpear();
            _shootTimer = 0;
            var mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            var dirToMouse = mousePos - transform.position;
            float angle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg; //create rotation for bullet to face where mouse is
            var spear = Instantiate(_spearPrefab, transform.position, Quaternion.identity);
            //set direction of bullet to face something.
            spear.AddForce(dirToMouse.normalized * _spearSpeed, ForceMode2D.Impulse);
            spear.transform.rotation = Quaternion.Euler(0, 0, angle); //rotates bullet to look at mouse

        }
        else if (_gameManager._spearCount <= 0)
        {
            _hasSpear = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            _gameManager.LostLife();
            transform.position = _resetPos.position;
        }
        else if (collider.CompareTag("Sword"))
        {
            _hasSword = true;
            _gameManager.ObtainSword();
            _gameManager.CollectSword();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Spear"))
        {
            _hasSpear = true;
            _gameManager.ObtainSpear();
            _gameManager.CollectSpear();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Cat"))
        {
            _gameManager.AddLife();
            _gameManager.AddLife();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Cat2"))
        {
            _gameManager.AddLife();
            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("CatNormal"))
        {
            _audioSource.clip = _catMeow;
            _audioSource.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Destroyable") && _isattacking)
        {
            Instantiate(_wallPieces, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("EnemyBack") && _isattacking)
        {
            _gameManager.EnemyKill();
            Instantiate(_blood, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && _usingSword && _gameManager._swordCount > 0)
        {
            _gameManager.UseSword();
            _animator.SetTrigger("isAttack");
            _isattacking = true;
            Invoke("SetBoolBack", 0.5f);
            _audioSource.clip = _slashSound;
            _audioSource.Play();
        }
        else if (_gameManager._swordCount <= 0)
        {
            _hasSword = false;
        }
    }

    private void Stance()
    {
        if (_hasSword && Input.GetKeyDown(KeyCode.Alpha1))
        {
            _usingSword = true;
            _usingSpear = false;
            _animator.SetBool("useSword", true);
            _animator.SetBool("useSpear", false);
            _swordHighlight.SetActive(true);
            _spearHighlight.SetActive(false);
        }
        else if (_hasSpear && Input.GetKeyDown(KeyCode.Alpha2))
        {
            _usingSpear = true;
            _usingSword = false;
            _animator.SetBool("useSpear", true);
            _animator.SetBool("useSword", false);
            _swordHighlight.SetActive(false);
            _spearHighlight.SetActive(true);
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) || (_hasSword == false && _usingSword) || (_hasSpear == false && _usingSpear))
        {
            _usingSword = false;
            _usingSpear = false;
            _animator.SetBool("useSword", false);
            _animator.SetBool("useSpear", false);
            _swordHighlight.SetActive(false);
            _spearHighlight.SetActive(false);
        }
    }

    private void SetBoolBack()
    {
        _isattacking = false;
    }

    private void NoMoreWeapons()
    {
        if (_gameManager._swordRemaining + _gameManager._spearRemaining == 0 && _gameManager._enemyRemaining > 0 && _gameManager._swordCount <= 0 && _gameManager._spearCount <= 0)
        {
            _gameManager.GameOver();
        }
    }

}
