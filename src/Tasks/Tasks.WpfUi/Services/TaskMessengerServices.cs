using CommunityToolkit.Mvvm.Messaging;

namespace Tasks.WpfUi.Services;

public static class TaskMessengerServices
{
    public static void Send<TMessage>(TMessage message) where TMessage : class
    {
        WeakReferenceMessenger.Default.Send(message);
    }
}
