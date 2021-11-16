using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ReminderService.API.Models;

namespace ReminderService.API.Repository
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly IReminderContext reminderContext;
        public ReminderRepository(IReminderContext _reminderContext)
        {
            reminderContext = _reminderContext;
        }
        /// <summary>
        /// create the reminder
        /// </summary>
        /// <param name="reminder"></param>
        /// <returns></returns>
        public Reminder CreateReminder(Reminder reminder)
        {
            reminderContext.Reminders.InsertOne(reminder);
            FilterDefinition<Reminder> filter = Builders<Reminder>.Filter.Eq(u => u.Id, reminder.Id);
            return reminderContext.Reminders.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// used to delete reminder
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        public bool DeleteReminder(int reminderId)
        {
            var filter = Builders<Reminder>.Filter.Eq(u => u.Id, reminderId);

            var result = reminderContext.Reminders.DeleteOneAsync(filter).Result;
            return result.IsAcknowledged;
        }
        /// <summary>
        /// used to get all reminder
        /// </summary>
        /// <returns></returns>
        public List<Reminder> GetAllReminders()
        {
            var reminders = reminderContext.Reminders.Find(Builders<Reminder>.Filter.Empty).ToList();
            return reminders;
        }
        /// <summary>
        /// used to get reminder by ID
        /// </summary>
        /// <param name="reminderId"></param>
        /// <returns></returns>
        public Reminder GetReminderById(int reminderId)
        {
            FilterDefinition<Reminder> filter = Builders<Reminder>.Filter.Eq(u => u.Id, reminderId);
            return reminderContext.Reminders.Find(filter).FirstOrDefault();
        }
        /// <summary>
        /// Update reminder by ID
        /// </summary>
        /// <param name="reminderId"></param>
        /// <param name="reminder"></param>
        /// <returns></returns>
        public bool UpdateReminder(int reminderId, Reminder reminder)
        {
            var filter = Builders<Reminder>.Filter.Eq(u => u.Id, reminderId);
            var update = Builders<Reminder>.Update
                .Set("Description", reminder.Description).Set("Name", reminder.Name).Set("Type", reminder.Type).Set("CreatedBy", reminder.CreatedBy).Set("CreationDate", reminder.CreationDate);
            var result = reminderContext.Reminders.UpdateOneAsync(filter, update).Result;
            return result.IsAcknowledged;
        }
    }
}
