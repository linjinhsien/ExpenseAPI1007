# Expense專案

## 專案描述
ExpenseAPI 是一個簡單的 ASP.NET Core Web API，用於管理支出。

## 專案的架構

- .NET 6.0 SDK
- ASP.NET Core
- Entity Framework Core
- Swashbuckle (Swagger)

## 專案的安裝

1. 克隆此倉庫：
    ```sh
    git clone https://github.com/yourusername/ExpenseAPI.git
    cd ExpenseAPI
    ```

2. 還原依賴項：
    ```sh
    dotnet restore
    ```

3. 更新數據庫：
    ```sh
    dotnet ef database update
    ```

4. 運行應用程序：
    ```sh
    dotnet run
    ```

應用程序將在 `https://localhost:5001` 上可用。

## API 端點

- **GET** `/api/Expense` - 獲取所有支出
- **GET** `/api/Expense/{id}` - 根據 ID 獲取特定支出
- **POST** `/api/Expense` - 創建新支出
- **PUT** `/api/Expense/{id}` - 根據 ID 更新支出
- **DELETE** `/api/Expense/{id}` - 根據 ID 刪除支出

## 專案文件

- [ExpenseController.cs](ExpenseAPI/Controllers/ExpenseController.cs): 處理支出相關的 HTTP 請求。
- [ExpenseContext.cs](ExpenseAPI/Models/ExpenseContext.cs): Entity Framework Core 的支出上下文。
- [Expense.cs](ExpenseAPI/Models/Expense.cs): 支出模型。

## 授權

此項目基於 MIT 許可證 - 詳見 [LICENSE.txt](LICENSE.txt) 文件。