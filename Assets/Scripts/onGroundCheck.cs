using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onGroundCheck : MonoBehaviour
{
    [SerializeField] Transform[] _translates;
    [SerializeField] bool _IsOnGroud = true;
    [SerializeField] float _maxDistance;
    [SerializeField] LayerMask _layerMask;
    public static bool IsOnGroud;
    [SerializeField] Image _circle;
    [SerializeField] Image _yellowCircle;
    [SerializeField] GameObject line;
    [SerializeField] Text _perfectOkText;
    [SerializeField] GameObject _perfectOk;
    bool die = false;
    public float timer;
    private void Awake()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("plane"))
        {
            die = true;
        }
    }

    private void FixedUpdate()
    {
  
        _circle.rectTransform.sizeDelta = new Vector2((transform.position.y*3)+7, (transform.position.y * 3) + 7);

        if(PlayerController._jumpCount==1)
        {
            _yellowCircle.enabled = true;
        }
        if(PlayerController._jumpCount == 4)
        {
            _yellowCircle.enabled = false;
            _circle.enabled = false;
        }
        if(die)
            FindObjectOfType<PlayerController>().Die();
        if(Input.GetMouseButtonDown(0))
        {
            die = false;
        }

        if(_IsOnGroud)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = -0.1f;
        }

    }
    IEnumerator Waiting()
    {
        _perfectOk.SetActive(true);
        yield return new WaitForSeconds(1f);
        _perfectOk.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && PlayerController._jumpCount == 0)
        {
            float fark = Mathf.Abs(gameObject.transform.position.z - line.transform.position.z);
            if (fark <= 0.5f)
            {
                _perfectOkText.color = Color.magenta;
                _perfectOkText.text = "Perfect!";
                StartCoroutine(Waiting());
                PlayerController._secondJumpSpeed += 20;

            }
            else if (fark <= 1.8f)
            {
                _perfectOkText.color = Color.grey;
                _perfectOkText.text = "OK!";
                StartCoroutine(Waiting());
                PlayerController._secondJumpSpeed += 10;
            }
        }

        if (Input.GetMouseButtonDown(0) && (PlayerController._jumpCount ==1 || PlayerController._jumpCount == 2) && _IsOnGroud)
        {
            if(timer<=0f)
            {
                _perfectOkText.color = Color.magenta;
                _perfectOkText.text = "Perfect!";
                StartCoroutine(Waiting());
                PlayerController._secondJumpSpeed += 20;
            }
            else if (timer <= 0.2f)
            {
                _perfectOkText.color = Color.grey;
                _perfectOkText.text = "OK!";
                StartCoroutine(Waiting());
                PlayerController._secondJumpSpeed += 10;
            }

        }




        foreach (Transform footTransform in _translates)
        {
            CheckFootOnGroud(footTransform);

            if (_IsOnGroud) break;
        }
        IsOnGroud = _IsOnGroud;
    }

    void CheckFootOnGroud(Transform footTransform)
    {
        Ray hitt = new Ray(footTransform.position, footTransform.TransformDirection(-footTransform.up * _maxDistance));
        Debug.DrawRay(footTransform.position, footTransform.TransformDirection(-footTransform.up * _maxDistance), Color.red);

        if (Physics.Raycast(hitt,out RaycastHit hit,_maxDistance))
        {
            _IsOnGroud = true;
        }
        else
        {
            _IsOnGroud = false;
        }
    }

   
}
