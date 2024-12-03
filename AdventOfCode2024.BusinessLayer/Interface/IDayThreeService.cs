namespace AdventOfCode2024.BusinessLayer.Interface;
public interface IDayThreeService : ICommonService
{
    Task<Result<GenericResponse<int, int>>> DayThreeAsync();
}
