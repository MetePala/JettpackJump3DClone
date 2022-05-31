using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody _playerRigid;
    [SerializeField] Animator _playerAnimator;
    [SerializeField] Transform _playerTransform; 
    [SerializeField] float _jumpSpeed,_runSpeed;  
    [SerializeField]float timer;

    bool _startActive,_jumpActive=false;
    public static int _jumpCount=0;
 

    [Header("Score Panel")]
    [SerializeField] GameObject _ScorePanel;
    [SerializeField] Text _scoreText;


    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _startActive = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
           if(_startActive==true)
            {
                _runSpeed = 4f;
                Debug.Log(timer);
                Jump(_playerRigid, _jumpSpeed, onGroundCheck.IsOnGroud);
               
            }  
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
       if(_jumpActive && onGroundCheck.IsOnGroud)
        {
            Die();
        }
    }
    

    public void Die()
    {
        timer += Time.deltaTime;
        if (timer >= 0.2f && _jumpCount < 3)
        {
            _runSpeed = 3f;
            if(timer>=2f)
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        _playerAnimator.SetBool("__isDie", true);
        _runSpeed = 1.5f;
        yield return new WaitForSeconds(1.2f);
        _runSpeed = 0f;
        yield return new WaitForSeconds(1.2f);
        _ScorePanel.SetActive(true);
        _scoreText.text = (_playerTransform.position.z + 10).ToString("##.");
        if((_playerTransform.position.z + 10)>PlayerPrefs.GetFloat("bestscore"))
        {
            PlayerPrefs.SetFloat("bestscore", _playerTransform.position.z + 10);
        }
       
        Time.timeScale = 0;

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
