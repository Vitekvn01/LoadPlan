using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo
{
    public float Length { get; private set; }
    public float Width { get; private set; }
    public float Height { get; private set; }
    public float Weight { get; private set; }
    public string Name { get; private set; }

    public bool IsTiering { get; private set; }
    public bool IsOnlyFloor { get; private set; }

    private Cargo3DView _cargo3DView ;

    public Cargo(float length, float width, float height, float weight, string name, bool isTiering, bool isOnlyFloor)
    {
        Length = length;
        Width = width;
        Height = height;
        Weight = weight;
        Name = name;
        IsTiering = isTiering;
        IsOnlyFloor = isOnlyFloor;
    }

    public void Delete3DView()
    {
        if (_cargo3DView != null)
        {
            _cargo3DView.Delete();
        }
    }

    public void SetCargo3DView(Cargo3DView view)
    {
        _cargo3DView = view;
    }
}