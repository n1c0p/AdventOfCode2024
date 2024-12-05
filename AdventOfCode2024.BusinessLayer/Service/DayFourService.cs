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

        string[] lines = DayFourInput.input.Replace("\r", "").Split("\n").ToArray();

        // Creiamo la griglia
        char[,] grid = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        // Step 2: Definisci la parola da cercare
        string word = "XMAS";

        // Step 3: Conta tutte le occorrenze della parola nella griglia
        int count = 0;

        // Definizione delle direzioni: [deltaX, deltaY]
        int[][] directions = new int[][]
        {
            new int[] { 0, 1 },    // Destra
            new int[] { 0, -1 },   // Sinistra
            new int[] { 1, 0 },    // In basso
            new int[] { -1, 0 },   // In alto
            new int[] { 1, 1 },    // Diagonale basso destra
            new int[] { 1, -1 },   // Diagonale basso sinistra
            new int[] { -1, 1 },   // Diagonale alto destra
            new int[] { -1, -1 }   // Diagonale alto sinistra
        };

        // Step 4: Scorriamo ogni cella della griglia
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                // Per ogni cella, verifica se possiamo trovare la parola in una delle 8 direzioni
                foreach (var direction in directions)
                {
                    if (IsWordPresent(grid, i, j, word, direction[0], direction[1]))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public async Task<int> PartTwoAsync()
    {
        await Task.CompletedTask;

        string input = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";


        string[] lines = input.Replace("\r", "").Split("\n").ToArray();
        //string[] lines = DayFourInput.input.Replace("\r", "").Split("\n").ToArray();

        // Creiamo la griglia
        char[,] grid = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        // Step 2: Definisci la parola da cercare
        string wordMas = "MAS";
        string wordSam = "SAM";

        // Step 3: Conta tutte le occorrenze della parola nella griglia
        int count = 0;

        // Definizione delle direzioni: [deltaX, deltaY]
        int[][] directionsASBD = new int[][]
        {
            new int[] { -1, -1 },   // Diagonale alto sinistra
            new int[] { 1, 1 }    // Diagonale basso destra
        };

        int[][] directionsADBS = new int[][]
        {
            new int[] { -1, 1 },   // Diagonale alto destra
            new int[] { 1, -1 }   // Diagonale basso sinistra
        };

        // Step 4: Scorriamo ogni cella della griglia
        for (int i = 0; i < grid.GetLength(0); i++)
        {

            var checkMAS = false;
            var checkSAM = false;

            for (int j = 0; j < grid.GetLength(1); j++)
            {
                char carattere = grid[i,j];

                if (carattere == 'A')
                {
                    //Per ogni cella, verifica se possiamo trovare la parola in una delle 8 direzioni
                    foreach (var direction in directionsASBD)
                    {
                        if (IsWordPresent(grid, i, j, wordMas, direction[0], direction[1]))
                        {
                            checkMAS = true;
                        }
                    }

                    foreach (var direction in directionsADBS)
                    {
                        if (IsWordPresent(grid, i, j, wordMas, direction[0], direction[1]))
                        {
                            checkSAM = true;
                        }
                    }

                    if (checkMAS && checkSAM)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    static bool IsWordPresent(char[,] grid, int startX, int startY, string word, int deltaX, int deltaY)
    {
        int x = startX;
        int y = startY;

        for (int k = 0; k < word.Length; k++)
        {
            // Se siamo fuori dai limiti della griglia, ritorna false
            if (x < 0 || x >= grid.GetLength(0) || y < 0 || y >= grid.GetLength(1))
            {
                return false;
            }

            // Se il carattere corrente non corrisponde, ritorna false
            if (grid[x, y] != word[k])
            {
                return false;
            }

            // Aggiorna la posizione secondo la direzione
            x += deltaX;
            y += deltaY;
        }

        return true;
    }

}
