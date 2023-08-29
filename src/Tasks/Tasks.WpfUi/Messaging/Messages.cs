using CommunityToolkit.Mvvm.Messaging.Messages;
using System;

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

    public sealed class DeleteOpenChecklistMessage : ValueChangedMessage<Guid>
    {
        public DeleteOpenChecklistMessage(Guid checklistId) : base(checklistId) { }
    }

    public sealed class OpenChecklistControlMessage : ValueChangedMessage<Guid>
    {
        public OpenChecklistControlMessage(Guid checklistId) : base(checklistId) { }
    }

    public sealed class OpenClonedChecklistMessage : ValueChangedMessage<Guid>
    {
        public OpenClonedChecklistMessage(Guid checklistId) : base(checklistId) { }
    }


    public sealed class ChecklistDeletedMessage : ValueChangedMessage<Guid>
    {
        public ChecklistDeletedMessage(Guid checklistId) : base(checklistId) { }
    }

    public sealed class OpenChecklistItemDeletedMessage : ValueChangedMessage<Guid>
    {
        public OpenChecklistItemDeletedMessage(Guid checklistId) : base(checklistId) { }
    }


}
