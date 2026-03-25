class Ticket
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }

    public Ticket(string title, string description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Titulli nuk mund të jetë bosh.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Përshkrimi nuk mund të jetë bosh.");

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
        try
        {
            tickets.Add(new Ticket(title, description));
            Console.WriteLine("Ticket u shtua me sukses!\n");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Gabim: {ex.Message}\n");
        }
    }

    public void ViewTickets()
    {
        if (tickets.Count == 0)
        {
            Console.WriteLine("Nuk u gjet asnjë ticket.\n");
            return;
        }

        Console.WriteLine("\n--- Të gjithë Ticketat ---");
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
            Console.WriteLine("Numër i pavlefshëm.\n");
            return;
        }

        tickets[index - 1].IsDone = true;
        Console.WriteLine($"Ticket \"{tickets[index - 1].Title}\" u shënua si i kryer!\n");
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
            Console.WriteLine("=== Sistemi i Ticketave ===");
            Console.WriteLine("1. Shto ticket");
            Console.WriteLine("2. Shiko ticketat");
            Console.WriteLine("3. Shëno ticket si të kryer");
            Console.WriteLine("4. Dil");
            Console.Write("Zgjidh një opsion: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Shkruaj titullin: ");
                    string title = Console.ReadLine();
                    Console.Write("Shkruaj përshkrimin: ");
                    string description = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description))
                        Console.WriteLine("Titulli dhe përshkrimi nuk mund të jenë bosh.\n");
                    else
                        manager.AddTicket(title, description);
                    break;

                case "2":
                    manager.ViewTickets();
                    break;

                case "3":
                    manager.ViewTickets();
                    Console.Write("Shkruaj numrin e ticketit për ta shënuar si të kryer: ");
                    if (int.TryParse(Console.ReadLine(), out int ticketNumber))
                        manager.MarkAsDone(ticketNumber);
                    else
                        Console.WriteLine("Ju lutem shkruaj një numër të vlefshëm.\n");
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Mirupafshim!");
                    break;

                default:
                    Console.WriteLine("Opsion i pavlefshëm. Zgjidh nga 1–4.\n");
                    break;
            }
        }
    }
}