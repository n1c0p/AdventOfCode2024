namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IAdventOfCode2024Service
{
    Task<Result<GenericResponse<int, int>>> DayOneAsync();
}
