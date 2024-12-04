using System.Text.RegularExpressions;

namespace AdventOfCode2024.BusinessLayer.Service;

public class DayFourService : IDayFourService

{
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
    public async Task<int> PartOneAsync()
    {
        await Task.CompletedTask;

        var input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

        List<string> matrixString = input.Replace("\r","").Split("\n").ToList();
        Regex regex = new Regex(@"\\*");

        List<List<string>> matrix = new();
        

        var matchString = "XMAS";
        var reverseMatchString = "SAMX";
        var counterMatching = 0;
        var counterReverseMatching = 0;
        foreach (var item in matrixString)
        {   
            string[] splitStringItemInput = regex.Split(item);
            var arraySplitStringItemInput = splitStringItemInput.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            matrix.AddRange(arraySplitStringItemInput);

            counterMatching += await MatchStringOrizzontal(item, matchString);
            counterReverseMatching += await MatchStringOrizzontal(item, reverseMatchString);

        }

        var countVertical = await MatchStringVertical(matrix, matchString, reverseMatchString);
        var countOblique = await MatchStringOblique(matrix, matchString, reverseMatchString);

        var totalCount = counterMatching + counterReverseMatching + countVertical + countOblique;


        return totalCount;
    }

    public async Task<int> PartTwoAsync()
    {
        await Task.CompletedTask;
        return 1;
    }

    private async Task<int> MatchStringOrizzontal(string lineFromMatrix, string pattern)
    {
        await Task.CompletedTask;
        var regexPattern = $@"{pattern}";

        Regex regexPatternResult = new Regex(regexPattern);
        MatchCollection matched = regexPatternResult.Matches(lineFromMatrix);

        var counter = matched.Count();

        return counter;
    }

    private async Task<int> MatchStringVertical(List<List<string>> matrix, string patternVertical, string patternReverseVertical)
    {
        await Task.CompletedTask;
        var regexPatternVertical = $@"{patternVertical}";
        var regexPatternReverseVertical = $@"{patternReverseVertical}";

        var verticalCounter = 0;
        var verticalReverseCounter = 0;

        for (int orizontalIndex = 0; orizontalIndex < matrix.Count(); orizontalIndex++)
        {
            var stringComparision = string.Empty;
            for (int verticalIndex = 0; verticalIndex < matrix.Count(); verticalIndex++)
            {
                stringComparision += $"{matrix[orizontalIndex][verticalIndex]}";
            }

            Regex regexPatternResult = new Regex(regexPatternVertical);
            MatchCollection matched = regexPatternResult.Matches(stringComparision);

            verticalCounter += matched.Count();

            Regex regexPatternResultReverse = new Regex(regexPatternReverseVertical);
            MatchCollection matchedReverse = regexPatternResult.Matches(stringComparision);

            verticalReverseCounter += matchedReverse.Count();

        }

        var totalCounter = verticalCounter + verticalReverseCounter;
        return totalCounter;
    }

    private async Task<int> MatchStringOblique(List<List<string>> matrix, string patternVertical, string patternReverseVertical)
    {
        await Task.CompletedTask;
        var regexPatternVertical = $@"{patternVertical}";
        var regexPatternReverseVertical = $@"{patternReverseVertical}";

        var verticalCounter = 0;
        var verticalReverseCounter = 0;

        

        // sx -> dx
        for (int orizontalIndex = 0; orizontalIndex < matrix.Count(); orizontalIndex++)
        {
            var stringComparision = string.Empty;

            var matrixCount = matrix[orizontalIndex].Count();

            for (int verticalIndex = orizontalIndex; verticalIndex < matrixCount; verticalIndex++)
            {
                if ((matrixCount - patternVertical.Length) >= orizontalIndex)
                {
                    stringComparision += $"{matrix[orizontalIndex][verticalIndex]}";
                }
                else
                {
                    break;
                }
            }

            Regex regexPatternResult = new Regex(regexPatternVertical);
            MatchCollection matched = regexPatternResult.Matches(stringComparision);

            verticalCounter += matched.Count();

            Regex regexPatternResultReverse = new Regex(regexPatternReverseVertical);
            MatchCollection matchedReverse = regexPatternResult.Matches(stringComparision);

            verticalReverseCounter += matchedReverse.Count();
        }

        



        var totalCounter = verticalCounter + verticalReverseCounter;
        return totalCounter;
    }

}
