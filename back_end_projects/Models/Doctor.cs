using System.ComponentModel.DataAnnotations.Schema;

namespace back_end_projects.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string? ImgUrl { get; set; }

        [NotMapped]
        public IFormFile? ImgFile { get; set; }



    }
}
