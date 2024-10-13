using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckerCollision
{
    public CheckerCollision(GameObject objectCollider)
    {
        _object = objectCollider;
        _objectCollider = _object.GetComponent<Collider>();
    }

    private GameObject _object;
    private Collider _objectCollider;
    public bool CheckCollisionCollider()
    {
        
        Vector3 halfCollider = _objectCollider.bounds.extents;
        Collider[] colliders = Physics.OverlapBox(_objectCollider.bounds.center, halfCollider, _object.transform.rotation);


        foreach (Collider collider in colliders)
        {
            // ��������� ����������� �� ���������
/*            Vector3 directionToCollider = (_objectCollider.bounds.center - collider.bounds.center).normalized;*/

            if (collider == _objectCollider) continue;

            if (_object.GetComponent<Cargo3DView>().IsOnlyFloor)
            {

                return (collider.gameObject.GetComponent<CargoArea>());

            }
            else
            {
                if (collider.gameObject.GetComponent<CargoArea>()) return true;

                if (collider.gameObject.GetComponent<Cargo3DView>().IsTiering)
                {
                    return IsTopCollision(collider);
                }
                else return false;

            }
        }
        return false;
    }

    // �������� �� ������� �����������
    private bool IsTopCollision(Collider collider)
    {
        float objectBottom = _objectCollider.bounds.min.y; // ������ ����� ���������������� �������
        float objectTop = _objectCollider.bounds.max.y;   // ������� ����� ���������������� �������

        // ������ ������� ����������
        float otherBottom = collider.bounds.min.y; // ������ ����� ������� ����������
        float otherTop = collider.bounds.max.y;     // ������� ����� ������� ����������

        Debug.Log(collider.name + " ���������: " + objectBottom + " - " + otherTop + " && " + objectTop + " - " + otherBottom);
        
        return objectBottom >= otherTop && objectTop >= otherBottom;
    }
    // �������� �� ������� �����������
    private bool IsSideCollision(Vector3 directionToCollider)
    {
        // ���� ����������� � ������� �������� (�������� �� ��� X ��� Z)
        return Mathf.Abs(directionToCollider.x) > 0.9f || Mathf.Abs(directionToCollider.z) > 0.9f;
    }
}
