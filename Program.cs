using System;
using TicketMaster.Application;
using TicketMaster.Domain;

namespace TicketMaster.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITicketRepo repository = new InMemoryTicketRepo();
            TicketManager ticketService = new TicketManager(repository);
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Sistemi i Ticketave ===");
                Console.WriteLine("1. Shto ticket");
                Console.WriteLine("2. Shiko të gjithë ticketat");
                Console.WriteLine("3. Shiko sipas prioritetit");
                Console.WriteLine("4. Fillo ticket");
                Console.WriteLine("5. Mbyll ticket");
                Console.WriteLine("0. Dil");
                Console.Write("Zgjidh një opsion: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Titulli: ");
                        string title = Console.ReadLine() ?? string.Empty;

                        Console.Write("Përshkrimi: ");
                        string description = Console.ReadLine() ?? string.Empty;

                        Console.Write("Afati (yyyy-mm-dd): ");
                        DateTime dueDate;
                        if (!DateTime.TryParse(Console.ReadLine(), out dueDate))
                            dueDate = DateTime.Today;

                        Priority priority = AskPriority();

                        try
                        {
                            ticketService.CreateTicket(title, description, dueDate, priority);
                            Console.WriteLine("Ticket u shtua me sukses!\n");
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Gabim: {ex.Message}\n");
                        }
                        break;

                    case "2":
                        var allTickets = ticketService.GetAllTickets();
                        if (allTickets.Count == 0)
                        {
                            Console.WriteLine("Nuk u gjet asnjë ticket.\n");
                            break;
                        }
                        Console.WriteLine("\n--- Të gjithë Ticketat (sipas prioritetit) ---");
                        foreach (Ticket t in allTickets)
                            Console.WriteLine(t);
                        break;

                    case "3":
                        Priority filter = AskPriority();
                        var filtered = ticketService.GetByPriority(filter);
                        string filterLabel = filter switch
                        {
                            Priority.High => "HIGH",
                            Priority.Medium => "MEDIUM",
                            Priority.Low => "LOW",
                            _ => "UNKNOWN"
                        };
                        Console.WriteLine($"\n--- Ticketat me prioritet {filterLabel} ---");
                        if (filtered.Count == 0)
                        {
                            Console.WriteLine("Nuk u gjet asnjë ticket për këtë prioritet.\n");
                            break;
                        }
                        foreach (Ticket t in filtered)
                            Console.WriteLine(t);
                        break;

                    case "4":
                        Console.Write("ID e ticketit: ");
                        if (int.TryParse(Console.ReadLine(), out int startId))
                            Console.WriteLine(ticketService.StartTicket(startId)
                                ? "Ticket u fillua.\n"
                                : "Ticket nuk u gjet.\n");
                        else
                            Console.WriteLine("ID e pavlefshme.\n");
                        break;

                    case "5":
                        Console.Write("ID e ticketit: ");
                        if (int.TryParse(Console.ReadLine(), out int completeId))
                            Console.WriteLine(ticketService.CompleteTicket(completeId)
                                ? "Ticket u mbyll me sukses.\n"
                                : "Ticket nuk u gjet.\n");
                        else
                            Console.WriteLine("ID e pavlefshme.\n");
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Mirupafshim!");
                        break;

                    default:
                        Console.WriteLine("Opsion i pavlefshëm. Zgjidh nga 0–5.\n");
                        break;
                }
            }
        }

        static Priority AskPriority()
        {
            while (true)
            {
                Console.WriteLine("Zgjidh prioritetin:");
                Console.WriteLine("  1. High");
                Console.WriteLine("  2. Medium");
                Console.WriteLine("  3. Low");
                Console.Write("Opsioni: ");
                string? input = Console.ReadLine();
                switch (input)
                {
                    case "1": return Priority.High;
                    case "2": return Priority.Medium;
                    case "3": return Priority.Low;
                    default:
                        Console.WriteLine("Opsion i pavlefshëm. Provo përsëri.\n");
                        break;
                }
            }
        }
    }
}