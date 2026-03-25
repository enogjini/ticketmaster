# 🎫 Ticket System

## 📌 Përshkrimi i Projektit

Ticket System është një aplikacion konsoli i shkruar në C# që lejon
menaxhimin e detyrave (tickets) përmes një ndërfaqe të thjeshtë.
Projekti ndërtohet sipas parimeve Agile dhe mundëson shtimin, shikimin
dhe mbylljen e ticketave.

## 🗂️ Informacioni Agile i Projektit

-   **Sprint:** Sprint 2 --- Validimi & Menaxhimi bazë\
-   **Versioni:** v1.1.0\
-   **Gjuha:** C# / .NET Console Application\


## 📖 User Stories

-   **US-01:** Shto Ticket ✅\
-   **US-02:** Shiko Ticketat ✅\
-   **US-03:** Mbyll Ticket ✅\
-   **US-04:** Validimi i Fushave ✅

## ✅ Kriteret e Pranimit

### Shto Ticket

-   Titulli dhe përshkrimi kërkohen
-   Fushat bosh japin gabim
-   Statusi fillestar: \[PENDING\]

### Shiko Ticketat

-   Shfaq listën me status
-   Nëse bosh: "Nuk u gjet asnjë ticket."

### Mbyll Ticket

-   Zgjedhje sipas numrit
-   Gabim nëse jashtë rangut
-   Statusi bëhet \[DONE\]

### Validimi

-   Nuk lejohen fusha bosh
-   Përdoret ArgumentException
-   Validim në Program.cs dhe Ticket

## 🏗️ Arkitektura

-   **Ticket** -- Model
-   **TicketManager** -- Logjikë
-   **Program** -- Entry point

## 🏁 Definition of Done

-   Kodi funksional pa gabime
-   Validim i implementuar
-   User stories të testuara
-   PR dhe Code Review
-   README i përditësuar


