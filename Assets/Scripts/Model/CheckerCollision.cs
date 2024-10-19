using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckerCollision
{
    public CheckerCollision(Cargo3DView cargoView)
    {
        _object = cargoView;
        _objectCollider = _object.gameObject.GetComponent<Collider>();
    }
    private Collider _objectCollider;
    private Cargo3DView _object;
    public bool CheckCollisionCollider(Collider collider)
    {
        if (_object.IsOnlyFloor)
        {
            if (collider.gameObject.GetComponent<CargoArea>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Объект может стоять на CargoArea или на ярус выше
            if (collider.gameObject.GetComponent<CargoArea>() != null)
            {
                return true;
            }
            else if (collider.gameObject.GetComponent<Cargo3DView>().IsTiering)
            {
                Debug.Log(collider.name + " IsTiering");
                return IsTopCollision(collider);
            }
            else
            {
                return false;
            }
        }
    }

    // Проверка на верхнюю поверхность
    public bool IsTopCollision(Collider collider)
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
