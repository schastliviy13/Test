using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject _camera;
    [SerializeField] GameObject _player;
    [SerializeField] float speed=5;

    public void Update()
    {
        Follow();
    }
    public void Follow()
    {
        if (_player!=null)
        {
            Vector3 newPosition = new Vector3(_player.transform.position.x, _player.transform.position.y,-1);
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, newPosition, speed * Time.deltaTime);
        }
    }
}
