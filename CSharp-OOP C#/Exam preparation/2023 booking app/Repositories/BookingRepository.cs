using BookingApp.Models.Bookings.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class BookingRepository : IRepository<IBooking>
    {
        private List<IBooking> bookings;
        public BookingRepository()
        {
            bookings = new List<IBooking>();
        }

        public void AddNew(IBooking model)
        {
            bookings.Add(model);
        }
        public IBooking Select(string bookingNumberToString)
            => bookings.FirstOrDefault(x => x.BookingNumber.ToString() == bookingNumberToString);

        public IReadOnlyCollection<IBooking> All() => bookings.AsReadOnly();
    }
}
