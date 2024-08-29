using System.Collections.Generic;
using UnityEngine;

public class Autoload : MonoBehaviour
{
    [SerializeField] private float X;
    [SerializeField] private float Y;
    [SerializeField] private float Z;

    private CargoHold cargoHold; // Грузовой отсек для управления грузами
    [SerializeField] private List<GameObject> cargos;

    private void Start()
    {
        cargoHold = new CargoHold(new Vector3(X, Y, Z), 50000); // Инициализация грузового отсека с максимальной полезной нагрузкой в 50 тонн
        PlaceCargos();
    }

    private void PlaceCargos()
    {
        Debug.Log("Начало размещения грузов.");

        // Сортировка грузов по убыванию объема
        cargos.Sort((a, b) => (b.transform.localScale.x * b.transform.localScale.y * b.transform.localScale.z)
            .CompareTo(a.transform.localScale.x * a.transform.localScale.y * a.transform.localScale.z));
        Debug.Log("Грузы отсортированы по убыванию объема.");

        foreach (var cargoObject in cargos)
        {
            bool placed = false;
            Vector3 cargoSize = cargoObject.transform.localScale;
            float cargoWeight = cargoObject.GetComponent<Cargo>().Weight; // Получаем вес груза из компонента

            Debug.Log($"Попытка разместить груз: {cargoObject.name} с размерами {cargoSize} и весом {cargoWeight}");

            for (int x = 0; x <= X - cargoSize.x; x += (int)cargoSize.x)
            {
                for (int y = 0; y <= Y - cargoSize.y; y += (int)cargoSize.y)
                {
                    for (int z = 0; z <= Z - cargoSize.z; z += (int)cargoSize.z)
                    {
                        Vector3 position = new Vector3(x, y, z);

                        // Проверяем, можно ли разместить груз
                        if (cargoHold.CanPlaceCargo(cargoObject, position))
                        {
                            cargoHold.AddCargo(cargoObject, position);
                            placed = true;
                            Debug.Log($"Груз {cargoObject.name} успешно размещен на позиции {position}");
                            break;
                        }
                        else
                        {
                            Debug.Log($"Не удалось разместить груз {cargoObject.name} на позиции {position}");
                        }
                    }
                    if (placed) break;
                }
                if (placed) break;
            }

            if (!placed)
            {
                Debug.LogWarning($"Не удалось разместить груз: {cargoObject.name}");
            }
        }

        Debug.Log("Завершение размещения грузов.");
    }
}

public class CargoHold
{
    public Vector3 Dimensions { get; private set; }
    public float MaxPayload { get; private set; }
    public float CurrentWeight { get; private set; }
    public Vector3 CenterOfGravity { get; private set; } = new Vector3(13, 0, 0);

    private List<PlacedCargo> placedCargos = new List<PlacedCargo>();

    public CargoHold(Vector3 dimensions, float maxPayload)
    {
        Dimensions = dimensions;
        MaxPayload = maxPayload;
    }

    public void AddCargo(GameObject cargo, Vector3 position)
    {
        Vector3 size = cargo.transform.localScale;
        float weight = cargo.GetComponent<Cargo>().Weight;
        CurrentWeight += weight;

        UpdateCenterOfGravity(size, position, weight);

        PlacedCargo newCargo = new PlacedCargo(position, size, weight);
        placedCargos.Add(newCargo);

        PlaceCargoInScene(cargo, position, size);
    }

    private void UpdateCenterOfGravity(Vector3 size, Vector3 position, float weight)
    {
        float newCenterOfGravityX = 
            (CurrentWeight * CenterOfGravity.x + weight * position.x) 
            / (CurrentWeight + weight);

        CenterOfGravity = new Vector3(newCenterOfGravityX, CenterOfGravity.y, CenterOfGravity.z);
        Debug.Log($"Обновленный центр тяжести: {CenterOfGravity.x}");
    }

    public bool CanPlaceCargo(GameObject cargo, Vector3 position)
    {
        Vector3 size = cargo.transform.localScale;

        if (position.x < 0 || position.y < 0 || position.z < 0 ||
            position.x + size.x > Dimensions.x ||
            position.y + size.y > Dimensions.y ||
            position.z + size.z > Dimensions.z)
        {
            return false;
        }

        foreach (var placedCargo in placedCargos)
        {
            if (Intersect(position, size, placedCargo.Position, placedCargo.Size))
            {
                return false;
            }
        }

        return true;
    }

    private void PlaceCargoInScene(GameObject cargoObject, Vector3 position, Vector3 size)
    {
        GameObject placedCargo = Instantiate(cargoObject);
        placedCargo.transform.position = position + size / 2; // Центрирование груза на позиции
        placedCargo.transform.localScale = size;
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
}

public class PlacedCargo
{
    public Vector3 Position { get; }
    public Vector3 Size { get; }
    public float Weight { get; }

    public PlacedCargo(Vector3 position, Vector3 size, float weight)
    {
        Position = position;
        Size = size;
        Weight = weight;
    }
}

public class Cargo : MonoBehaviour
{
    public float Weight;
}
