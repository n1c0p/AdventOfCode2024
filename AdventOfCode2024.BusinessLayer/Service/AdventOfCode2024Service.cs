using AdventOfCode2024.BusinessLayer.Interface;
using OperationResults;

namespace AdventOfCode2024.BusinessLayer.Service;

public class AdventOfCode2024Service : IAdventOfCode2024Service
{
    public async Task<Result<int>> DayOneAsync()
    {
        await Task.CompletedTask;

        return 1;
    }
}
