using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadWebServer.Controllers
{
public class ItemAPI : Microsoft.AspNetCore.Mvc.Controller
{
    private Data.ApplicationDbContext _dataContext;

    public ItemAPI(Data.ApplicationDbContext dataContext)
    {
        this._dataContext = dataContext;
    }

    [Microsoft.AspNetCore.Authorization.Authorize()]
    public Microsoft.AspNetCore.Mvc.IActionResult Index()
    {
        return Json(_dataContext.Items);
    }
}
}
