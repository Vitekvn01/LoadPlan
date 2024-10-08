using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    public void StartMoving();
    public void Move(Vector3 hitPos);
    public void StopMoving();
    public void OnDrop();
}
