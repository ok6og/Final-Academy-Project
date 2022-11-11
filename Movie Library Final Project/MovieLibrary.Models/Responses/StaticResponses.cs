namespace MovieLibrary.Models.Responses
{
    public static class StaticResponses
    {
        public static string UserIdLessThanOrEqualTo0 = "There is no user Id that is less than or equal to 0!";
        public static string MovieIdLessThanOrEqualTo0 = "There is no movie Id that is less than or equal to 0!";
        public static string UserDoesNotExist = "User with that Id does not exist!";
        public static string MovieDoesNotExist = "Movie with that Id does not exist!";
        public static string WatchListDoesNotExist = "Watch list with that Id does not exist!";
        public static string WatchedMoviesListNotExist = "Watched movies list with that Id does not exist!";

        public static string SuccessfullyCompletedTheOperation = "Successfully completed the operation";
        public static string SuccessfullyAddedTheObject = "Successfully added the object";
    }
}
