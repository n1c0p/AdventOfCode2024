namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayFourService : ICommonService
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}
