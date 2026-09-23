using System.ComponentModel.DataAnnotations;

public class StudentDto
{
    [Required]
    public string Name { get; set; } = "";

    [Range(0, 100)]
    public int Score { get; set; }
}
