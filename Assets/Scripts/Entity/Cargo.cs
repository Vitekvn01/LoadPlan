using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    private float _length;
    public float Length => _length;

    private float _width;
    public float Width => _width;

    private float _height;
    public float Height => _height;

    private float _weight;
    public float Weight => _weight;

    private bool _isTiering;
    public bool IsTiering => _isTiering;

    private bool _isOnlyFloor;
    public bool IsOnlyFloor => _isOnlyFloor;
    public Cargo(float length, float width, float height, float weight, bool isTiering, bool isOnlyFloor)
    {
        _length = length;
        _width = width;
        _height = height;
        _weight = weight;

        _isTiering = isTiering;
        _isOnlyFloor = isOnlyFloor;
    }
}