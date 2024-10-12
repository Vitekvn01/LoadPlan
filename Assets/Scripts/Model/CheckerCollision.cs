using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckerCollision
{
    public CheckerCollision(GameObject objectCollider)
    {
        _objectCollider = objectCollider;
    }

    private GameObject _objectCollider;


    /*    public bool CheckCollisionCollider()
        {
            // �������� ��������� �������
            Collider objectCollider = _objectCollider.GetComponent<Collider>();
            Vector3 halfCollider = objectCollider.bounds.extents;

            // ��������� ����������� ����� OverlapBox
            Collider[] colliders = Physics.OverlapBox(objectCollider.bounds.center, halfCollider, _objectCollider.transform.rotation);

            bool isOnValidSurface = false; // ���� ��� �������� ��������� �� ���������� ����������� (CargoArea)

            foreach (Collider collider in colliders)
            {
                // ���������� ����������� ���������
                if (collider == objectCollider) continue;

                // �������� ��� "������ ����" (IsOnlyFloor)
                if (_objectCollider.GetComponent<Cargo3DView>().IsOnlyFloor)
                {
                    // ���� ������ ������������ � CargoArea, ��� ���������
                    if (collider.gameObject.GetComponent<CargoArea>() != null)
                    {
                        return true;
                    }
                    // ���� ����������� �� � CargoArea � ��������� �����������
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // ���������, ������������ �� ������ � ���������� ������������ (CargoArea)
                    if (collider.gameObject.GetComponent<CargoArea>() != null)
                    {
                        isOnValidSurface = true; // ���������� �� ���������� �����������
                    }

                    // ���� ������ ��������� �� ���������� �����������, ��������� ���������� �����������
                    if (isOnValidSurface)
                    {
                        // ���� ����������� � ������ ������ � IsTiering, ��������� ���������
                        if (collider.gameObject.GetComponent<Cargo3DView>()?.IsTiering == true)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // ���� ������ �� ����� �� ���������� ����������� � ������������ � ������ ������
                        if (collider.gameObject.GetComponent<Cargo3DView>()?.IsTiering == true)
                        {
                            return true; // ��������� ���������� �� ������ ���� � IsTiering
                        }
                    }
                }
            }

            // ���� ����������� ��� ��� ������ ����� �� ���������� ����������� ��� ����������, ���������� true
            return isOnValidSurface;

        }*/
    /*public bool CheckCollisionCollider()
    {
        Collider objectCollider = _objectCollider.GetComponent<Collider>();
        Vector3 halfCollider = objectCollider.bounds.extents;

        Collider[] colliders = Physics.OverlapBox(objectCollider.bounds.center, halfCollider, _objectCollider.transform.rotation);


        foreach (Collider collider in colliders)
        {
            if (collider == objectCollider) continue;

            if (_objectCollider.GetComponent<Cargo3DView>().IsOnlyFloor)
            {
                if (collider.gameObject.GetComponent<CargoArea>() != null)
                {
                    return true;
                }
                else return false;
            }
            else
            {
                if (collider.gameObject.GetComponent<Cargo3DView>()?.IsTiering == true && collider.gameObject.GetComponent<CargoArea>() == null || collider.gameObject.GetComponent<CargoArea>() != null)
                {
                    return true;
                }
                else return false;
            }
        }

        return false;
    }*/

    public bool CheckCollisionCollider(RaycastHit hit)
    {
        Collider hitCollider = hit.collider;
        Collider objectCollider = _objectCollider.GetComponent<Collider>();
        Vector3 halfCollider = objectCollider.bounds.extents;

        Collider[] colliders = Physics.OverlapBox(objectCollider.bounds.center, halfCollider, _objectCollider.transform.rotation);


        foreach (Collider collider in colliders)
        {
            if (collider == objectCollider) continue;

            if (_objectCollider.GetComponent<Cargo3DView>().IsOnlyFloor)
            {
                if (collider.gameObject.GetComponent<CargoArea>() != null)
                {
                    return true;
                }

                return false;
            }
            else
            {
                if (hitCollider.gameObject.GetComponent<Cargo3DView>() == collider.gameObject.GetComponent<Cargo3DView>() && collider.gameObject.GetComponent<CargoArea>() == null)
                {
                    if (hitCollider.gameObject.GetComponent<Cargo3DView>() == collider.gameObject.GetComponent<Cargo3DView>().IsTiering)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    Debug.Log(hitCollider.name + "==" + collider.name);
                    if (hitCollider.gameObject.GetComponent<CargoArea>() == collider.gameObject.GetComponent<CargoArea>() && collider.gameObject.GetComponent<Cargo3DView>() == null)
                    {
                        return true;
                    }
                    return false;
                }
            }
        }

        return false;
    }
}
