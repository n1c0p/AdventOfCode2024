namespace AdventOfCode2024.BusinessLayer.Service;

public class DayOneService : IDayOneService
{
    #region Public method
    public async Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync()
    {
        var partOne = await PartOneAsync();
        var partTwo = await PartTwoAsync();

        var result = new GenericResponse<int, int>
        {
            PartOne = partOne,
            PartTwo = partTwo
        };

        return result;
    }
    #endregion

    #region Private method
    public async Task<int> PartOneAsync()
    {
        await Task.CompletedTask;

        var firstIdsLocationListOrderByIdLocation = DayOneInput.firstIdsLocationListExt.OrderBy(x => x).ToList();
        var secondIdsLocationListOrderByIdLocation = DayOneInput.secondIdsLocationList.OrderBy(x => x).ToList();

        var sumLocation = 0;
        for (int i = 0; i < firstIdsLocationListOrderByIdLocation.Count(); i++)
        {
            var itemFirstLocation = firstIdsLocationListOrderByIdLocation[i];
            var itemSecondLocation = secondIdsLocationListOrderByIdLocation[i];
            var different = Math.Abs(itemFirstLocation - itemSecondLocation);
            sumLocation += different;
        }

        return sumLocation;
    }

    public async Task<int> PartTwoAsync()
    {
        await Task.CompletedTask;

        var firstIdsLocationList = DayOneInput.firstIdsLocationListExt;
        var secondIdsLocationList = DayOneInput.secondIdsLocationList;

        var sumSimilarityScore = 0;
        for (int i = 0; i < firstIdsLocationList.Count(); i++)
        {
            var itemFirstLocation = firstIdsLocationList[i];
            var findingSimilarityScore = secondIdsLocationList.Count(x => x.Equals(itemFirstLocation));

            var similarityScore = itemFirstLocation * findingSimilarityScore;
            sumSimilarityScore += similarityScore;
        }

        return sumSimilarityScore;
    }
    #endregion
}
