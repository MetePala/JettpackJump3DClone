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
    [SerializeField] float _runSpeed;  
    [SerializeField]float timer;
    [SerializeField] TrailRenderer _trail;

    public static float _jumpSpeed, _jettPackSpeed;

    bool _startActive,_jumpActive=false , _jettPack=false;
    float gold;
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

        if (_jumpCount == 3)
        {
            Jump(_playerRigid, 100, onGroundCheck.IsOnGroud);
            _jettPack = true;
            _playerAnimator.SetBool("__isFly", true);
            _playerTransform.Rotate(-30, 0, 0);
        }
        if(Input.GetMouseButton(0) && _jettPack == true)
        {
            _trail.emitting = true;
            if (_jumpCount == 4)
            {
                _playerRigid.AddForce(Vector3.up * 2);
                _runSpeed = _jettPackSpeed;
            }
        }
        if (Input.GetMouseButtonUp(0) && _jettPack == true)
        {
            _trail.emitting = false;
            _runSpeed = 5f;
        }

        if (Input.GetMouseButtonDown(0) && _jettPack == false)
        {
            
            if(_startActive==true)
            {
                _runSpeed = 5f;
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
        if (_jumpActive && onGroundCheck.IsOnGroud)
        {
            Die();
        }
    }
    

    public void Die()
    {
       if(onGroundCheck.IsOnGroud)
        timer += Time.deltaTime;

        if (timer >= 0.2f && _jumpCount < 5)
        {
            _runSpeed = 3f;
            if(timer>=2f)
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
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
