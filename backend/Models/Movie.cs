namespace MovieReviewApp.Models
{
    public class Movie{
    public int MovieId { get; set; }
    public required string Title { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public MPAARating Rating { get; set; }

    

    public Movie(){}
}


    public enum MPAARating
    {
        G=0,
        PG=1,
        PG13=2,
        R=3,
        NC17=4
    }
}