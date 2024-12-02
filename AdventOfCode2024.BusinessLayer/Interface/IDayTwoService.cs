namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayTwoService
{
    Task<Result<GenericResponse<int, int>>> DayTwoAsync();
    Task<int> PartOneAsync();
    Task<int> PartTwoAsync();
}