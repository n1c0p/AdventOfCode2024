
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.BusinessLayer.Service;
public class DayThreeService : IDayThreeService
{
    public async Task<Result<GenericResponse<int, int>>> DayThreeAsync()
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
        var stringPattern = @"[mul]{3,3}[(][\d]*[,][\d]*[)]";
        var numberPattern = @"[\d+]{1,9999}";

        var input = DayThreeInput.input;

        Regex regexStringPattern = new Regex(stringPattern);
        Regex regexNumberPatter = new Regex(numberPattern);
        MatchCollection matched = regexStringPattern.Matches(input);

        var sum = 0;

        foreach (Match match in matched) 
        {
            MatchCollection matchValue = regexNumberPatter.Matches(match.Value);
            var arr = matchValue.Select(p => Convert.ToInt32(p.Value)).ToArray();
            sum += (arr.First() * arr.Last());
        }

        return sum;
    }

    public async Task<int> PartTwoAsync()
    {
        var pattern = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";

        var input = DayThreeInput.input_partTwo;

        Regex regexStringPattern = new Regex(pattern);
        MatchCollection matched = regexStringPattern.Matches(input);

        int totalSum = 0;
        // Stato iniziale: le moltiplicazioni sono abilitate
        bool isEnabled = true;
        foreach (Match match in matched)
        {
            // Se è una moltiplicazione
            if (match.Value.StartsWith("mul"))
            {
                if (isEnabled)
                {
                    // Estrai i numeri X e Y dalla corrispondenza
                    int x = int.Parse(match.Groups[1].Value);
                    int y = int.Parse(match.Groups[2].Value);
                    // Calcola la moltiplicazione e aggiungi il risultato alla somma totale
                    totalSum += (int)x * y;
                }
            }
            // Se è un "do()" cambia lo stato a abilitato
            else if (match.Value == "do()")
            {
                isEnabled = true;
            }
            // Se è un "don't()" cambia lo stato a disabilitato
            else if (match.Value == "don't()")
            {
                isEnabled = false;
            }
        }

        return totalSum;
    }
}
