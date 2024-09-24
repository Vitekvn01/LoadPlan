public class CargoManager : MonoBehaviour
{
    public Vector3 Dimensions; // Размеры грузового отсека
    public List<PlacedCargo> placedCargos = new List<PlacedCargo>();
    public float MaxVolume = 321f; // Максимальный объем грузового отсека (м³)
    public float MaxWeight = 50000f; // Максимальная допустимая масса (кг)

    public float MinCenterOfGravity = 11.65f; // Минимальное значение центра тяжести (м)
    public float MaxCenterOfGravity = 18.64f; // Максимальное значение центра тяжести (м)

    public float MaxMomentOfForce = 5000f; // Максимальный момент силы для устойчивости (Н·м)
    public float AirDensity = 1.225f; // Плотность воздуха (кг/м³)
    public float Speed = 50f; // Скорость груза (м/с)
    public float DragCoefficient = 0.3f; // Коэффициент лобового сопротивления

    public bool CanPlaceCargo(GameObject cargo, Vector3 position)
    {
        Vector3 size = cargo.transform.localScale;
        float cargoVolume = size.x * size.y * size.z;
        float cargoWeight = cargo.GetComponent<Cargo>().Weight;
        float crossSectionalArea = size.x * size.z; // Площадь поперечного сечения

        // Проверка на выход за границы грузового отсека
        if (position.x < 0  position.y < 0  position.z < 0 ||
            position.x + size.x > Dimensions.x ||
            position.y + size.y > Dimensions.y ||
            position.z + size.z > Dimensions.z)
        {
            return false;
        }

        // Проверка на пересечение с уже размещенными грузами
        foreach (var placedCargo in placedCargos)
        {
            if (Intersect(position, size, placedCargo.Position, placedCargo.Size))
            {
                return false;
            }
        }

        // Подсчет общего объема и массы всех размещенных грузов
        float totalVolume = cargoVolume;
        float totalWeight = cargoWeight;
        Vector3 totalCenterOfGravity = position * cargoWeight; // Центр тяжести умножаем на массу

        foreach (var placedCargo in placedCargos)
        {
            totalVolume += placedCargo.Size.x * placedCargo.Size.y * placedCargo.Size.z;
            totalWeight += placedCargo.Weight;

            // Вклад каждого груза в общий центр тяжести
            totalCenterOfGravity += placedCargo.Position * placedCargo.Weight;
        }

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
        float momentOfForce = cargoWeight * (position.y); // Расстояние до центра тяжести - по оси Y
        if (momentOfForce > MaxMomentOfForce)
        {
            return false;
        }

        // Проверка лобового сопротивления
        float dragForce = 0.5f * DragCoefficient * AirDensity * Speed * Speed * crossSectionalArea;
        if (dragForce > MaxMomentOfForce) // Можно сравнивать с моментом силы или другим критерием
        {
            return false;
        }

        return true;
    }

    public void PlaceCargoInScene(GameObject cargoObject, Vector3 position, Vector3 size)
    {
        GameObject placedCargo = Instantiate(cargoObject);
        placedCargo.transform.position = position + size / 2;
        placedCargo.transform.localScale = size;

        // Добавление груза в список размещенных
        placedCargos.Add(new PlacedCargo(position, size, cargoObject.GetComponent<Cargo>().Weight));
    }
