namespace App.Infrastructure.Models;

public class ErrorResponse
{
    public int Status { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
}