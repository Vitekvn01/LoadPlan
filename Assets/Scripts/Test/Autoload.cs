using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Autoload : MonoBehaviour
{
    [SerializeField] private float X;
    [SerializeField] private float Y;
    [SerializeField] private float Z;

    private Container container;
    [SerializeField] private List<GameObject> cargos;

    private void Start()
    {
        PlaceCargos();
    }

    private void PlaceCargos()
    {
        Debug.Log("Начало размещения грузов.");

        // Сортировка грузов по убыванию объема
        cargos.Sort((a, b) => (b.transform.localScale.x * b.transform.localScale.y * b.transform.localScale.z)
            .CompareTo(a.transform.localScale.x * a.transform.localScale.y * a.transform.localScale.z));
        Debug.Log("Грузы отсортированы по убыванию объема.");

        // Размеры контейнера
        Vector3 containerSize = new Vector3(X, Y, Z);
        Debug.Log($"Размеры контейнера: {containerSize}");

        List<PlacedCargo> placedCargos = new List<PlacedCargo>();

        foreach (var cargo in cargos)
        {
            bool placed = false;
            Vector3 cargoSize = cargo.transform.localScale;

            Debug.Log($"Попытка разместить груз: {cargo.name} с размером {cargoSize}");

            // Попробуйте поместить груз в контейнер
            for (int x = 0; x <= containerSize.x - cargoSize.x; x += (int)cargoSize.x)
            {
                for (int y = 0; y <= containerSize.y - cargoSize.y; y += (int)cargoSize.y)
                {
                    for (int z = 0; z <= containerSize.z - cargoSize.z; z += (int)cargoSize.z)
                    {
                        Vector3 position = new Vector3(x, y, z);

                        // Проверка, чтобы груз поместился
                        if (CanPlaceCargo(position, cargoSize, placedCargos, containerSize))
                        {
                            PlaceCargo(position, cargo, cargoSize);
                            placedCargos.Add(new PlacedCargo(position, cargoSize));
                            placed = true;
                            Debug.Log($"Груз {cargo.name} успешно размещен на позиции {position}");
                            break;
                        }
                        else
                        {
                            Debug.Log($"Невозможно разместить груз {cargo.name} на позиции {position}");
                        }
                    }
                    if (placed) break;
                }
                if (placed) break;
            }

            if (!placed)
            {
                Debug.LogWarning($"Не удалось поместить груз: {cargo.name}");
            }
        }

        Debug.Log("Завершение размещения грузов.");
    }

    private bool CanPlaceCargo(Vector3 position, Vector3 size, List<PlacedCargo> placedCargos, Vector3 containerSize)
    {
        Debug.Log($"Проверка возможности размещения груза в позиции {position} с размером {size}");

        // Проверка, что груз помещается в контейнере
        if (position.x < 0 || position.y < 0 || position.z < 0 ||
            position.x + size.x > containerSize.x ||
            position.y + size.y > containerSize.y ||
            position.z + size.z > containerSize.z)
        {
            Debug.Log($"Груз выходит за пределы контейнера: {position} + {size} (контейнер: {containerSize})");
            return false;
        }

        // Проверка пересечения с уже размещенными грузами
        foreach (var placedCargo in placedCargos)
        {
            if (Intersect(position, size, placedCargo.Position, placedCargo.Size)) // Используем размер текущего груза
            {
                Debug.Log($"Пересечение с уже размещенным грузом в позиции {placedCargo.Position} с размером {placedCargo.Size}");
                return false;
            }
        }

        return true;
    }

    private void PlaceCargo(Vector3 position, GameObject cargoObject, Vector3 size)
    {
        // Создание нового экземпляра груза
        GameObject placedCargo = Instantiate(cargoObject);
        placedCargo.transform.position = position + size / 2; // Установка центра объекта на позицию
        placedCargo.transform.localScale = size;
        Debug.Log($"Размещен груз: {placedCargo.name} на позиции {position} с размером {size}");
    }

    private bool Intersect(Vector3 pos1, Vector3 size1, Vector3 pos2, Vector3 size2)
    {
        return !(pos1.x + size1.x <= pos2.x ||
                 pos2.x + size2.x <= pos1.x ||
                 pos1.y + size1.y <= pos2.y ||
                 pos2.y + size2.y <= pos1.y ||
                 pos1.z + size1.z <= pos2.z ||
                 pos2.z + size2.z <= pos1.z);
    }

    // Класс для хранения информации о размещенных грузах
    private class PlacedCargo
    {
        public Vector3 Position { get; }
        public Vector3 Size { get; }

        public PlacedCargo(Vector3 position, Vector3 size)
        {
            Position = position;
            Size = size;
        }
    }
}


