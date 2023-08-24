using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using Tasks.WpfUi.DisplayModels;

namespace Tasks.WpfUi.Messaging;

public class Messages
{
    public sealed class CloseOpenChecklistMessage : ValueChangedMessage<Guid>
    {
        public CloseOpenChecklistMessage(Guid checklistId) : base(checklistId) { }
    }

    public sealed class OpenChecklistSettingsPageMessage : ValueChangedMessage<Guid>
    {
        public OpenChecklistSettingsPageMessage(Guid checklistId) : base(checklistId) { }
    }

    public sealed class DeleteChecklistMessage : ValueChangedMessage<Guid>
    {
        public DeleteChecklistMessage(Guid checklistId) : base(checklistId) { }
    }

    public sealed class OpenChecklistControlMessage : ValueChangedMessage<Guid>
    {
        public OpenChecklistControlMessage(Guid checklistId) : base(checklistId) { }
    }



}
