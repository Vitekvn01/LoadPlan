using System.Collections.Generic;
using UnityEngine;

public class CargoManager : MonoBehaviour
{
    public Vector3 Dimensions; // Размеры грузового отсека
    public List<PlacedCargo> placedCargos = new List<PlacedCargo>();
    public float MaxVolume = 321f; // Максимальный объем грузового отсека (м³)
    public float MaxWeight = 50000f; // Максимальная допустимая масса (кг)

    public float MinCenterOfGravity = 11.65f; // Минимальное значение центра тяжести (м)
    public float MaxCenterOfGravity = 18.64f; // Максимальное значение центра тяжести (м)

    public float MaxMomentOfForce = 5000f; // Максимальный момент силы для устойчивости (Н·м)

    public bool CanPlaceCargoList(List<Cargo> cargoList, Vector3 startPosition)
    {
        float totalVolume = 0;
        float totalWeight = 0;
        Vector3 totalCenterOfGravity = Vector3.zero;

        // Проход по каждому грузу в списке
        foreach (var cargo in cargoList)
        {
            Vector3 size = new Vector3(cargo.Length, cargo.Height, cargo.Width);
            float cargoVolume = size.x * size.y * size.z;
            float cargoWeight = cargo.Weight;

            // Проверка на выход за границы грузового отсека
            if (startPosition.x < 0 || startPosition.y < 0 || startPosition.z < 0 ||
                startPosition.x + size.x > Dimensions.x ||
                startPosition.y + size.y > Dimensions.y ||
                startPosition.z + size.z > Dimensions.z)
            {
                return false;
            }

            // Проверка на пересечение с уже размещенными грузами
            foreach (var placedCargo in placedCargos)
            {
                if (Intersect(startPosition, size, placedCargo.Position, placedCargo.Size))
                {
                    return false;
                }
            }

            // Добавление массы и объема груза
            totalVolume += cargoVolume;
            totalWeight += cargoWeight;
            totalCenterOfGravity += startPosition * cargoWeight; // Центр тяжести умножаем на массу

            // Проверка, не превышает ли общий объем и масса допустимые значения
            if (totalVolume > MaxVolume || totalWeight > MaxWeight)
            {
                return false;
            }

            // Вычисление нового центра тяжести
            Vector3 newCenterOfGravity = totalCenterOfGravity / totalWeight;

            // Проверка, находится ли центр тяжести в допустимом диапазоне
            if (newCenterOfGravity.x < MinCenterOfGravity || newCenterOfGravity.x > MaxCenterOfGravity)
            {
                return false;
            }

            // Проверка устойчивости (момент силы)
            float momentOfForce = cargoWeight * startPosition.y; // Момент силы по оси Y
            if (momentOfForce > MaxMomentOfForce)
            {
                return false;
            }

            // Смещение позиции для следующего груза
            startPosition += new Vector3(0, size.y, 0); // Примерное смещение по высоте
        }

        return true;
    }

    public void PlaceCargoListInScene(List<Cargo> cargoList, Vector3 startPosition)
    {
        foreach (var cargo in cargoList)
        {
            Vector3 size = new Vector3(cargo.Length, cargo.Height, cargo.Width);
            GameObject placedCargo = Instantiate(cargo.CargoPrefab);
            placedCargo.transform.position = startPosition + size / 2;
            placedCargo.transform.localScale = size;

            placedCargos.Add(new PlacedCargo(startPosition, size, cargo.Weight));

            // Смещение позиции для следующего груза
            startPosition += new Vector3(0, size.y, 0);
        }
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

