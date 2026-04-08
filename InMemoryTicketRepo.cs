using System.Collections.Generic; // importojmë List<T>
using System.Linq;                // importojmë FirstOrDefault dhe metoda LINQ
using TicketMaster.Domain;        // importojmë modelin Ticket

namespace TicketMaster.Application // namespace i shtresës Application
{
    // implementim i ITicketRepo që ruan ticketat vetëm në memorie (pa persistence)
    // i dobishëm për testim ose kur nuk nevojitet ruajtje e përhershme
    public class InMemoryTicketRepo : ITicketRepo
    {
        private readonly List<Ticket> _tickets = new List<Ticket>(); // lista private që mban të gjithë ticketat në memorie

        // shton ticketin e dhënë në fund të listës
        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket); // shtojmë ticketin direkt në listë
        }

        // kthen referencën e listës me të gjithë ticketat
        public List<Ticket> GetAll()
        {
            return _tickets; // kthejmë listën e plotë pa filtra
        }

        // kërkon ticketin me ID të caktuar duke përdorur LINQ
        public Ticket? GetById(int id)
        {
            return _tickets.FirstOrDefault(t => t.Id == id); // kthen ticketin e parë që ka ID-në e kërkuar, ose null
        }

        // fshin ticketin me ID të caktuar nga lista në memorie
        public bool Delete(int id)
        {
            Ticket? ticket = _tickets.FirstOrDefault(t => t.Id == id); // kërkon ticketin me ID-në e dhënë

            if (ticket == null) return false; // nëse nuk u gjet, kthen false

            _tickets.Remove(ticket); // heq ticketin nga lista

            return true; // fshirja u krye me sukses
        }
    }
}