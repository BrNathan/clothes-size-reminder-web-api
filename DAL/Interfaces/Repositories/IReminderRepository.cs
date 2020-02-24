﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces.Repositories
{
    public interface IReminderRepository: IRepositoryBase<Reminder>
    {
        IEnumerable<Reminder> GetAllReminders();
        IEnumerable<Reminder> GetAllRemindersByUser(int userId);
        Reminder GetReminderById(int id);
        void UpdateReminder(Reminder dbReminder, Reminder reminder);
        void CreateReminder(Reminder reminder);
        void DeleteReminder(Reminder reminder);
    }
}
