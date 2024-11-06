namespace MovieReviewApp.Tools;

public static class ErrorDictionary
{
    // Dictionary to store error codes and messages
    public static readonly Dictionary<int, string> ErrorLibrary = new Dictionary<int, string>
    {
        { 404, "Error when retrieving data from database: " },
        { 400, "No item(s) with this id were found." },
        { 500, "An unexpected error has occured: " },
        { 501, "Error when adding item to database: " },
        { 502, "Error when removing item from database: " }
    };
}