using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IReminderRepository: IRepositoryBase<Reminder>
    {
        IEnumerable<Reminder> GetAllReminders();
        Reminder GetReminderById(int id);
        void UpdateReminder(Reminder dbReminder, Reminder reminder);
        void DeleteReminder(Reminder reminder);
    }
}
