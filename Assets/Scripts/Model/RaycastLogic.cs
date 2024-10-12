using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLogic
{
    private Ray _ray;
    private RaycastHit _hit;

    public RaycastHit Hit => _hit;

    public Vector3 HitPostion { get; private set; }
    public GameObject HitObject { get; private set; }
    public void RaycastPosition()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            HitPostion = _hit.point;
/*            Debug.Log(HitPostion);
            Debug.Log(_hit.collider.name);*/
        }
    }


    public void CheckClickIMovable(out IMovable selectedMovable)
    {
        selectedMovable = null;
        if (_hit.collider != null)
        {
            if (_hit.collider.TryGetComponent(out IMovable movableObject))
            {
                movableObject.StartMoving();
                selectedMovable = movableObject;
            }
        }
    }
}
