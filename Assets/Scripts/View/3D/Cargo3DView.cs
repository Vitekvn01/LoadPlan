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


    private void Awake()
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


            AutoLinking(hit);

            if (_checkerCollision.CheckCollisionCollider())
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

    public void StopMoving()
    {

        if (_checkerCollision.CheckCollisionCollider())
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

    private void ChangeColor(Color color)
    {
        Renderer[] visualRenderers = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in visualRenderers)
        {
            renderer.material.color = color;
        }
    }

    private void AutoLinking(RaycastHit hit)
    {
        Bounds targetBounds = hit.collider.bounds; // коллайдер груза на который устанавливаем

        Vector3 newPosition = transform.position;

        newPosition.y = targetBounds.max.y + (transform.localScale.y / 2f); // высота

        newPosition.x = Mathf.Clamp(newPosition.x, targetBounds.min.x + (transform.localScale.x / 2f), targetBounds.max.x - (transform.localScale.x / 2f)); //ограничение по X
        newPosition.z = Mathf.Clamp(newPosition.z, targetBounds.min.z + (transform.localScale.z / 2f), targetBounds.max.z - (transform.localScale.z / 2f));//ограничение по Y

        transform.position = newPosition;
    }   

    public bool IsCanPlace()
    {
        return _checkerCollision.CheckCollisionCollider();
    }
}

