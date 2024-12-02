namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayOneService
{
    Task<Result<GenericResponse<int, int>>> DayOneAsync();
    Task<int> PartOneAsync();
    Task<int> PartTwoAsync();
}