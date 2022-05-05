﻿using CapTwitch.Model.Interfaces;
using CapTwitch.Model.Model;
using CapTwitch.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapTwitch.Api.Controllers;

public class GenericController<T> : ControllerBase where T : class, IStoredObject
{
    protected CapTwitchDbContext Ctx;
    private readonly IService<T> _service;
    public GenericController(CapTwitchDbContext ctx, IService<T> service)
    {
        Ctx = ctx;
        _service = service;
    }

    [HttpGet]
    public virtual List<T> All()
    {
        return Ctx.Set<T>().ToList();
    }

    [HttpPost]
    public virtual T Post([FromBody] T se)
    {
        return _service.Add(se);
    }

    [HttpGet("{id}")]
    public T Get(int id)
    {
        return Ctx.Set<T>().Find(id);
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