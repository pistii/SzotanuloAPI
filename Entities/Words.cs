using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SzotanuloAPI.Entities
{
    public class Words
    {
        [Key]
        public int wordId { get; set; }
        public string EnglishMeaning { get; set; } = null!;
        public string HungarianMeaning { get; set; } = null!;
        public byte RememberanceLevel { get; set; }
    }
}
