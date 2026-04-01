using System.Collections.Generic;
using System.Linq;
using TicketMaster.Domain;

namespace TicketMaster.Application
{
    public class InMemoryTicketRepo : ITicketRepo
    {
        private readonly List<Ticket> _tickets = new List<Ticket>();

        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket);
        }

        public List<Ticket> GetAll()
        {
            return _tickets;
        }

        public Ticket? GetById(int id)
        {
            return _tickets.FirstOrDefault(t => t.Id == id);
        }
    }
}