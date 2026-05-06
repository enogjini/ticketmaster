using System.Collections.Generic; // importojmë koleksionet gjeneriket
using TicketMaster.Domain;        // importojmë modelin Ticket dhe enum-et

namespace TicketMaster.Application // namespace i shtresës Application
{
    public interface ITicketRepo // interface që përcakton kontratën për çdo implementim repo-je
    {
        void Add(Ticket ticket); // shton një ticket të ri në ruajtje
        List<Ticket> GetAll(); // kthen listën e plotë të të gjithë ticketave
        Ticket? GetById(int id); // kërkon dhe kthen ticketin me ID të caktuar (ose null nëse nuk gjendet)
        bool Delete(int id); // fshin ticketin me ID të caktuar; kthen true nëse u fshi, false nëse nuk u gjet
    }
}