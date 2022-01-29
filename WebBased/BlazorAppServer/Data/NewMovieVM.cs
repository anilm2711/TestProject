using EBazarModels.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace BlazorAppServer.Data
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Display(Name = "Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Movie description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Movie start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Display(Name = "Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; } = DateTime.Now;

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        [MinLength(1)]
        [MaxLength(10)]
        public MovieCategory MovieCategory { get; set; } = MovieCategory.Action;

        //Relationships
        [Display(Name = "Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; } = new List<int>();

        [Display(Name = "Select a cinema")]
        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }

        [Display(Name = "Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }
    }
}
