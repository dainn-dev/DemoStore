## Getting Started

Clone or download this repository locally (it includes all the files you will need including a fully configured SQLlite database)
Once you have all the files downloaded you can open the `Umbraco.Commerce.DemoStore.sln` solution file in the root of the repository in Visual Studio. Make sure the `Umbraco.Commerce.DemoStore.Web` project is the startup project by right clicking the project in the Solution Explorer and choosing `Set as StartUp Project`, and then press `Ctrl + F5` to launch the site.

*Optional* - Import `.\db\UmbracoCommerceDemoStore_v13.1.0.bacpac` using Data-tier application and update `ConnectionStrings` in `appsettings.json` to be similar like this:
```json
  "ConnectionStrings": {
    "umbracoDbDSN": "Server=.;Database=UmbracoCommerceDemoStore_v13.1.0;User Id={your_db_username};Password={your_db_password};TrustServerCertificate=true;",
    "umbracoDbDSN_ProviderName": "Microsoft.Data.SqlClient"
  }
```

To login to the back office you can do so using the credentails:

* **Email** `admin@example.com`
* **Password** `password1234`
