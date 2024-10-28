using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;

namespace MovieReviewApp.Services {
    public class ShowTimeService : IShowTimeService {
        private readonly MovieReviewDbContext dbContext;

        public ShowTimeService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }
        public IList<ShowTime> GetAllShowTimes() {
            List<ShowTime> allShowTimes = new List<ShowTime>();
            allShowTimes = dbContext.ShowTimes.ToList();
            return allShowTimes;
        }
        public IList<ShowTime> GetShowTimes(int movieId) {
            var allShowTimes = dbContext.ShowTimes.Where(st => st.MovieId == movieId).ToList();
            return allShowTimes;
        }

        public int GetTickets(int movieId) {   
            var allShowTimes = (from st in dbContext.ShowTimes
            where st.MovieId == movieId
            select st.NumOfTickets).Sum();
            return allShowTimes;
        }
    
    }
}