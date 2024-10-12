using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo3DView : MonoBehaviour, IMovable
{
    public bool IsTiering;
    public bool IsOnlyFloor;


    private bool _isMoving = false;
    private Vector3 _lastDropPosition;
    private Vector3 _movablePosition;
    private CheckerCollision _checkerCollision;

    private void Start()
    {
        _checkerCollision = new CheckerCollision(gameObject);
    }

    public void StartMoving()
    {
        _lastDropPosition = transform.position;
        gameObject.layer = 2;
        _isMoving = true;
    }

    public void Move(RaycastHit hit)
    {
        if (_isMoving)
        {
            transform.position = hit.point + hit.normal * (transform.localScale.y / 2f);
            if (_checkerCollision.CheckCollisionCollider() && Vector3.Dot(hit.normal, Vector3.up) == 1f)
            {
                ChangeColor(Color.green);
            }
            else
            {
                ChangeColor(Color.red);
            }
        }
    }

    public void OnDrop()
    {
        transform.position = _lastDropPosition;
    }

    public void StopMoving(RaycastHit hit)
    {

        if (_checkerCollision.CheckCollisionCollider() && Vector3.Dot(hit.normal, Vector3.up) == 1f)
        {
            _lastDropPosition = transform.position;
            ChangeColor(new Color(1, 1, 1, 1));
        }
        else
        {
            OnDrop();
            ChangeColor(new Color(1, 1, 1, 1));
        }

        gameObject.layer = 0;
        _isMoving = false;
    }

    private void ChangeColor( Color color)
    {
        Renderer[] visualRenderers = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in visualRenderers)
        {
            renderer.material.color = color;
        }
    }


}

