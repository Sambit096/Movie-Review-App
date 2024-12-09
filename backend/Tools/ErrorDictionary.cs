namespace MovieReviewApp.Tools
{
    public static class ErrorDictionary
    {
        // Dictionary to store error codes and messages
        public static readonly Dictionary<int, string> ErrorLibrary = new Dictionary<int, string>
        {
            { 400, "Bad Request - The server could not understand the request due to invalid syntax." },
            { 401, "Unauthorized - Authentication is required to access this resource." },
            { 403, "Forbidden - You do not have permission to access this resource." },
            { 404, "Not Found - The requested resource could not be found." },
            { 409, "Conflict - There is a conflict with the current state of the resource. Duplicate Value Detected." },
            { 422, "Unprocessable Entity - The request was well-formed but could not be followed due to semantic errors." },
            { 500, "Internal Server Error - The server encountered an unexpected condition." },
            { 501, "Not Implemented - The server does not support the functionality required to fulfill the request." },
            { 503, "Service Unavailable - The server is currently unable to handle the request." }
        };
    }
}
