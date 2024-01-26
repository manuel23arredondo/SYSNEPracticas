using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MAUI.Utilidades
{
    public class VideoGMensajeria: ValueChangedMessage<VideoGMensaje>
    {
        public VideoGMensajeria(VideoGMensaje value): base(value)
        {

        }
    }
}
