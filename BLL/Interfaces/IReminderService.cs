using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    public interface IReminderService
    {
        IEnumerable<Reminder> GetAllReminders();
        IEnumerable<Reminder> GetAllRemindersByUser(int userId);
        Reminder GetReminderById(int reminderId);
        void CreateReminder(Reminder reminder);
        void UpdateReminder(Reminder dbReminder, Reminder reminder);
        void DeleteReminder(Reminder reminder);
        void Save();
    }
}
