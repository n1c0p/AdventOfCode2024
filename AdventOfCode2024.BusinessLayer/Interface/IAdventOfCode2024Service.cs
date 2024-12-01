using AdventOfCode2024.SharedLayer.Dto;
using OperationResults;

namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IAdventOfCode2024Service
{
    Task<Result<GenericResponse<int, int>>> DayOneAsync();
}
