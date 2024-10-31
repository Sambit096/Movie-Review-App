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
                throw new Exception("Error when retrieving ShowTimes from Database:", error);
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
                throw new Exception("Error when retrieving ShowTimes from Database:", error);
            }
        }
        /// <summary>
        /// Gets all Tickets for a specific Showtime
        /// </summary>
        /// <param name="showTimeId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IList<Ticket>> GetTicketsForShowTime(int showTimeId) {   
            try {
                var allTickets = await (from ticket in dbContext.Tickets
                where ticket.ShowTimeId == showTimeId
                select ticket).ToListAsync();
                return allTickets;
            } catch (Exception error) {
                throw new Exception("Error when retrieving ShowTime Tickets from Database:", error);
            }
        }*/
    
    }
}