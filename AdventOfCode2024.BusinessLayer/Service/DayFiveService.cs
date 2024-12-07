namespace AdventOfCode2024.BusinessLayer.Service;
public class DayFiveService : IDayFiveService
{
    public async Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync()
    {
        var partOne = await PartOneAsync();
        var partTwo = await PartTwoAsync();

        var result = new GenericResponse<int, int>
        {
            PartOne = partOne.Result,
            PartTwo = partTwo
        };

        return result;
    }

    public async Task<(int Result, List<List<int>> Incorrects, Dictionary<int, List<int>> Before)> PartOneAsync()
    {
        await Task.CompletedTask;
        int result = 0;

        var parts = DayFiveInput.input.Split("\r\n\r\n");
        var rules = parts.First().Split("\r\n").Select(number => (Convert.ToInt32(number.Split("|").First()), Convert.ToInt32(number.Split("|").Last()))).ToList();
        var updates = parts.Last().Split("\r\n").Select(number => number.Split(",").Select(int.Parse).ToList());

        var pages = rules.Select(x => x.Item1).Union(rules.Select(x => x.Item2)).Distinct().ToList();
        var after = new Dictionary<int, List<int>>();
        var before = new Dictionary<int, List<int>>();

        foreach (var page in pages)
        {
            before[page] = [];
            after[page] = [];

            foreach (var rule in rules.Where(rule => rule.Item1 == page))
            {
                after[page].Add(rule.Item2);
            }

            foreach (var rule in rules.Where(rule => rule.Item2 == page))
            {
                before[page].Add(rule.Item1);
            }
        }

        var incorrects = new List<List<int>>();

        foreach (var update in updates) 
        {
            var correct = true;

            for (var index = 0; index < update.Count; index++)
            {
                if (before[update[index]].Intersect(update.Skip(index + 1)).Any())
                {
                    correct = false;
                    break;
                }

                if (after[update[index]].Intersect(update.Take(index)).Any())
                {
                    correct = false;
                    break;
                }
            }

            if (correct)
            {
                result += update[update.Count / 2];
            }
            else
            {
                incorrects.Add(update);
            }
        }

        return (Result: result, Incorrects: incorrects, Before: before);
    }

    public async Task<int> PartTwoAsync()
    {
        await Task.CompletedTask;
        var result = 0;

        var partOneResult = await PartOneAsync();
        var incorrects = partOneResult.Incorrects;
        var before = partOneResult.Before;

        foreach (var incorrect in incorrects)
        {
            for (var i = 0; i < incorrect.Count; i++)
            {
                for (var j = i +1; j < incorrect.Count; j++)
                {
                    if (before[incorrect[i]].Contains(incorrect[j]))
                    {
                        (incorrect[i], incorrect[j]) = (incorrect[j], incorrect[i]);
                    }
                }
            }

            result += incorrect[incorrect.Count / 2];
        }


        return result;
    }

    
}
