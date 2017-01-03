using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackSlashCommandWebhook
{
    public interface IClock
    {
        DateTime Now { get; }
    }

    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;
    }

    public class StaticClock : IClock
    {
        public DateTime Now => new DateTime(2016, 12, 31, 1, 10, 15);
    }
}
