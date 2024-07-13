create migration SQL Server
     dotnet ef migrations add CustomerDetail -s ../Para.Api/ --context ParaDbContext
create migration PostgreSQL Server
     dotnet ef migrations add InitialCreate -s ../Para.Api/ --context ParaDbContext    
  
db guncelleme SQL 
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaDbContext
db guncelleme Postgre
     dotnet ef database update --project "./Para.Data" --startup-project "Para.Api/" --context ParaDbContext