using System;                     // importojmë DateTime dhe tipe bazë
using System.Collections.Generic; // importojmë List<T> dhe IEnumerable<T>
using System.IO;                   // importojmë File.ReadAllText dhe File.WriteAllText
using System.Linq;                 // importojmë Select, FirstOrDefault, Max
using System.Text.Json;            // importojmë JsonSerializer
using TicketMaster.Domain;         // importojmë Ticket, Priority, TicketStatus

namespace TicketMaster.Application // namespace i shtresës Application
{
    // implementim i ITicketRepo që ruan ticketat në një skedar JSON
    // çdo operacion shkrimor e përditëson skedarin menjëherë
    public class JsonTicketRepo : ITicketRepo
    {
        private readonly string _filePath;       // rruga e skedarit JSON ku ruhen ticketat
        private readonly List<Ticket> _tickets;  // cache në memorie e ticketave të ngarkuara nga JSON

        // konstruktor — merr rrugën e skedarit (parazgjedhja: "tickets.json" në direktorinë aktuale)
        public JsonTicketRepo(string filePath = "tickets.json")
        {
            _filePath = filePath;  // ruan rrugën e skedarit për përdorim të mëvonshëm
            _tickets = Load();   // ngarkon ticketat ekzistues nga skedari (ose listë boshe nëse nuk ekziston)
        }

        // shton ticketin në listën e cache-it dhe e shkruan menjëherë në skedar
        public void Add(Ticket ticket)
        {
            _tickets.Add(ticket); // shton ticketin në listën në memorie
            Save();               // e serializojn të gjithë listën dhe e shkruan në JSON
        }

        // kthen listën e plotë të ticketave nga cache-i (pa lexuar skedarin përsëri)
        public List<Ticket> GetAll()
        {
            return _tickets; // cache-i është gjithmonë i sinkronizuar me skedarin
        }

        // kërkon ticketin me ID të caktuar në cache
        public Ticket? GetById(int id)
        {
            return _tickets.FirstOrDefault(t => t.Id == id); // kthen ticketin ose null nëse nuk gjendet
        }

        // fshin ticketin me ID të caktuar dhe e shkruan ndryshimin në skedar
        public bool Delete(int id)
        {
            Ticket? ticket = _tickets.FirstOrDefault(t => t.Id == id); // kërkon ticketin në cache

            if (ticket == null) return false; // nëse nuk u gjet, kthen false pa shkruar në skedar

            _tickets.Remove(ticket); // heq ticketin nga cache-i

            Save(); // rifreskojmë skedarin JSON me listën e përditësuar

            return true; // fshirja u krye me sukses
        }

        // ── Serializim / Deserializim ────────────────────────────────────────────────────

        // DTO (Data Transfer Object) i brendshëm — përdoret vetëm për lexim/shkrim JSON
        // nevojitet sepse Ticket ka "private set" dhe nuk mund të deserializohet drejtpërdrejt
        private record TicketDto(
            int Id,            // ID e ticketit
            string Title,      // titulli
            string Description,// përshkrimi
            DateTime DueDate,  // afati
            Priority Priority, // prioriteti (ruhet si numër i enum-it)
            TicketStatus Status // statusi (ruhet si numër i enum-it)
        );

        // shkruan të gjithë ticketat nga cache-i në skedarin JSON
        private void Save()
        {
            IEnumerable<TicketDto> dtos = _tickets.Select(t => // konvertojmë çdo Ticket në DTO për serializim
                new TicketDto(                                  // krijojmë DTO me të gjitha fushat e ticketit
                    t.Id,           // kopjojmë ID-në
                    t.Title,        // kopjojmë titullin
                    t.Description,  // kopjojmë përshkrimin
                    t.DueDate,      // kopjojmë afatin
                    t.Priority,     // kopjojmë prioritetin
                    t.Status));     // kopjojmë statusin aktual

            string json = JsonSerializer.Serialize(           // konvertojmë listën e DTO-ve në varg JSON
                dtos,                                         // të dhënat që do të serializohen
                new JsonSerializerOptions { WriteIndented = true }); // formatim me indentim për lexueshmëri

            File.WriteAllText(_filePath, json); // shkruajmë JSON-in në skedar (krijon ose mbishkruan)
        }

        // lexon ticketat nga skedari JSON dhe i rindërton si objekte Ticket
        private List<Ticket> Load()
        {
            if (!File.Exists(_filePath))          // nëse skedari nuk ekziston ende (hera e parë)
                return new List<Ticket>();         // kthejmë listë boshe pa gabime

            string json = File.ReadAllText(_filePath);                       // lexojmë përmbajtjen e skedarit si tekst
            List<TicketDto>? dtos = JsonSerializer.Deserialize<List<TicketDto>>(json); // deserializojmë JSON-in në listë DTO-sh

            if (dtos == null) return new List<Ticket>(); // nëse deserializimi dha null (skedar i korruptuar), kthejmë listë boshe

            return dtos.Select(d =>                               // konvertojmë çdo DTO në objekt Ticket
                Ticket.Restore(                                   // përdorim Restore() sepse Status ka private set
                    d.Id,           // ID e ruajtur
                    d.Title,        // titulli i ruajtur
                    d.Description,  // përshkrimi i ruajtur
                    d.DueDate,      // afati i ruajtur
                    d.Priority,     // prioriteti i ruajtur
                    d.Status))      // statusi i ruajtur (Restore e aplikon saktë)
                .ToList();          // kthejmë rezultatin si List<Ticket>
        }
    }
}