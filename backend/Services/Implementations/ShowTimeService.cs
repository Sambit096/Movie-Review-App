using MovieReviewApp.Interfaces;
using MovieReviewApp.Models;
using MovieReviewApp.Data;
using Microsoft.EntityFrameworkCore;
using MovieReviewApp.Tools;

namespace MovieReviewApp.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly MovieReviewDbContext dbContext;

        public ShowTimeService(MovieReviewDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Gets all ShowTimes from the database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ShowTime>> GetAllShowTimes() {
            try{
                var allShowTimes = await dbContext.ShowTimes.ToListAsync();
                return allShowTimes;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Gets all ShowTimes for a specific movie based on MovieId from the database
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        public async Task<IList<ShowTime>> GetShowTimes(int movieId){
            try {
                var allShowTimes = await dbContext.ShowTimes.Where(st => st.MovieId == movieId).ToListAsync();
                return allShowTimes;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Gets all Tickets for a specific Showtime
        /// </summary>
        /// <param name="showTimeId"></param>
        /// <returns></returns>
        public async Task<IList<Ticket>> GetTicketsForShowTime(int showTimeId)
        {
            try
            {
                var allTickets = await dbContext.Tickets.Where(t => t.ShowTimeId == showTimeId).ToListAsync();
                return allTickets;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Adds a new ShowTime to the database
        /// </summary>
        /// <param name="showTime"></param>
        /// <returns></returns>
        public async Task<bool> AddShowTime(ShowTime showTime)
        {
            try
            {
                await this.dbContext.ShowTimes.AddAsync(showTime);
                await this.dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        /// <summary>
        /// Gets a ShowTime by its unique identifier.
        /// </summary>
        /// <param name="showTimeId">The unique identifier of the ShowTime.</param>
        /// <returns>The ShowTime object if found; otherwise, null.</returns>
        public async Task<ShowTime> GetShowTimeById(int showTimeId)
        {
            try
            {
                var showTime = await dbContext.ShowTimes
                    .FirstOrDefaultAsync(st => st.ShowTimeId == showTimeId);
                return showTime;
            }
            catch (Exception)
            {
                throw new Exception(ErrorDictionary.ErrorLibrary[500]);
            }
        }

        
    }
}
