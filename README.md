# MvcJson.Net

ASP.NET MVC project with better defaults for Newtonsoft Json.Net library

## Why use this?
The [Json protocol](http://Json.org) has a vulnerability which can result in disclosing sensitive information. 
This exploit has being well covered by many others, with a good summary provided by 
[Phil Haack](http://haacked.com/) found here: [Json hijacking](http://haacked.com/archive/2009/06/25/json-hijacking.aspx).

## Usage
The code usage is seamless with the added benefit of integrating Json.Net and being safe from the security exploit.
```CSharp
public ActionResult Index()
{
    var custs = Customer.GetAll();
    if (Request.IsAjaxRequest())
    {
        return Json(custs);
    }
    return View(custs);
}
```

## Documentation
Blog posts that describe the code in detail.

* [A Safer Json in .NET Part 1] (http://tech.ngensoft.net/post/23464196179)
* [A Safer Json in .NET Part 2] (http://tech.ngensoft.net/post/25251258199)