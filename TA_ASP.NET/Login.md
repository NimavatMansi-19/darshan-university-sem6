# Implementing Login Functionality in ASP.NET Core

## Prerequisite

* **Stored Procedure: `PR_User_ValidateLogin`**

```sql
CREATE PROCEDURE [dbo].[PR_User_ValidateLogin]
    @Username VARCHAR(50),
    @Password VARCHAR(500)
AS
BEGIN
    SELECT 
        [dbo].[User].[UserID], 
        [dbo].[User].[Username], 
        [dbo].[User].[Email]
    FROM 
        [dbo].[User] 
    WHERE 
        [dbo].[User].[Username] = @Username 
        AND [dbo].[User].[Password] = @Password;
END
```

---

## Step 1: Layout Setup in `_Layout_Login.cshtml`

###### `Views/Shared/_Layout_Login.cshtml`

```html
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <title>Pages / Login - NiceAdmin Bootstrap Template</title>

    <link href="~/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <link href="~/css/style.css" rel="stylesheet">
</head>
<body>
    @RenderBody()

    <script src="~/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="~/js/main.js"></script>
    @await RenderSectionAsync("Scripts", required: false)
</body>
</html>
```

---

## Step 2: Create the `UserLoginModel`

###### `Models/UserLoginModel.cs`

```csharp
public class UserLoginModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}
```

---

## Step 3: Implement Login Logic in Controller

###### `Controllers/UserController.cs`

```csharp
public IActionResult UserLogin(UserLoginModel userLoginModel)
{
    try
    {
        if (ModelState.IsValid)
        {
            string connectionString = this._configuration.GetConnectionString("ConnectionString");

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("PR_User_ValidateLogin", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@Username", SqlDbType.VarChar).Value = userLoginModel.Username;
            sqlCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = userLoginModel.Password;

            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read()) 
            {
                HttpContext.Session.SetString("UserID", reader["UserID"].ToString());
                HttpContext.Session.SetString("UserName", reader["UserName"].ToString());
                HttpContext.Session.SetString("EmailAddress", reader["Email"].ToString());

                reader.Close();
                sqlConnection.Close();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                reader.Close();
                sqlConnection.Close();

                TempData["ErrorMessage"] = "User is not found";
                return RedirectToAction("Login", "User");
            }
        }
    }
    catch (Exception e)
    {
        TempData["ErrorMessage"] = e.Message;
    }

    return RedirectToAction("Login");
}
```

---

## ✅ What is HttpContext in ASP.NET Core?

HttpContext represents all the information about a single HTTP request and response in your web application.

## ✅ What is HttpContext.Session?

HttpContext.Session is used in ASP.NET Core to store user-specific data temporarily on the server during a user’s session.

---

## Step 4: Login View

###### `Views/User/Login.cshtml`

```html
@model UserLoginModel

<form asp-action="UserLogin" asp-controller="User">
    
    <label asp-for="Username"></label>
    <input asp-for="Username" />
    <span asp-validation-for="Username"></span>

    <label asp-for="Password"></label>
    <input type="password" asp-for="Password" />
    <span asp-validation-for="Password"></span>

    <button type="submit">Login</button>

</form>
```

---

## Step 5: Logout

###### `Controllers/UserController.cs`

```csharp
public IActionResult Logout()
{
    HttpContext.Session.Clear();
    return RedirectToAction("Login", "User");
}
```

---

## Step 6: CheckAccess (Authorization Filter)

###### `Filters/CheckAccess.cs` *(or project root)*

```csharp
public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Session.GetString("UserID") == null)
        {
            context.Result = new RedirectResult("~/User/Login");
        }
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
        context.HttpContext.Response.Headers["Expires"] = "-1";
        context.HttpContext.Response.Headers["Pragma"] = "no-cache";

        base.OnResultExecuting(context);
    }
}
```

###### Usage

###### `Controllers/HomeController.cs`

```csharp
[CheckAccess]
public class HomeController : Controller
{
}
```

---

## Step 7: CommonVariable Helper

###### `Helpers/CommonVariable.cs`

```csharp
public class CommonVariable
{
    private static IHttpContextAccessor _HttpContextAccessor = new HttpContextAccessor();

    public static int? UserID()
    {
        if (_HttpContextAccessor.HttpContext.Session.GetString("UserID") == null)
            return null;

        return Convert.ToInt32(_HttpContextAccessor.HttpContext.Session.GetString("UserID"));
    }

    public static string UserName()
    {
        return _HttpContextAccessor.HttpContext.Session.GetString("UserName");
    }

    public static string Email()
    {
        return _HttpContextAccessor.HttpContext.Session.GetString("EmailAddress");
    }
}
```

---

## Step 8: Configure Session in Program.cs

###### `Program.cs`

```csharp
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
```

```csharp
app.UseSession();
```

---

# ✅ Final Flow

```
User enters Username & Password
        ↓
Controller calls Stored Procedure
        ↓
If valid → Session created
        ↓
User redirected to Home
        ↓
[CheckAccess] protects pages
        ↓
Logout clears session
```
