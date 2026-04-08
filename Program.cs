using System;                       // importojmë Console, DateTime
using TicketMaster.Application;     // importojmë JsonTicketRepo dhe TicketManager
using TicketMaster.Domain;          // importojmë Ticket dhe Priority

namespace TicketMaster.ConsoleUI    // namespace i shtresës UI (ndërfaqja e konsolës)
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITicketRepo repository = new JsonTicketRepo();          // krijojmë repo-në JSON (skedari "tickets.json" në direktorinë aktuale)
            TicketManager ticketService = new TicketManager(repository); // inicializojmë shërbimin kryesor me repo-në JSON
            bool running = true;                                     // flamuri i ciklit kryesor — false do të ndalojë programin

            while (running) // cikli kryesor i programit — vazhdon derisa përdoruesi zgjedh "Dil"
            {
                // shfaqim menynë kryesore
                Console.WriteLine("\n=== Sistemi i Ticketave ==="); // titull i menysë
                Console.WriteLine("1. Shto ticket");                // opsioni 1: krijon ticket të ri
                Console.WriteLine("2. Shiko të gjithë ticketat");   // opsioni 2: liston të gjithë ticketat
                Console.WriteLine("3. Shiko sipas prioritetit");    // opsioni 3: filtron sipas prioritetit
                Console.WriteLine("4. Fillo ticket");               // opsioni 4: kalon ticketin në InProgress
                Console.WriteLine("5. Mbyll ticket");               // opsioni 5: kalon ticketin në Done
                Console.WriteLine("6. Fshij ticket");               // opsioni 6: fshin ticketin nga sistemi
                Console.WriteLine("0. Dil");                        // opsioni 0: mbyll programin
                Console.Write("Zgjidh një opsion: ");               // ftojmë përdoruesin të japë zgjedhjen
                string? choice = Console.ReadLine();                 // lexojmë zgjedhjen e përdoruesit

                switch (choice) // trajtojmë çdo opsion të mundshëm
                {
                    case "1": // opsioni: krijo ticket të ri
                        Console.Write("Titulli: ");                        // kërkojmë titullin
                        string title = Console.ReadLine() ?? string.Empty; // lexojmë titullin (string.Empty nëse është null)

                        Console.Write("Përshkrimi: ");                           // kërkojmë përshkrimin
                        string description = Console.ReadLine() ?? string.Empty; // lexojmë përshkrimin

                        Console.Write("Afati (yyyy-mm-dd): ");                       // kërkojmë afatin në formatin e specifikuar
                        DateTime dueDate;                                             // variabla për afatin
                        if (!DateTime.TryParse(Console.ReadLine(), out dueDate))     // provujmë të analizojmë datën
                            dueDate = DateTime.Today;                                 // nëse formati është i gabuar, përdorim datën e sotme si parazgjedhje

                        Priority priority = AskPriority(); // pyesim përdoruesin për prioritetin nëpërmjet metodës ndihmëse

                        try // tentojmë të krijojmë ticketin (mund të hedhin ArgumentException)
                        {
                            ticketService.CreateTicket(title, description, dueDate, priority); // krijojmë ticketin nëpërmjet shërbimit
                            Console.WriteLine("Ticket u shtua me sukses!\n");                  // konfirmojmë suksesin
                        }
                        catch (ArgumentException ex) // kapim gabimin nëse titulli ose përshkrimi janë bosh
                        {
                            Console.WriteLine($"Gabim: {ex.Message}\n"); // shfaqim mesazhin e gabimit
                        }
                        break; // dalim nga case-i 1

                    case "2": // opsioni: shiko të gjithë ticketat
                        var allTickets = ticketService.GetAllTickets(); // marrim të gjithë ticketat të renditur sipas prioritetit

                        if (allTickets.Count == 0) // nëse nuk ka asnjë ticket
                        {
                            Console.WriteLine("Nuk u gjet asnjë ticket.\n"); // informojmë përdoruesin
                            break;                                            // dalim pa shtypur asgjë tjetër
                        }

                        Console.WriteLine("\n--- Të gjithë Ticketat (sipas prioritetit) ---"); // titulli i listës
                        foreach (Ticket t in allTickets) // iterojmë mbi çdo ticket
                            Console.WriteLine(t);        // shfaqim ticketin duke thirrur ToString() automatikisht
                        break; // dalim nga case-i 2

                    case "3": // opsioni: filtro sipas prioritetit
                        Priority filter = AskPriority();                  // pyesim përdoruesin për prioritetin e dëshiruar
                        var filtered = ticketService.GetByPriority(filter); // marrim ticketat me atë prioritet

                        // konvertojmë enum-in në etiketë teksti për shfaqje
                        string filterLabel = filter switch
                        {
                            Priority.High => "HIGH",   // prioritet i lartë
                            Priority.Medium => "MEDIUM", // prioritet mesatar
                            Priority.Low => "LOW",    // prioritet i ulët
                            _ => "UNKNOWN" // rast i panjohur (mbrojtje)
                        };

                        Console.WriteLine($"\n--- Ticketat me prioritet {filterLabel} ---"); // titull i listës së filtruar

                        if (filtered.Count == 0) // nëse nuk ka ticket me atë prioritet
                        {
                            Console.WriteLine("Nuk u gjet asnjë ticket për këtë prioritet.\n"); // informojmë përdoruesin
                            break;                                                               // dalim
                        }

                        foreach (Ticket t in filtered) // iterojmë mbi ticketat e filtruar
                            Console.WriteLine(t);      // shfaqim çdo ticket
                        break; // dalim nga case-i 3

                    case "4": // opsioni: fillo ticketin (Todo → InProgress)
                        Console.Write("ID e ticketit: ");                            // kërkojmë ID-në e ticketit
                        if (int.TryParse(Console.ReadLine(), out int startId))       // analizojmë ID-në si numër të plotë
                            Console.WriteLine(ticketService.StartTicket(startId)     // provojmë ta fillojmë
                                ? "Ticket u fillua.\n"                               // sukses: ticket u kalua në InProgress
                                : "Ticket nuk u gjet.\n");                           // dështim: nuk ekziston ticket me atë ID
                        else
                            Console.WriteLine("ID e pavlefshme.\n"); // ID nuk ishte numër i vlefshëm
                        break; // dalim nga case-i 4

                    case "5": // opsioni: mbyll ticketin (→ Done)
                        Console.Write("ID e ticketit: ");                              // kërkojmë ID-në e ticketit
                        if (int.TryParse(Console.ReadLine(), out int completeId))      // analizojmë ID-në
                            Console.WriteLine(ticketService.CompleteTicket(completeId) // provojmë ta mbyllim
                                ? "Ticket u mbyll me sukses.\n"                        // sukses: ticket u kalua në Done
                                : "Ticket nuk u gjet.\n");                             // dështim: nuk ekziston ticket me atë ID
                        else
                            Console.WriteLine("ID e pavlefshme.\n"); // ID nuk ishte numër i vlefshëm
                        break; // dalim nga case-i 5

                    case "6": // opsioni: fshij ticketin
                        Console.Write("ID e ticketit për fshirje: ");                 // kërkojmë ID-në e ticketit për fshirje
                        if (int.TryParse(Console.ReadLine(), out int deleteId))       // analizojmë ID-në
                            Console.WriteLine(ticketService.DeleteTicket(deleteId)    // provojmë ta fshijmë
                                ? "Ticket u fshi me sukses.\n"                        // sukses: ticket u fshi nga lista dhe skedari
                                : "Ticket nuk u gjet.\n");                            // dështim: nuk ekziston ticket me atë ID
                        else
                            Console.WriteLine("ID e pavlefshme.\n"); // ID nuk ishte numër i vlefshëm
                        break; // dalim nga case-i 6

                    case "0": // opsioni: dil nga programi
                        running = false;                    // vendosim flamurin false për të ndaluar ciklin while
                        Console.WriteLine("Mirupafshim!"); // mesazh i lamtumirës
                        break; // dalim nga case-i 0

                    default: // çdo opsion tjetër që nuk njohim
                        Console.WriteLine("Opsion i pavlefshëm. Zgjidh nga 0–6.\n"); // informojmë për opsion të gabuar
                        break; // dalim nga default
                }
            }
        }

        // metodë ndihmëse — pyet përdoruesin të zgjedhë prioritetin dhe kthen enum-in përkatës
        // ripërsërit pyetjen nëse input-i është i pavlefshëm
        static Priority AskPriority()
        {
            while (true) // cikël i pafund derisa jepet input i vlefshëm
            {
                // shfaqim nënmenynë e prioritetit
                Console.WriteLine("Zgjidh prioritetin:"); // titull i nënmenysë
                Console.WriteLine("  1. High");           // opsioni për prioritet të lartë
                Console.WriteLine("  2. Medium");         // opsioni për prioritet mesatar
                Console.WriteLine("  3. Low");            // opsioni për prioritet të ulët
                Console.Write("Opsioni: ");               // ftojmë përdoruesin të japë zgjedhjen
                string? input = Console.ReadLine();        // lexojmë zgjedhjen

                switch (input) // kontrollojmë çfarë zgjodhi përdoruesi
                {
                    case "1": return Priority.High;   // 1 → High, dalim menjëherë nga metoda
                    case "2": return Priority.Medium; // 2 → Medium, dalim menjëherë nga metoda
                    case "3": return Priority.Low;    // 3 → Low, dalim menjëherë nga metoda
                    default:
                        Console.WriteLine("Opsion i pavlefshëm. Provo përsëri.\n"); // opsion i gabuar — ripërsërisim
                        break; // vazhdojmë ciklin while dhe pyesim përsëri
                }
            }
        }
    }
}