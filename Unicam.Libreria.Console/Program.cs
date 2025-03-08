
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Unicam.Libreria.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Unicam.Libreria.Console;

string connectionString = "Data Source=localhost;User Id=libreria_user;Password=libreria_user;Trust Server Certificate=true";    //stringa di connnessione con il DB

var builder = Host.CreateDefaultBuilder(args);     //builder dell'applicazione: imposta un host generico per l'applicazione, utile per i servizi .NET

builder.ConfigureServices(services =>              //configura servizi: andiamo a registrare e specializzare i servizi che servono nell'app
{
    services.AddDbContext<MyDbContext>(opt =>       //aggiunge il DB context all'iniezione delle dipendenze
    {
        opt.UseLazyLoadingProxies();                //abilita il lazy loading
        opt.UseSqlServer(connectionString);         //configura il DB context per usare SQL Server con la stringa di connessione specificata
    });
    services.AddScoped<MainService>();               //aggiunge il servizio MainService all'iniezione delle dipendenze
});

var app = builder.Build();                          //crea l'applicazione

var mainService = app.Services.GetRequiredService<MainService>();    //recupera il servizio MainService dall'applicazione

await mainService.ExecuteAsync();                              //esegue il servizio

//var context = new MyDbContext();                   NON SERVE PIU' PERCHE' E' GIA' STATO AGGIUNTO COME SERVIZIO
