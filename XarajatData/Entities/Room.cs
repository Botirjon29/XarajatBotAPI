namespace XarajatData.Entities;

public class Room
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Key { get; set; }
    public RoomStatus? Status { get; set; }

    public List<UserEntity>? Users { get; set; }
    public List<Outlay>? Outlays { get; set; }
}