using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using Tasks.Configurations;
using Tasks.Email;
using Tasks.Email.Messages;
using Tasks.Reminders;
using Tasks.Services.Interfaces;
using Tasks.Validation;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

Console.WriteLine($"Sending out reminders on {DateTime.Now.ToLocalTime()}");

TasksRemindersCliArgs cliArgs = Parser.Default.ParseArguments<TasksRemindersCliArgs>(args).Value;

RemindersController controller = new(cliArgs);

// inject all the dependencies into the services
ServiceProvider serviceProvider = controller.GetServiceProvider();

var userServices = serviceProvider.GetService<IUserServices>();
var reminderServices = serviceProvider.GetService<IRemiderServices>();
var configs = serviceProvider.GetService<IConfigs>();

// need to make a date range to use for the recurrences filter
ValidDateRange validDateRange = new()
{
    StartsOn = DateTime.Now,
    EndsOn = DateTime.Now,
};

// get a list of all the users whose email's are confirmed and they elected to receive daily reminders
var users = (await userServices.GetUsersWithRemindersAsync());

// get the recurrences for each of the users
var recurrencesForUsers = await reminderServices.GetRecurrencesForUsersAsync(users, validDateRange);

// send each user an email of their recurrences
EmailServer emailServer = new(configs);
emailServer.Connect();

foreach (var userRecurrences in recurrencesForUsers)
{
    DailyRecurrencesMessage message = new(configs, userRecurrences);

    try
    {
        await emailServer.SendMessageAsync(message);
    }
    catch (SmtpException smtpException)
    {
        Console.WriteLine($"MAIL EXCEPTION: {smtpException.ToString()}");
    }
}

emailServer.CloseConnection();