using System; // importojmë tipe bazë

namespace TicketMaster.Domain // namespace për domain
{
    public enum Priority // enum për prioritetin e ticketit
    {
        Low, // prioritet i ulët
        Medium, // prioritet mesatar
        High // prioritet i lartë
    }

    public enum TicketStatus // enum për statusin e ticketit
    {
        Todo, // status fillestar
        InProgress, // në progres
        Done // i përfunduar
    }

    public class Ticket // klasa Ticket
    {
        public int Id { get; private set; } // ID e ticketit
        public string Title { get; private set; } // titulli i ticketit
        public string Description { get; private set; } // përshkrimi i ticketit
        public DateTime DueDate { get; private set; } // afati i ticketit
        public Priority Priority { get; private set; } // prioriteti i ticketit
        public TicketStatus Status { get; private set; } // statusi i ticketit

        public Ticket(int id, string title, string description, DateTime dueDate, Priority priority) // konstruktor
        {
            if (string.IsNullOrWhiteSpace(title)) // kontrollojmë titullin
                throw new ArgumentException("Titulli nuk mund të jetë bosh."); // hedhim përjashtim nëse është bosh
            if (string.IsNullOrWhiteSpace(description)) // kontrollojmë përshkrimin
                throw new ArgumentException("Përshkrimi nuk mund të jetë bosh."); // hedhim përjashtim nëse është bosh

            Id = id; // caktojmë ID
            Title = title; // caktojmë titullin
            Description = description; // caktojmë përshkrimin
            DueDate = dueDate; // caktojmë afatin
            Priority = priority; // caktojmë prioritetin
            Status = TicketStatus.Todo; // statusi fillestar është Todo
        }

        public void Start() // fillojmë ticketin
        {
            if (Status == TicketStatus.Todo) // nëse statusi është Todo
                Status = TicketStatus.InProgress; // ndryshojmë në InProgress
        }

        public void Complete() // përfundojmë ticketin
        {
            if (Status == TicketStatus.InProgress || Status == TicketStatus.Todo) // nëse është në progres ose Todo
                Status = TicketStatus.Done; // ndryshojmë në Done
        }

        // Shto brenda klasës Ticket, pas Complete()
        public static Ticket Restore(int id, string title, string description,
                                      DateTime dueDate, Priority priority, TicketStatus status) // rikthejmë një ticket të fshirë
        {
            var t = new Ticket(id, title, description, dueDate, priority); // krijojmë një instancë të re Ticket
            if (status == TicketStatus.InProgress) t.Start(); // nëse statusi ishte në progres, e fillojmë ticketin
            else if (status == TicketStatus.Done) t.Complete(); // nëse statusi ishte i përfunduar, e përfundojmë ticketin
            return t; // kthejmë ticketin e rikthyer
        }

        public override string ToString() // konvertojmë objektin në string
        {
            string priorityLabel = Priority switch // etiketimi i prioritetit
            {
                Priority.High => "[HIGH]",
                Priority.Medium => "[MEDIUM]",
                Priority.Low => "[LOW]",
                _ => "[UNKNOWN]"
            };

            string statusLabel = Status switch // etiketimi i statusit
            {
                TicketStatus.Todo => "[TODO]",
                TicketStatus.InProgress => "[IN PROGRESS]",
                TicketStatus.Done => "[DONE]",
                _ => "[UNKNOWN]"
            };

            // kthejmë stringun e formatizuar
            return $"{statusLabel} {priorityLabel} [{Id}] {Title}: {Description} | Afati: {DueDate:yyyy-MM-dd}";
        }
    }
}