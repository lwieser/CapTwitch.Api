﻿using System.Linq.Expressions;
using CapTwitch.Api.Model;

namespace CapTwitch.Api.Controllers;

public interface IRepository<T> where T : class, IStoredObject
{
    T Add(T obj);
    T Get(Expression<Func<T, bool>> condition);
}