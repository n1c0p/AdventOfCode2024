namespace AdventOfCode2024.BusinessLayer.Interface;

public interface IDayOneService : ICommonService<int,int>
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}