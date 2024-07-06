create migration SQL Server
     dotnet ef migrations add CustomerDetail -s ../Para.Api/ --context ParaSqlDbContext
create migration PostgreSQL Server
     dotnet ef migrations add InitialCreate -s ../Para.Api/ --context ParaPostgreDbContext    
  
db guncelleme SQL 
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaSqlDbContext
db guncelleme Postgre
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaPostgreDbContext