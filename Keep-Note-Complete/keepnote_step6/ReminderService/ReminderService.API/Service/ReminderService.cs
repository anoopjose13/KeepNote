using System;
using System.Collections.Generic;
using ReminderService.API.Exceptions;
using ReminderService.API.Models;
using ReminderService.API.Repository;

namespace ReminderService.API.Service
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository reminderRepository;
        public ReminderService(IReminderRepository _reminderRepository)
        {
            reminderRepository = _reminderRepository;
        }
        /// <summary>
        /// Used to create reminder
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns></returns>
        public Reminder CreateReminder(Reminder reminder)
        {
            try
            {
                var result = reminderRepository.CreateReminder(reminder);
                if (result == null)
                {
                    throw new ReminderNotCreatedException("This reminder id already exists");
                }
                else
                    return result;

            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to delete reminder
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        public bool DeleteReminder(int reminderId)
        {
            try
            {
                if (!reminderRepository.DeleteReminder(reminderId))
                {
                    throw new ReminderNotFoundException("This reminder id not found");
                }
                else
                { return true; }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Used to get all reminders
        /// </summary>
        /// <returns></returns>
        public List<Reminder> GetAllReminders()
        {

            try
            {
                var result = reminderRepository.GetAllReminders();
                if (result == null)
                    throw new ReminderNotFoundException("This reminder does not exist");
                else return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        /// <summary>
        /// Used to get reminder by ID
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        public Reminder GetReminderById(int reminderId)
        {
            try
            {
                var result = reminderRepository.GetReminderById(reminderId);
                if (result == null)
                    throw new ReminderNotFoundException("This reminder id not found");
                else return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        /// <summary>
        /// Update reminder by ID
        /// </summary>
        /// <param name="reminderId"></param>
        /// <param name="reminder"></param>
        /// <returns></returns>
        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            try
            {
                var result = reminderRepository.UpdateReminder(reminderId, reminder);
                if (!result)
                {
                    throw new ReminderNotFoundException("This reminder id not found");
                }
                else
                    return result;
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
