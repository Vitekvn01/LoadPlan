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
        Debug.Log(_object.GetComponent<Collider>());
    }

    private GameObject _object;
    private Collider _objectCollider;
    public bool CheckCollisionCollider()
    {
        
        Vector3 halfCollider = _objectCollider.bounds.extents;
        Collider[] colliders = Physics.OverlapBox(_objectCollider.bounds.center, halfCollider, _object.transform.rotation);


        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);
            if (collider.name == _objectCollider.name) continue;

            if (_object.GetComponent<Cargo3DView>().IsOnlyFloor)
            {

                return (collider.gameObject.GetComponent<CargoArea>());

            }
            else
            {
                if (collider.gameObject.GetComponent<CargoArea>()) return true;

                if (collider.gameObject.GetComponent<Cargo3DView>().IsTiering)
                {
                    Debug.Log(collider.name + " IsTiering");
                    return IsTopCollision(collider);
                }
                else return false;

            }
        }
        return false;
    }

    // Проверка на верхнюю поверхность
    private bool IsTopCollision(Collider collider)
    {
        float objectBottom = _objectCollider.bounds.min.y; // Нижняя грань устанавливаемого объекта
        float objectTop = _objectCollider.bounds.max.y;   // Верхняя грань устанавливаемого объекта

        // Высота другого коллайдера
        float otherBottom = collider.bounds.min.y; // Нижняя грань другого коллайдера
        float otherTop = collider.bounds.max.y;     // Верхняя грань другого коллайдера

/*        Debug.Log(collider.name + " Коллайдер: " + objectBottom + " - " + otherTop + " && " + objectTop + " - " + otherBottom);*/
        
        return objectBottom >= otherTop && objectTop >= otherBottom;
    }
}
