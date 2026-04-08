using System;                     // importojmë DateTime
using System.Collections.Generic; // importojmë List<T>
using System.Linq;                 // importojmë OrderByDescending, Where, Max
using TicketMaster.Domain;         // importojmë Ticket dhe Priority

namespace TicketMaster.Application // namespace i shtresës Application
{
    // shërbimi kryesor i aplikacionit — orkestrues i logjikës biznesore
    // nuk di asgjë rreth si ruhen të dhënat (punon me interface ITicketRepo)
    public class TicketManager
    {
        private readonly ITicketRepo _repository; // referenca e repo-së, injektuar nga konstruktori
        private int _nextId;                      // numëruesi i ID-ve, i inicializuar nga të dhënat ekzistuese

        // konstruktor — merr repo-në si varësi (Dependency Injection)
        public TicketManager(ITicketRepo repository)
        {
            _repository = repository; // ruajmë referencën e repo-së për përdorim në të gjitha metodat

            List<Ticket> existing = _repository.GetAll(); // marrim të gjithë ticketat ekzistues (mund të jetë listë boshe)

            // nëse ka ticketa ekzistues (p.sh. të ngarkuar nga JSON), vazhdojmë nga ID-ja maksimale + 1
            // kështu shmangim ID-të të duplikuara pas rindezjes së programit
            _nextId = existing.Count > 0
                ? existing.Max(t => t.Id) + 1 // max ID ekzistuese + 1
                : 1;                           // nëse nuk ka asnjë ticket, fillojmë nga 1
        }

        // krijon një ticket të ri, e shton në repo dhe e kthen
        public Ticket CreateTicket(string title, string description, DateTime dueDate, Priority priority)
        {
            Ticket ticket = new Ticket(_nextId++, title, description, dueDate, priority); // krijon ticketin me ID-në aktuale dhe inkrementohet
            _repository.Add(ticket); // shton ticketin e ri në repo (memorie ose JSON)
            return ticket;           // kthen ticketin e krijuar për konfirmim
        }

        // kthen listën e plotë të ticketave, të renditur nga prioriteti më i lartë
        public List<Ticket> GetAllTickets()
        {
            return _repository.GetAll()                    // marrim të gjithë ticketat nga repo
                .OrderByDescending(t => t.Priority)        // renditim zbritshëm sipas prioritetit (High > Medium > Low)
                .ToList();                                  // kthejmë si List<Ticket>
        }

        // filtron dhe kthen ticketat me prioritetin e specifikuar
        public List<Ticket> GetByPriority(Priority priority)
        {
            return _repository.GetAll()                    // marrim të gjithë ticketat
                .Where(t => t.Priority == priority)        // filtrojmë vetëm ata me prioritetin e kërkuar
                .ToList();                                  // kthejmë si List<Ticket>
        }

        // kalon ticketin me ID-në e dhënë nga Todo → InProgress
        public bool StartTicket(int id)
        {
            Ticket? ticket = _repository.GetById(id); // kërkojmë ticketin me ID-në e dhënë

            if (ticket == null) return false; // nëse nuk u gjet, raportojmë dështim

            ticket.Start(); // kalojmë ticketin në InProgress (logjika është brenda Ticket.Start())

            return true; // operacioni u krye me sukses
        }

        // kalon ticketin me ID-në e dhënë në Done
        public bool CompleteTicket(int id)
        {
            Ticket? ticket = _repository.GetById(id); // kërkojmë ticketin me ID-në e dhënë

            if (ticket == null) return false; // nëse nuk u gjet, raportojmë dështim

            ticket.Complete(); // kalojmë ticketin në Done (logjika është brenda Ticket.Complete())

            return true; // operacioni u krye me sukses
        }

        // fshin ticketin me ID-në e dhënë nga repo
        public bool DeleteTicket(int id)
        {
            return _repository.Delete(id); // ia delegojmë repo-së dhe kthejmë rezultatin (true/false)
        }
    }
}