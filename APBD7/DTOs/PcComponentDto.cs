namespace APBD_CW7.DTOs;

public class PcComponentDto
{
    public int Amount { get; set; }

    public ComponentDto Component { get; set; } = null!;
}