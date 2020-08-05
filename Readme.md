git init
git add README.md
git commit -m "first commit"
git remote add origin https://github.com/atul-sachan/AmazingShop.git
git push -u origin master

dotnet new sln --name AmazingShop
dotnet new webapi --name AmazingShop.Api
dotnet new classlib --name AmazingShop.Core
dotnet sln add src/AmazingShop.Core/AmazingShop.Core.csproj src/AmazingShop.Api/AmazingShop.Api.csproj
dotnet add src/AmazingShop.Core/AmazingShop.Core.csproj reference src/AmazingShop.Api/AmazingShop.Api.csproj


dotnet build
dotnet restore
dotnet clean
dotnet run --project src/AmazingShop.Api/AmazingShop.Api.csproj


dotnet ef database drop -p AmazingShop.Infrastructure -s AmazingShop.Api
dotnet ef migrations remove -p AmazingShop.Infrastructure -s AmazingShop.Api
dotnet ef migrations add initial-migration -p AmazingShop.Infrastructure -s AmazingShop.Api -o Data/Migrations
dotnet ef database update -p AmazingShop.Infrastructure -s AmazingShop.Api