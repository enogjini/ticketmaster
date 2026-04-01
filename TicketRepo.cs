using System.Collections.Generic;
using TicketMaster.Domain;

namespace TicketMaster.Application
{
    public interface ITicketRepo
    {
        void Add(Ticket ticket);
        List<Ticket> GetAll();
        Ticket? GetById(int id);
    }
}