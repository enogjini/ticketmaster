using TicketMaster.Application; // importojmë namespace-n Application

namespace TicketMaster.ConsoleUI // namespace për UI
{
    public static class TicketRepoFactory // klasë statike për Factory Pattern
    {
        public static ITicketRepo Create(string type = "json") // metodë për të krijuar repo sipas llojit
        {
            return type.ToLower() switch // kontrollojmë llojin e kërkuar
            {
                "json" => new JsonTicketRepo(), // kthejmë JsonTicketRepo nëse kërkohet "json"
                "memory" => new InMemoryTicketRepo(), // kthejmë InMemoryTicketRepo nëse kërkohet "memory"
                _ => new JsonTicketRepo() // parazgjedhje: JsonTicketRepo
            };
        }
    }
}
