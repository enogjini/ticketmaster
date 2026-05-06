1.	Nisja e Programit
•	Programi nis me metodën Main(string[]), ku krijohet një repo për ticketat (JSON ose memorie) përmes një fabrike (Factory Pattern).
•	Krijohet një instancë unike e shërbimit të menaxhimit të ticketave (Singleton Pattern).
2.	Menuja Kryesore
•	Përdoruesi sheh një menu me opsione për të shtuar, parë, filtruar, filluar, mbyllur ose fshirë ticketat.
•	Çdo opsion përpunohet me një switch, ku thirren metodat përkatëse të shërbimit.
3.	Shtimi i një Ticketi
•	Përdoruesi jep titullin, përshkrimin, afatin dhe prioritetin.
•	Nëse të dhënat janë të vlefshme, krijohet një objekt i ri Ticket dhe ruhet në repo.
4.	Shfaqja dhe Filtrimi
•	Mund të shfaqen të gjithë ticketat ose vetëm ata me një prioritet të caktuar.
•	Ticketat renditen sipas prioritetit.
5.	Ndryshimi i Statusit
•	Përdoruesi mund të kalojë një ticket nga "Todo" në "InProgress" ose nga "InProgress" në "Done" duke dhënë ID-në.
6.	Fshirja e Ticketit
•	Ticketat mund të fshihen nga lista dhe nga skedari JSON duke dhënë ID-në.
7.	Ruajtja e të Dhënave
•	Nëse përdoret repo JSON, çdo ndryshim ruhet menjëherë në skedar.
•	Nëse përdoret repo në memorie, të dhënat humbasin pas mbylljes së programit.
8.	Strukturat Kryesore
•	Ticket: Përfaqëson një detyrë me ID, titull, përshkrim, afat, prioritet dhe status.
•	ITicketRepo: Interface për ruajtjen e ticketave.
•	JsonTicketRepo dhe InMemoryTicketRepo: Implementime të ndryshme të ruajtjes.
•	TicketManager: Përgjegjës për logjikën e biznesit dhe menaxhimin e ticketave.
•	TicketRepoFactory: Krijon repo-në sipas zgjedhjes.
9.	Pattern-et e përdorura
•	Factory Pattern: Për të krijuar repo-në sipas llojit.
•	Singleton Pattern: Për të siguruar që ekziston vetëm një instancë e TicketManager.
Ky program është i thjeshtë për t’u përdorur dhe mund të zgjerohet lehtësisht për nevoja të tjera të menaxhimit të detyrave.
