namespace APBD_CW7.DTOs;

public class ManufacturerDto
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime FoundationDate { get; set; }
}