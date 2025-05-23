namespace AdminLibrary.Core.Dtos
{

    public class MovementsDto(
         int materialId,
         int userId,
         string? observations,
         string movementType,
         DateTime? movementDate
         )
    {
        public int MaterialId { get; set; } = materialId;
        public int UserId { get; set; } = userId;
        public string? Observations { get; set; } = observations;
        public string MovementType { get; set; } = movementType;
        public DateTime? MovementDate { get; set; } = movementDate;
    }

    public class MovementsDtoResponse
    {
        public string? Title { get; set; } 
        public string? UserName { get; set; }
        public string? Observations { get; set; } 
        public string MovementType { get; set; } 
        public DateTime? MovementDate { get; set; } 
    }
}
