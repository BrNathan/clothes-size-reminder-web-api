using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extensions
{
    public static class ReminderExtensions
    {
        public static void ApplyChange(this Reminder dbReminder, Reminder reminder)
        {
            //if (!String.IsNullOrWhiteSpace(reminder.Code))
            //{
            //    dbReminder.Code = reminder.Code;
            //}
            //if (!String.IsNullOrWhiteSpace(reminder.Label))
            //{
            //    dbReminder.Label = reminder.Label;
            //}
        }
    }
}
