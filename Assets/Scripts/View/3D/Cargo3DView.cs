using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo3DView : MonoBehaviour, IMovable
{

    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public string Name { get; private set; }

    public bool IsTiering;
    public bool IsOnlyFloor;



    private bool IsCollision = false;
    private bool _isMoving = false;

    private Vector3 _lastDropPosition;

    private CheckerCollision _checkerCollision;


    private void Start()
    {
        _checkerCollision = new CheckerCollision(this);
    }

    #region IMovable
    public void StartMoving()
    {
        _lastDropPosition = transform.position;
        gameObject.layer = 2;
        _isMoving = true;
        gameObject.GetComponent<Collider>().isTrigger = true;
    }

    public void Move(RaycastHit hit)
    {
        if (_isMoving)
        {

            transform.position = hit.point + hit.normal * (transform.localScale.y / 2f);


            AutoLinking(hit);

            if (IsCollision)
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

        if (IsCollision)
        {
            _lastDropPosition = transform.position;
            ChangeColor(new Color(1, 1, 1, 1));
        }
        else
        {
            OnDrop();
            ChangeColor(new Color(1, 1, 1, 1));
        }
        gameObject.GetComponent<Collider>().isTrigger = false;
        gameObject.layer = 0;
        _isMoving = false;
    }
    public bool IsCanPlace()
    {
        return IsCollision;
    }
    #endregion
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

    public void SetParameters(float length, float width, float height, float weight, string name, bool isTiering, bool isOnlyFloor)
    {

        Length = length;
        Height = height;
        Width = width;
        Weight = weight;
        Name = name;
        IsTiering = isTiering;
        IsOnlyFloor = isOnlyFloor;
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (_isMoving)
        {
            IsCollision = _checkerCollision.CheckCollisionCollider(other);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
    }
}

