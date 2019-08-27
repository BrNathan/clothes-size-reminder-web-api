using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Services
{
    public class ReminderRepository: RepositoryBase<Reminder>, IReminderRepository
    {
        public ReminderRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }
    }
}
