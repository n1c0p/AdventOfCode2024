namespace AdventOfCode2024.BusinessLayer.Interface;

public interface ICommonService<T1,T2>
{
    Task<T1> PartOneAsync();
    Task<T2> PartTwoAsync();
}