using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] Rigidbody _playerRigidbody;

    Vector3 velocity;

    private void Update()
    {
        Vector3 _player = new Vector3(_playerTransform.position.x, 0, _playerTransform.position.z);
        if(PlayerController._jumpCount>=4)
        transform.position = Vector3.SmoothDamp(transform.position, _player, ref velocity, 0.6f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            _playerRigidbody.AddForce(Vector3.up * 700);
            _playerRigidbody.AddForce(Vector3.forward * 700);
            PlayerController._jumpCount = 5;
            Destroy(gameObject, 1f);
        }
        
    }

}
