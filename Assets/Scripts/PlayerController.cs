using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRigid;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Transform _playerTransform; 
    [SerializeField] float _runSpeed=7f;  
    [SerializeField]float timer;

    public static float _jumpSpeed, _jettPackSpeed, _secondJumpSpeed;

    bool _startActive, _jumpActive = false, _death = false;
    float gold;
    public static int _jumpCount=0;
    public int jumpcon;

    [Header("Score Panel")]
    [SerializeField] GameObject _ScorePanel;
    [SerializeField] Text _scoreText;


    private void Awake()
    {
        _secondJumpSpeed = 100;
        Time.timeScale = 1;
    }
    public void StartGame()
    {
        _startActive = true;
    }
    void Update()
    {
        jumpcon = _jumpCount;
        if (Input.GetMouseButtonDown(0) && _jumpCount <= 2 && !_death)
        {

            if (_startActive == true)
            {
                _runSpeed = 7.5f;
                Jump(_playerRigid, _jumpSpeed, onGroundCheck.IsOnGroud);
            }

        
        }
        if(Input.GetMouseButton(0) && _jumpCount >= 4 && JettPackController.slidervalue>=1)
        { 
            _runSpeed = _jettPackSpeed;
            if(JettPackController.slidervalue <=3)
                _runSpeed = 7.5f;
        }
        if (Input.GetMouseButtonUp(0) && _jumpCount >= 4 && JettPackController.slidervalue >= 1)
        {
            _runSpeed = 7.5f;
        }

        if (_jumpCount == 3)
        {
            Jump(_playerRigid, _secondJumpSpeed, onGroundCheck.IsOnGroud);
            _playerAnimator.SetBool("__isFly", true);
            _playerTransform.Rotate(-30, 0, 0);
        }

    }
    private void FixedUpdate()
    {
       if(_startActive==true)
        {
            _playerTransform.Translate(transform.forward * Time.deltaTime * _runSpeed);
            _playerAnimator.SetBool("__isWalk", true);
        }
       if(_playerTransform.position.y >=0.5f)
        {
            _jumpActive = true;
        }
        if (_jumpActive && onGroundCheck.IsOnGroud)
        {
            Die();
        }
    }
    

    public void Die()
    {
       if(onGroundCheck.IsOnGroud)
        timer += Time.deltaTime;

       if(timer>=0.15f && _jumpCount>=4)
            _runSpeed = 1f;

       if(_jumpCount>=5 && timer>=1f)
            StartCoroutine(Death());

        if (timer >= 0.2f && _jumpCount <=2)
        {
            _runSpeed = 3f;
            if(timer>=1.5f)
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        _death = true;
        _playerTransform.eulerAngles = new Vector3(0, 0, 0);
        _playerAnimator.SetBool("__isDie", true);
        _runSpeed = 1.5f;
        yield return new WaitForSeconds(1.2f);
        _runSpeed = 0f;
        yield return new WaitForSeconds(1.2f);
        gold = _playerTransform.position.z + 10;
        _scoreText.text = "Kazandýn\n\n" +gold.ToString("##.")+ " Gold";
        _ScorePanel.SetActive(true);

        if ((_playerTransform.position.z + 10)>PlayerPrefs.GetInt("bestscore"))
        {
            PlayerPrefs.SetInt("bestscore", (int)gold);
        }
       
        Time.timeScale = 0;

    }
    public void Topla()
    {
        UpgradeComponent._totalGold += (int)gold;
        _jumpCount = 0;
        SceneManager.LoadScene(0);
    }




    public void Jump(Rigidbody _rjump, float _jSpeed, bool _isJumpActive)
    {
        switch (_isJumpActive)
        {
            case true:
                _rjump.AddForce(Vector3.up * _jSpeed);
                _playerAnimator.SetTrigger("__isJump");
                timer = 0;
                _jumpCount++;
               

                break;

            default:
                _isJumpActive = false;
                break;
        }

    }


}
