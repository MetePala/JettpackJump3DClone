using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Vector3 velocity;
    [SerializeField] float _smoothTime;
    void Start()
    {

    }

    void Update()
    {
        Vector3 _targetPos = new Vector3(transform.position.x, transform.position.y, _player.position.z-3f);
        transform.position = Vector3.SmoothDamp(transform.position, _targetPos, ref velocity, _smoothTime);
    }
    private void FixedUpdate()
    {
    
    }
}
