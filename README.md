Scaffold 指令
```
dotnet ef dbcontext scaffold Name=ConnectionStrings:Airdnd Microsoft.EntityFrameworkCore.SqlServer --project Airdnd.Web --namespace Airdnd.Core.Entities --output-dir ..\Airdnd.Core\Entities --context AirBnBContext --context-namespace Airdnd.Infrastructure.Data --context-dir ..\Airdnd.Infrastructure\Data -f
```

-f:代表如果有重複檔案會蓋過去
相關指令說明:https://docs.microsoft.com/zh-tw/ef/core/cli/powershell

Migration 指令
記得改Migration 名稱
```
dotnet ef migrations add 名稱 --project Airdnd.Infrastructure --startup-project Airdnd.Web --output-dir Data\Migrations
```

Update DB
```
dotnet ef database update --project Airdnd.Infrastructure --startup-project Airdnd.Web
```