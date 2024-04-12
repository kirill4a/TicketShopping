# Ticket Shopping

## Steps to delivery
### Database
>+ Change connections string values (keeping key name _Default_) in appsettings.json files at WebApi and Migrator.MySql projects 
>
> ` "ConnectionStrings": {
    "Default": "Server=localhost;Database=TicketShopping;Uid=yourUser;Pwd=yourPwd;"
  }`
>
>+ Create MySQL database manually, name it as specified in connection strings
>+ Run `TicketShopping.Persistence.Migrator.MySql` console project to seed database structure

### WebApi
>+ Replace _AviationstackSettings.AccessToken_ value in appsettings.json file at WebApi project
>
>  ` "AviationstackSettings": {
    "BaseUrl": "http://api.aviationstack.com/v1/",
    "AccessToken": "YOUR_TOKEN_HERE"
  }`
>+ Run `TicketShopping.WebApi` project
>+ Call `/api/Airports/import` endpoint to populate database from Aviationstack
 

## How to use API
+ Feel free to call `/api/Airports/search` GET endpoint with query string `search` to get airports by name or IATA or ICAO codes, e.g. 
  `https://localhost:7224/api/Airports/search?query=AAA'`