using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Infrastructure.InteractionRequests
{
    public interface IPopupWindowActionAware
    {
        Window HostWindow { get; set; }

        Notification HostNotification { get; set; }
    }
}
