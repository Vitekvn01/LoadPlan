using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor3DController : SingletonBase<Cursor3DController>
{

    private IMovable _movableObject;
    private RaycastLogic _raycastLogic;
    public event Action OnPlacedEvent;

    protected override void Awake()
    {
        base.Awake();
        InitRaycastLogic();
    }

    private void Update()
    {
        _raycastLogic.RaycastPosition();
        if (_movableObject == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetIMovable();
            }
        }
        else
        {
            CargoMove();

            if (Input.GetMouseButtonDown(0))
            {
                Place();
            }
        }
    }

    private void GetIMovable()
    {
        _raycastLogic.CheckClickIMovable(out _movableObject); 
    }

    public void GetIMovable(IMovable movableObject)
    {
        _movableObject = movableObject;
        _movableObject.StartMoving();
    }

    private void CargoMove()
    {
/*            Debug.Log("CargoMove");*/
            _movableObject.Move(_raycastLogic.Hit);
    }

    private void Place()
    {
        if (_movableObject.IsCanPlace())
        {
            _movableObject.StopMoving();
            _movableObject = null;
            OnPlacedEvent?.Invoke();
        }
    }

    private void InitRaycastLogic()
    {
        _raycastLogic = new RaycastLogic();
    }
}
