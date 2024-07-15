using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Player _playerMovement;

    private void Update()
    {
        transform.position = new Vector3(_playerMovement.transform.position.x, _playerMovement.transform.position.y, transform.position.z);
    }
}
