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
            // Получаем коллайдер объекта
            Collider objectCollider = _objectCollider.GetComponent<Collider>();
            Vector3 halfCollider = objectCollider.bounds.extents;

            // Проверяем пересечения через OverlapBox
            Collider[] colliders = Physics.OverlapBox(objectCollider.bounds.center, halfCollider, _objectCollider.transform.rotation);

            bool isOnValidSurface = false; // Флаг для проверки установки на допустимую поверхность (CargoArea)

            foreach (Collider collider in colliders)
            {
                // Пропускаем собственный коллайдер
                if (collider == objectCollider) continue;

                // Проверка для "только пола" (IsOnlyFloor)
                if (_objectCollider.GetComponent<Cargo3DView>().IsOnlyFloor)
                {
                    // Если объект пересекается с CargoArea, это допустимо
                    if (collider.gameObject.GetComponent<CargoArea>() != null)
                    {
                        return true;
                    }
                    // Если пересечение не с CargoArea — установка недопустима
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // Проверяем, пересекается ли объект с допустимой поверхностью (CargoArea)
                    if (collider.gameObject.GetComponent<CargoArea>() != null)
                    {
                        isOnValidSurface = true; // Установлен на допустимую поверхность
                    }

                    // Если объект находится на допустимой поверхности, проверяем дальнейшие пересечения
                    if (isOnValidSurface)
                    {
                        // Если пересечение с другим грузом с IsTiering, установка запрещена
                        if (collider.gameObject.GetComponent<Cargo3DView>()?.IsTiering == true)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        // Если объект не стоит на допустимой поверхности и пересекается с другим грузом
                        if (collider.gameObject.GetComponent<Cargo3DView>()?.IsTiering == true)
                        {
                            return true; // Разрешено установить на другой груз с IsTiering
                        }
                    }
                }
            }

            // Если пересечений нет или объект стоит на допустимой поверхности без конфликтов, возвращаем true
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
