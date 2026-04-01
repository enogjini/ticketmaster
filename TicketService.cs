using System;
using System.Collections.Generic;
using System.Linq;
using TicketMaster.Domain;

namespace TicketMaster.Application
{
    public class TicketManager
    {
        private readonly ITicketRepo _repository;
        private int _nextId = 1;

        public TicketManager(ITicketRepo repository)
        {
            _repository = repository;
        }

        public Ticket CreateTicket(string title, string description, DateTime dueDate, Priority priority)
        {
            Ticket ticket = new Ticket(_nextId++, title, description, dueDate, priority);
            _repository.Add(ticket);
            return ticket;
        }

        public List<Ticket> GetAllTickets()
        {
            return _repository.GetAll()
                .OrderByDescending(t => t.Priority)
                .ToList();
        }

        public List<Ticket> GetByPriority(Priority priority)
        {
            return _repository.GetAll()
                .Where(t => t.Priority == priority)
                .ToList();
        }

        public bool StartTicket(int id)
        {
            Ticket? ticket = _repository.GetById(id);
            if (ticket == null) return false;
            ticket.Start();
            return true;
        }

        public bool CompleteTicket(int id)
        {
            Ticket? ticket = _repository.GetById(id);
            if (ticket == null) return false;
            ticket.Complete();
            return true;
        }
    }
}