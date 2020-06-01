using Entities.Models;
using System;

namespace Entities.Extensions
{
    public static class ReminderExtensions
    {
        public static void ApplyChange(this Reminder dbReminder, Reminder reminder)
        {
            dbReminder.ClothesSizeId = reminder.ClothesSizeId;
            dbReminder.BrandId = reminder.BrandId;
            dbReminder.UserId = reminder.UserId;
            if (!String.IsNullOrWhiteSpace(reminder.Comments))
            {
                dbReminder.Comments = reminder.Comments;
            }
        }
    }
}
