using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.ApplicationModel.Background;

namespace VoiceCommandService
{
    public sealed class VoiceCommandService : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            throw new NotImplementedException();

            //BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();



            //_deferral.Complete();
        }
    }
}
