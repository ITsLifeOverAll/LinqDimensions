namespace LinqDimensions;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public int Age { get; set; }
    public double Score { get; set; }
    public Grade Grade { get; set; }

    public Grade SetGrade()
    {
        Grade = Score switch
        {
            < 50 => Grade.Retry,
            < 60 => Grade.Improvement,
            < 70 => Grade.Good,
            < 80 => Grade.Better,
            _ => Grade.Excellent,
        };
        return Grade;
    }

    public override string ToString() => ToString(null);

    public string ToString(char? option)
    {
        return option switch
        {
            'g'  => $"{Name, 8}, {Gender,-7}, {Age,4}, {Score,8:N1}, {Grade}",
            _    => $"{Name, 8}, {Gender,-7}, {Age,4}, {Score,8:N1}",
        };
    }
}

public enum Gender
{
    Male, 
    Female,
}


public enum Grade
{
    Retry,
    Improvement,
    Good,
    Better,
    Excellent,
}