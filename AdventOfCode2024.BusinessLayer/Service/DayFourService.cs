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
        var map = DayFourInput.input.Split("\r\n").ToList();
        var rows = map.Count;
        var cols = map.First().Length;

        string word = "MAS";
        var count = 0;

        var dirs = (from row in new[] { 0, 1, -1 }
                    from col in new[] { 0, 1, -1 }
                    select (row,col)
                    ).ToList();

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                var masWord_1 = $"{FindWord(row - 1, col - 1,rows,cols,map)}{FindWord(row, col, rows, cols, map)}{FindWord(row + 1, col + 1, rows, cols, map)}";
                var masWord_2 = $"{FindWord(row - 1, col + 1, rows, cols, map)}{FindWord(row, col, rows, cols, map)}{FindWord(row + 1, col - 1, rows, cols, map)}";

                if (
                    (masWord_1 == word || new string(masWord_1.Reverse().ToArray()) == word)
                    &&
                    (masWord_2 == word || new string(masWord_2.Reverse().ToArray()) == word)
                )
                {
                    count++;
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

    
    static char FindWord(int row, int col, int rows, int cols, List<string> map)
    {
        return (row >= 0 && row < rows) && (col >= 0 && col < cols) ? map[row][col] : '.'; ;
    }

}
