namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayTwoService : ICommonService<int, int>
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}