using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

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
        //List<string> matrixString = DayFourInput.input.Replace("\r", "").Split("\n").ToList();

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
            MatchCollection matchedReverse = regexPatternResultReverse.Matches(stringComparision);

            verticalReverseCounter += matchedReverse.Count();

        }

        var totalCounter = verticalCounter + verticalReverseCounter;
        return totalCounter;
    }

    private async Task<int> MatchStringOblique(List<List<string>> matrix, string patternVertical, string patternReverseVertical)
    {
        var regexPatternVertical = $@"{patternVertical}";
        var regexPatternReverseVertical = $@"{patternReverseVertical}";

        // sx -> dx
        
        var counter = await Processing(matrix, patternVertical, regexPatternVertical, regexPatternReverseVertical);

        // dx -> sx
        matrix.ForEach(x =>
        {
            x.Reverse();
        });

        var reverseCounter = await Processing(matrix, patternVertical, regexPatternVertical, regexPatternReverseVertical);

        var totalCounter = counter + reverseCounter;
        return totalCounter;
    }

    private async Task<int> Processing(List<List<string>> matrix, string patternVertical, string regexPatternVertical, string regexPatternReverseVertical)
    {
        await Task.CompletedTask;

        var counter = 0;
        var reverseCounter = 0;
        var inc = 0;


        // bottom
        for (int orizontalIndex = 0; orizontalIndex < matrix.Count(); orizontalIndex++)
        {
            var stringComparision = string.Empty;
            var stringComparisionTop = string.Empty;

            var matrixCount = matrix[orizontalIndex].Count();

            for (int verticalIndex = 0; verticalIndex < matrixCount; verticalIndex++)
            {
                if ((matrixCount - inc) > verticalIndex)
                {
                    stringComparision += $"{matrix[orizontalIndex + verticalIndex][verticalIndex]}";
                }
            }

            if (stringComparision.Length < patternVertical.Length)
            {
                break;
            }

            inc++;

            Regex regexPatternResult = new Regex(regexPatternVertical);
            MatchCollection matched = regexPatternResult.Matches(stringComparision);

            counter += matched.Count();

            Regex regexPatternResultReverse = new Regex(regexPatternReverseVertical);
            MatchCollection matchedReverse = regexPatternResultReverse.Matches(stringComparision);

            reverseCounter += matchedReverse.Count();
        }

        // top
        inc = 0;
        for (int orizontalIndex = 0; orizontalIndex < matrix.Count(); orizontalIndex++)
        {
            var stringComparision = string.Empty;
            var stringComparisionTop = string.Empty;

            var matrixCount = matrix[orizontalIndex].Count();
            

            for (int verticalIndex = 1; verticalIndex < matrixCount; verticalIndex++)
            {
                if ((matrixCount - inc) > verticalIndex)
                {
                    stringComparision += $"{matrix[orizontalIndex][verticalIndex]}";
                    orizontalIndex++;
                }
            }

            if (stringComparision.Length < patternVertical.Length)
            {
                break;
            }

            inc++;

            Regex regexPatternResult = new Regex(regexPatternVertical);
            MatchCollection matched = regexPatternResult.Matches(stringComparision);

            counter += matched.Count();

            Regex regexPatternResultReverse = new Regex(regexPatternReverseVertical);
            MatchCollection matchedReverse = regexPatternResultReverse.Matches(stringComparision);

            reverseCounter += matchedReverse.Count();
        }
        return counter + reverseCounter;
    }
}
