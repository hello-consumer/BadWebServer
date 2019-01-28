using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BadWebServer.Controllers
{
public class HomeController : Microsoft.AspNetCore.Mvc.Controller
{
    private Data.ApplicationDbContext _dataContext;

    public HomeController(Data.ApplicationDbContext dataContext)
    {
        this._dataContext = dataContext;
    }

    public Microsoft.AspNetCore.Mvc.IActionResult Index()
    {
        return View(_dataContext.Items.ToList());
    }

    [Microsoft.AspNetCore.Mvc.HttpPost]
    public Microsoft.AspNetCore.Mvc.IActionResult Create(string name)
    {
        _dataContext.Items.Add(new Data.Item { Value = name });
        _dataContext.SaveChanges();
        return RedirectToAction("Index");
    }
        
    [Microsoft.AspNetCore.Mvc.HttpPost]
    public Microsoft.AspNetCore.Mvc.IActionResult Delete(string name)
    {
        _dataContext.Items.Remove(_dataContext.Items.First(x => x.Value == name));
        _dataContext.SaveChanges();
        return RedirectToAction("Index");
    }
}
}
