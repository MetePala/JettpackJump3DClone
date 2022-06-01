using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject _camera1;
    [SerializeField] GameObject _camera2;
    private void Awake()
    {
        _camera1.SetActive(false);
        _camera2.SetActive(true);

    }
    private void Update()
    {
      if(Input.GetKeyDown(KeyCode.Space))
        {
            _camera1.SetActive(true);
            _camera2.SetActive(false);
        }
    }

}
