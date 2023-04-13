namespace EvaFloraStore.Models.ViewModels
{
    public class PageDividerViewModel
    {
        public int TotalItems { get; set; }     
        public int ItemsPerPage { get; set; }   
        public int CurrentPage { get; set; }
        public string? Category { get; set; }
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
