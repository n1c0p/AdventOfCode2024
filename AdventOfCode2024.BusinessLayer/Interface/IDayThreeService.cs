namespace AdventOfCode2024.BusinessLayer.Interface;
public interface IDayThreeService : ICommonService<int, int>
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}
