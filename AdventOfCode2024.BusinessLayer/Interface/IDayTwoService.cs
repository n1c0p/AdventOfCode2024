namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayTwoService : ICommonService
{
    Task<Result<GenericResponse<int, int>>> DayTwoAsync();
}