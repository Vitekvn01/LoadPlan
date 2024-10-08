using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo3DView : MonoBehaviour, IMovable
{
    private bool _isMoving = false;
    private Vector3 _lastDropPosition;
    private Vector3 _movablePosition;

    public void StartMoving()
    {
        _lastDropPosition = transform.position;
        gameObject.layer = 2;
        _isMoving = true;
    }

    public void Move(Vector3 hitPos)
    {
        if (_isMoving)
        {
            transform.position = hitPos;
        }
    }

    public void OnDrop()
    {
        transform.position = _lastDropPosition;
    }

    public void StopMoving()
    {
        gameObject.layer = 0;
        _isMoving = false;
        _lastDropPosition = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


}
    
