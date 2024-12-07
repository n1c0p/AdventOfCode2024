namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayFourService : ICommonService<int, int>
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}
