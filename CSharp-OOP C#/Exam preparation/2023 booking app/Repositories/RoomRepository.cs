using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;

        public RoomRepository()
        {
            rooms = new List<IRoom>();
        }

        public void AddNew(IRoom model)
        {
            rooms.Add(model);
        }

        public IRoom Select(string roomTypeName)
            => rooms.FirstOrDefault(x => x.GetType().Name == roomTypeName);

        public IReadOnlyCollection<IRoom> All() => rooms.AsReadOnly();
    }
}
