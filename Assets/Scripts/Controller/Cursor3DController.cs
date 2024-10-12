using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3DController : SingletonBase<Cursor3DController>
{

    private IMovable movableObject;
    private RaycastLogic _raycastLogic;

    protected override void Awake()
    {
        base.Awake();
        InitRaycastLogic();
    }

    private void Update()
    {
        _raycastLogic.RaycastPosition();



        if (movableObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _raycastLogic.CheckClickIMovable(out movableObject);
            }
        }
        else
        {


            movableObject.Move(_raycastLogic.Hit);

            if (Input.GetMouseButtonDown(0))
            {
                movableObject.StopMoving(_raycastLogic.Hit);
                movableObject = null;
            }
        }

    }

    private void InitRaycastLogic()
    {
        _raycastLogic = new RaycastLogic();
    }


}
