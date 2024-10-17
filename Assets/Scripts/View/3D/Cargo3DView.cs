using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo3DView : MonoBehaviour, IMovable
{
    public bool IsTiering;
    public bool IsOnlyFloor;
    private bool IsCollision = false;

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
        Bounds targetBounds = hit.collider.bounds; // ��������� ����� �� ������� �������������

        Vector3 newPosition = transform.position;

        newPosition.y = targetBounds.max.y + (transform.localScale.y / 2f); // ������

        newPosition.x = Mathf.Clamp(newPosition.x, targetBounds.min.x + (transform.localScale.x / 2f), targetBounds.max.x - (transform.localScale.x / 2f)); //����������� �� X
        newPosition.z = Mathf.Clamp(newPosition.z, targetBounds.min.z + (transform.localScale.z / 2f), targetBounds.max.z - (transform.localScale.z / 2f));//����������� �� Y

        transform.position = newPosition;
    }

    public bool IsCanPlace()
    {
        return _checkerCollision.CheckCollisionCollider();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //���������, ��� �� ������� � ������ Play Mode , ����� �� �������� ���������� ��� � ������ Editor 
        //��������� ��� ���, ��� ��������� OverlapBox (������������� ��� ��, ��� � ��� GameObject , � ����� ������� ������)
        Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
    }


    void OnTriggerStay(Collider other)
    {
        if (_isMoving)
        {
            if (IsOnlyFloor)
            {
                // ���� ������ ����� ������ ������ �� ����
                if (other.gameObject.GetComponent<CargoArea>() != null)
                {
                    IsCollision = true;
                }
                else
                {
                    IsCollision = false;
                }
            }
            else
            {
                // ������ ����� ������ �� CargoArea ��� �� ������� �������
                if (other.gameObject.GetComponent<CargoArea>() != null)
                {
                    IsCollision = true;
                }
                else if (other.gameObject.GetComponent<Cargo3DView>().IsTiering)
                {
                    Debug.Log(other.name + " IsTiering");
                    IsCollision = _checkerCollision.IsTopCollision(other);
                }
                else
                {
                    IsCollision = false;
                }
            }
        }

    }


}

