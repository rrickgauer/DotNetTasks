﻿using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Tasks.Domain.Views;
using Tasks.Reminders;
using Tasks.Services.Implementations;
using Tasks.Services.Interfaces;
using Tasks.Validation;

#pragma warning disable CS8602 // Dereference of a possibly null reference.


TasksRemindersCliArgs cliArgs = Parser.Default.ParseArguments<TasksRemindersCliArgs>(args).Value;

RemindersController controller = new(cliArgs);

ServiceProvider serviceProvider = controller.GetServiceProvider();

var userServices = serviceProvider.GetService<IUserServices>();
var reminderServices = serviceProvider.GetService<IRemiderServices>();

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
foreach (var userRecurrences in recurrencesForUsers)
{
    // send email to user
}