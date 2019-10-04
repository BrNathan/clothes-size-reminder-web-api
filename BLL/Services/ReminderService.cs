using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ReminderService: IReminderService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(RepositoryContext repositoryContext, IReminderRepository reminderRepository)
        {
            this._repositoryContext = repositoryContext;
            this._reminderRepository = reminderRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateReminder(Reminder reminder)
        {
            _reminderRepository.Create(reminder);
        }

        public void DeleteReminder(Reminder reminder)
        {
            _reminderRepository.DeleteReminder(reminder);
        }

        public IEnumerable<Reminder> GetAllReminders()
        {
            return _reminderRepository.GetAllReminders();
        }

        public Reminder GetReminderById(int reminderId)
        {
            return _reminderRepository.GetReminderById(reminderId);
        }

        public void UpdateReminder(Reminder dbReminder, Reminder reminder)
        {
            _reminderRepository.UpdateReminder(dbReminder, reminder);
        }
    }
}
