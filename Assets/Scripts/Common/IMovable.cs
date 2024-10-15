using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void StartMoving();
    public void Move(RaycastHit hit);
    public void StopMoving();
    public void OnDrop();

    public bool IsCanPlace();
}
