namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayOneService : ICommonService
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}