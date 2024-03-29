﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace TodoApi3.Models
{
    public class TodoContext : DbContext
    { 
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
        public DbSet<Todoitem> TodoItems { get; set; }
    }
}
