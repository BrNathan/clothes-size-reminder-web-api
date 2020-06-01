using Entities.Models;
using System.Collections.Generic;

namespace DAL.Interfaces.Repositories
{
    public interface IReminderRepository : IRepositoryBase<Reminder>
    {
        IEnumerable<Reminder> GetAllReminders();
        IEnumerable<Reminder> GetAllRemindersByUser(int userId);
        Reminder GetReminderById(int id);
        void UpdateReminder(Reminder dbReminder, Reminder reminder);
        void CreateReminder(Reminder reminder);
        void DeleteReminder(Reminder reminder);
    }
}
