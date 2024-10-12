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


    /*public bool CheckCollisionCollider()
    {
        Collider[] colliders = Physics.OverlapBox(_objectCollider.transform.position, _objectCollider.transform.localScale, _objectCollider.transform.rotation);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.name);

            if (collider != _objectCollider.GetComponent<Collider>() && collider.gameObject.GetComponent<CargoArea>() != null ||
                (collider != _objectCollider.GetComponent<Collider>() && collider.gameObject.GetComponent<Cargo3DView>().IsTiering
                && collider != _objectCollider.GetComponent<Cargo3DView>().IsOnlyFloor))
            {
                return true;
            }
        }

        return false;
    }*/


    public bool CheckCollisionCollider()
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
            }
            else
            {
                if (collider.gameObject.GetComponent<Cargo3DView>()?.IsTiering == true || collider.gameObject.GetComponent<CargoArea>() != null)
                {
                    return true; 
                }
            }
        }

        return false;
    }


}
