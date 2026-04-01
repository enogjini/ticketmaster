# TicketMaster

Sistem menaxhimi ticketash i ndërtuar me C#, bazuar në arkitekturën me shtresa (Layered Architecture).

---

## Struktura e Projektit

```
ticketmaster/
├── Domain/
│   └── Ticket.cs               # Model + enum Priority + enum TicketStatus
├── Application/
│   ├── TicketRepo.cs           # Interface ITicketRepo
│   ├── InMemoryTicketRepo.cs   # Implementim në memorie
│   └── TicketManager.cs        # Logjika e biznesit
└── ConsoleUI/
    └── Program.cs              # UI dhe entry point
```

---

## Veçoritë e Reja

### 1. Prioritetet e Ticketave
Çdo ticket ka një nivel prioriteti të caktuar gjatë krijimit:

| Prioritet | Etiketa |
|-----------|---------|
| High      | 🔴 HIGH   |
| Medium    | 🟡 MEDIUM |
| Low       | 🟢 LOW    |

### 2. Statuset e Ticketave
Çdo ticket kalon nëpër tre faza:

```
Todo  →  InProgress  →  Done
```

- **Todo** — ticket i sapo krijuar
- **InProgress** — ticket i filluar (opsioni "Fillo ticket")
- **Done** — ticket i mbyllur (opsioni "Mbyll ticket")

### 3. Renditja Automatike sipas Prioritetit
Kur shikon të gjithë ticketat (opsioni 2), lista renditet automatikisht:
**High → Medium → Low**, pavarësisht rendit të krijimit.

### 4. Filtrimi sipas Prioritetit
Opsioni 3 lejon shikimin e ticketave vetëm për një prioritet të zgjedhur (High, Medium ose Low).

### 5. ID dhe Afati (DueDate)
Çdo ticket ka:
- **ID** unike të gjeneruar automatikisht
- **Afat** (DueDate) të vendosur gjatë krijimit në formatin `yyyy-MM-dd`

### 6. Repository Pattern
Logjika e ruajtjes është e ndarë nga logjika e biznesit përmes interface-it `ITicketRepo`. Kjo lejon zëvendësimin e `InMemoryTicketRepo` me një implementim tjetër (p.sh. bazë të dhënash) pa ndryshuar pjesën tjetër të kodit.

---

## Menyja

```
=== Sistemi i Ticketave ===
1. Shto ticket
2. Shiko të gjithë ticketat
3. Shiko sipas prioritetit
4. Fillo ticket
5. Mbyll ticket
0. Dil
```

---

## Teknologjitë

- **Gjuha:** C# (.NET)
- **Arkitektura:** Layered Architecture (Domain / Application / ConsoleUI)
- **Ruajtja:** In-Memory (List&lt;Ticket&gt;)
