using System;

namespace TicketMaster.Domain
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum TicketStatus
    {
        Todo,
        InProgress,
        Done
    }

    public class Ticket
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public Priority Priority { get; private set; }
        public TicketStatus Status { get; private set; }

        public Ticket(int id, string title, string description, DateTime dueDate, Priority priority)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Titulli nuk mund të jetë bosh.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Përshkrimi nuk mund të jetë bosh.");

            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            Status = TicketStatus.Todo;
        }

        public void Start()
        {
            if (Status == TicketStatus.Todo)
                Status = TicketStatus.InProgress;
        }

        public void Complete()
        {
            if (Status == TicketStatus.InProgress || Status == TicketStatus.Todo)
                Status = TicketStatus.Done;
        }

        public override string ToString()
        {
            string priorityLabel = Priority switch
            {
                Priority.High => "[HIGH]",
                Priority.Medium => "[MEDIUM]",
                Priority.Low => "[LOW]",
                _ => "[UNKNOWN]"
            };

            string statusLabel = Status switch
            {
                TicketStatus.Todo => "[TODO]",
                TicketStatus.InProgress => "[IN PROGRESS]",
                TicketStatus.Done => "[DONE]",
                _ => "[UNKNOWN]"
            };

            return $"{statusLabel} {priorityLabel} [{Id}] {Title}: {Description} | Afati: {DueDate:yyyy-MM-dd}";
        }
    }
}