namespace AdventOfCode2024.BusinessLayer.Interface;
public interface IDayFiveService : ICommonService<(int Result, List<List<int>> Incorrects, Dictionary<int, List<int>> Before), int>
{
    Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync();
}
