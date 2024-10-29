using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using System.Linq;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MovieReviewApp.Services {
    public class ShowTimeService : IShowTimeService {
        private readonly MovieReviewDbContext dbContext;

        public ShowTimeService(MovieReviewDbContext dbContext) {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets all ShowTimes from Database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<ShowTime>> GetAllShowTimes() {
            try {
                List<ShowTime> allShowTimes = new List<ShowTime>();
                allShowTimes = await dbContext.ShowTimes.ToListAsync();
                return allShowTimes;
            } catch (Exception error) {
                throw new Exception("Error when retreiving ShowTimes from Database:", error);
            }
        }
        /// <summary>
        /// Gets all ShowTimes for a specific movie based on MovieId from database
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<ShowTime>> GetShowTimes(int movieId) {
            try {
                var allShowTimes = await dbContext.ShowTimes.Where(st => st.MovieId == movieId).ToListAsync();
                return allShowTimes;   
            } catch (Exception error) {
                throw new Exception("Error when retreiving ShowTimes from Database:", error);
            }
        }
        /// <summary>
        /// Gets all Tickets for a specific movie based on ShowTimes for the movie (by Movie Id)
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<int> GetTickets(int movieId) {   
            try {
                var allShowTimes = await (from st in dbContext.ShowTimes
                where st.MovieId == movieId
                select st.NumOfTickets).SumAsync();
                return allShowTimes;
            } catch (Exception error) {
                throw new Exception("Error when retreiving ShowTime Tickets from Database:", error);
            }
        }
    
    }
}