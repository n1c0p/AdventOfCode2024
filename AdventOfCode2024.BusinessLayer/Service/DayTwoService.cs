namespace AdventOfCode2024.BusinessLayer.Service;

public class DayTwoService : IDayTwoService
{   
    public async Task<Result<GenericResponse<int, int>>> DayTwoAsync()
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

        var report = DayTwoInput.report;

        var sumSafeReport = 0;

        for (int i = 0; i < report.Count(); i++)
        {
            var checkSafeReport = SafeReport(report[i]);
            if (!checkSafeReport)
            {
                continue;
            }

            sumSafeReport++;
        }

        return sumSafeReport;
    }

    public async Task<int> PartTwoAsync()
    {
        await Task.CompletedTask;

        var report = DayTwoInput.report;

        var sumProblemDampener = 0;

        for (int i = 0; i < report.Count(); i++)
        {
            var checkProblemDampener = ProblemDampener(report[i]);
            if (!checkProblemDampener)
            {
                continue;
            }

            sumProblemDampener++;
        }

        return sumProblemDampener;
    }

    private bool SafeReport(List<int> currentReport)
    {
        /*
         * State
         * initial -> 0
         * increase -> 1
         * decrease -> 2
         * */
        var initialState = 0;
        var currentState = 0;

        for (int i = 0; i < currentReport.Count(); i++)
        {
            if ((i + 1) > (currentReport.Count() - 1))
            {
                break;
            }

            var first = currentReport[i];
            var second = currentReport[i + 1];
            var stateConsistencyCheck = true;

            if (initialState == 0)
            {
                initialState = first > second ? 1 : 2;
                currentState = initialState;
            }
            else
            {
                currentState = first > second ? 1 : 2;
                if (initialState != currentState)
                {
                    stateConsistencyCheck = false;
                }
                else
                {
                    initialState = currentState;
                }
            }

            
            var difference = Math.Abs(first - second);

            var rangeDifference = (difference >= 1 && difference <= 3);
            var noRangeDifference = first == second;

            if (!rangeDifference || noRangeDifference || !stateConsistencyCheck)
            {
                return false;
            }
        }

        return true;
    }

    private bool ProblemDampener(List<int> currentReport)
    {
        var checkSafeReport = SafeReport(currentReport);
        var currentReportTmp = new List<int>();

        if (!checkSafeReport) 
        {
            if (!currentReportTmp.Any())
            {
                currentReportTmp.AddRange(currentReport);
                for (int i = 0; i < currentReportTmp.Count(); i++)
                {
                    currentReportTmp.RemoveAt(i);
                    checkSafeReport = SafeReport(currentReportTmp);
                    if (checkSafeReport)
                    {
                        return checkSafeReport;
                    }
                    else
                    {
                        currentReportTmp = new();
                        currentReportTmp.AddRange(currentReport);
                    }
                }
            }
        }

        return checkSafeReport;
    }
}