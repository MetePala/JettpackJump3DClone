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
    bool die = false;
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
    }


    void Update()
    {
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
