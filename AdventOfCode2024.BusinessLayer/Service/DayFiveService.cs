namespace AdventOfCode2024.BusinessLayer.Service;
public class DayFiveService : IDayFiveService
{

    //Nicola - Giorno 5 (Coda di stampa)

    //Per risolvere puzzle e restituire i risultati ho usato una struttura dati generica formata due Interfacce

    //IDayFiveService
    //ICommonService

    //La prima interfaccia contiene un solo metodo SolutionPuzzleAsync che restituisce una risposta generica, customizzabile in base all’esigenza.
    //La seconda interfaccia invece è un'astrazione generica per risolvere le due parti dei puzzle, infatti è formata da due metodi PartOneAsync e PartTwoAsync che restituiscono un generic.

    //Questa astrazione in questo preciso puzzle mi ha permesso di restituire come risultato della prima parte non solo la somma richiesta ma anche altri due strutture dati che mi servivano per sviluppare la seconda parte.
    //Ho restituito il tutto sono forma di tupla, in questo modo nella prima nella risposta generica ho prelevato il campo della somma e
    //nella seconda parte ho richiamato la prima ed estratto le due strutture dati che mi servivano per riordinare le code di stampa sbagliate e restituire la loro somma alla risposta generica. (spiegazione Marzulliana 🙂)


    public async Task<Result<GenericResponse<int, int>>> SolutionPuzzleAsync()
    {
        /**
         * Chiamo le due parti e restituisco le somma nelle risposta generica 
         */

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

        /*
         * Split della stringa in input
         * Posizione 1 -> array delle regole
         * Posizione 2 -> array degli aggiornamenti
         */
        var parts = DayFiveInput.tmp.Split("\r\n\r\n");

        /*
         * Split dell'array delle regole, creo per ogni regola una tupla (47,53) 
         */
        var rules = parts.First().Split("\r\n").Select(number => (Convert.ToInt32(number.Split("|").First()), Convert.ToInt32(number.Split("|").Last()))).ToList();
        /*
         * Split delle riga degli update (contestualmente faccio il parse da stringa ad intero): creo una lista
         *
         */
        var updates = parts.Last().Split("\r\n").Select(number => number.Split(",").Select(int.Parse).ToList());

        /*
         * Unisco le tuple delle regole eliminando i doppioni
         */
        var pages = rules.Select(x => x.Item1).Union(rules.Select(x => x.Item2)).Distinct().ToList();
        
        /*
         * Creo due dizionari per le regole prima e dopo
         * formato dal numero corrente come indice e da una lista di numeri che ci sono prima oppure dopo di esso
         */
        var after = new Dictionary<int, List<int>>();
        var before = new Dictionary<int, List<int>>();

        /*
         * Popolo gli array secondo la logica sopra descritta
         */
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

        /*
         * Inizializzo una lista di lista di interi per contenere tutti gli update incorrentti
         * **/
        var incorrects = new List<List<int>>();

        /*
         * Scorro tutte righe degli update applicando le regole create sopra
         * 
         */
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

            /*
             * In caso positivo sommo al risultato parziale il numero centrale
             * */
            if (correct)
            {
                result += update[update.Count / 2];
            }
            else
            {
                /*
                 * altrimenti aggiungo l'array di interi nella lista degli incorretti (che userò nella seconda parte)
                 */
                incorrects.Add(update);
            }
        }

        /*
         * Restituisco la tupla
         * **/

        return (Result: result, Incorrects: incorrects, Before: before);
    }

    public async Task<int> PartTwoAsync()
    {
        await Task.CompletedTask;
        var result = 0;

        /*
         * Senza riscrivere tutta la logica, chiamo la prima parte e mi prendo le strutture dati 
         * necessarie da riordinare e poi sommare, seguendo la stessa logica della prima parte
         */
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
