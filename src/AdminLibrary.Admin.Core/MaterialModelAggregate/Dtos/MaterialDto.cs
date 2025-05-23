namespace AdminLibrary.Core.Dtos
{
    public class MaterialDto(
         string identifier,
         string title,
         int userId,
         DateTime registerDate,
         int registerQuantity,
         int currentQuantity)
    {
        public string? Identifier { get; set; } = identifier;
        public string? Title { get; set; } = title;

        public int UserId { get; set; }   = userId;
        public DateTime? RegisterDate { get; set; } = registerDate;
        public int? RegisterQuantity { get; set; } = registerQuantity;
        public int? CurrentQuantity { get; set; } = currentQuantity;
    }

    public class MaterialUpdateDto(
         int id,
         string identifier,
         string title,
         DateTime registerDate,
         int registerQuantity,
         int currentQuantity)
    {
        public int Id { get; set; } = id;
        public string? Identifier { get; set; } = identifier;
        public string? Title { get; set; } = title;
        public DateTime? RegisterDate { get; set; } = registerDate;
        public int? RegisterQuantity { get; set; } = registerQuantity;
        public int? CurrentQuantity { get; set; } = currentQuantity;
    }
}
