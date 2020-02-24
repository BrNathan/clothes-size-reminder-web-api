using DAL.Interfaces.Repositories;
using Entities;
using Entities.Extensions;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Services
{
    public class ReminderRepository: RepositoryBase<Reminder>, IReminderRepository
    {
        public ReminderRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        { }

        public IEnumerable<Reminder> GetAllReminders()
        {
            return FindAll();
        }

        public IEnumerable<Reminder> GetAllRemindersByUser(int userId)
        {
            return FindByCondition(r => r.UserId == userId);
        }

        public Reminder GetReminderById(int id)
        {
            return FindByCondition(b => b.Id == id)
                .FirstOrDefault();
        }

        public void UpdateReminder(Reminder dbReminder, Reminder reminder)
        {
            dbReminder.ApplyChange(reminder);
            Update(dbReminder);
        }

        public void CreateReminder(Reminder reminder)
        {
            Create(reminder);
        }

        public void DeleteReminder(Reminder reminder)
        {
            Delete(reminder);
        }
    }
}
