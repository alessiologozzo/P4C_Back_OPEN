# P4C_BACK
## Funzionalità dell'applicazione
Applicazione back-end .NET 8 destinata ad interfacciarsi con il componente front-end P4C_FRONT_OPEN.
L'applicazione permette di eseguire le operazioni CRUD delle entità Report, Kpi e Criterio ed è protetta attraverso Windows Authentication.
Questa è la versione open di un'applicazione attualmente in produzione.

## Istruzioni per l'installazione
- Lanciare lo script db.sql presente in questa cartella per creare il database e inserire alcuni valori di test
- Modificare a mano il database appena creato ed inserire l'id dell'utenza Windows attiva sulla macchina nel campo Utente della tabella tbl_abilitazioni (Il valore di default è "Alessio"). L'applicazione accetta solamente le richieste provenienti da utenti validati e presenti in questa tabella, di conseguenza se si omette questo passaggio tutte le richieste verranno respinte (per ottenere il proprio username digitare il comando whoami da linea di comando, e rilevare la stringa che segue l'ultimo backslash)
- In caso di necessità modificare il server e le credenziali di accesso alla propria istanza MySQL nella AppDbConnectionString dentro appsettings.json
``` "AllowedHosts": "*",
   "ConnectionStrings": {
       "AppDbConnectionString": "server=localhost;database=P4C_OPEN_db;User=root;Password=;"
   }
```
- In caso di necessità modificare il server e la porta di accesso in cui sta girando P4C_FRONT_OPEN in policy.WithOrigins() dentro Program.cs
```
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsFrontApp,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyMethod().AllowAnyHeader();
                      });
});
```
- Lanciare l'applicazione
