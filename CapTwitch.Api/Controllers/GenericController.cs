using CapTwitch.Model.Interfaces;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapTwitch.Api.Controllers;

public class GenericController<T> : ControllerBase where T : class, IStoredObject
{
    protected CapTwitchDbContext Ctx;
    protected readonly IService<T> Service;
    public GenericController(CapTwitchDbContext ctx, IService<T> service)
    {
        Service = service;
        Ctx = ctx;
    }

    [HttpGet]
    public virtual List<T> All()
    {
        return Service.GetAll();
    }

    [HttpPost]
    public virtual T Post([FromBody] T se)
    {
        return Service.Add(se);
    }

    [HttpGet("{id}")]
    public T Get(int id)
    {
        return Service.Find(id);
    }

    [HttpPut("{id}")]
    public T Put(int id, [FromBody] T se)
    {
        se.Id = id;
        var entityEntry = Ctx.Attach(se);
        entityEntry.State = EntityState.Modified;
        Ctx.SaveChanges();
        ; return se;
    }

    [HttpPatch("{id}")]
    public T Patch(int id, [FromBody] JsonPatchDocument<T> patchDocument)
    {
        var baseObject = Ctx.Set<T>().Find(id);
        patchDocument.ApplyTo(baseObject);
        Ctx.SaveChanges();
        return baseObject;
    }

    [HttpDelete("{id}")] // /streamEvent/5
    public ActionResult<bool> Delete(int id)
    {
        var se = Ctx.Set<T>().SingleOrDefault(streamEvent => streamEvent.Id == id);
        if (se == null)
        {
            return BadRequest(false);
        }
        Ctx.Set<T>().Remove(se);
        Ctx.SaveChanges();
        return Ok(true);
    }
}