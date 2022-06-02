using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JettPackController : MonoBehaviour
{
    [SerializeField] GameObject _sliderPanel;
    [SerializeField] Slider _jettpackSlider;
    [SerializeField] TrailRenderer _trail;
    [SerializeField] Rigidbody _playerRigidbody;
    public static float slidervalue;
    private void Update()
    {
        if(PlayerController._jumpCount>=3)
        {
            _sliderPanel.SetActive(true);
        }
        if (Input.GetMouseButton(0) && PlayerController._jumpCount >= 3 && _jettpackSlider.value>=1)
        {
            _trail.emitting = true;
            _playerRigidbody.AddForce(Vector3.up * 2);
            if(_jettpackSlider.value <=3)
                _trail.emitting = false;
        }
        if (Input.GetMouseButtonUp(0) && PlayerController._jumpCount >= 3)
        {
            _trail.emitting = false;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0) && PlayerController._jumpCount >= 3)
        {
            _jettpackSlider.value -= 1f;
            slidervalue = _jettpackSlider.value;
        }
    }
}

