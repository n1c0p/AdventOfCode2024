namespace AdventOfCode2024.SharedLayer.Dto
{
    public class GenericResponse<T1,T2>
    {
        public T1 PartOne { get; set; }
        public T2 PartTwo { get; set; }
    }
}