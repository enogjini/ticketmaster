class Ticket
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Ticket(string title, string description)
    {
        Title = title;
        Description = description;
        IsDone = false;
    }

    public override string ToString()
    {
        string status = IsDone ? "[DONE]" : "[PENDING]";
        return $"{status} {Title}: {Description}";
    }
}

class TicketManager
{
    private List<Ticket> tickets = new List<Ticket>();

    public void AddTicket(string title, string description)
    {
        tickets.Add(new Ticket(title, description));
        Console.WriteLine("Ticket added successfully!\n");
    }

    public void ViewTickets()
    {
        if (tickets.Count == 0)
        {
            Console.WriteLine("No tickets found.\n");
            return;
        }

        Console.WriteLine("\n--- All Tickets ---");
        for (int i = 0; i < tickets.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tickets[i]}");
        }
        Console.WriteLine();
    }

    public void MarkAsDone(int index)
    {
        if (index < 1 || index > tickets.Count)
        {
            Console.WriteLine("Invalid ticket number.\n");
            return;
        }

        tickets[index - 1].IsDone = true;
        Console.WriteLine($"Ticket \"{tickets[index - 1].Title}\" marked as done!\n");
    }
}

class Program
{
    static void Main(string[] args)
    {
        TicketManager manager = new TicketManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("=== Ticket System ===");
            Console.WriteLine("1. Add ticket");
            Console.WriteLine("2. View tickets");
            Console.WriteLine("3. Mark ticket as done");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter description: ");
                    string description = Console.ReadLine();
                    manager.AddTicket(title, description);
                    break;

                case "2":
                    manager.ViewTickets();
                    break;

                case "3":
                    manager.ViewTickets();
                    Console.Write("Enter ticket number to mark as done: ");
                    if (int.TryParse(Console.ReadLine(), out int ticketNumber))
                        manager.MarkAsDone(ticketNumber);
                    else
                        Console.WriteLine("Please enter a valid number.\n");
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose 1–4.\n");
                    break;
            }
        }
    }
}