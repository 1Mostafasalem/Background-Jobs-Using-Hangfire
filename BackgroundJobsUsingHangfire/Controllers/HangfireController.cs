using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundJobsUsingHangfire.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangfireController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
            //Fire-and-forget: These jobs are executed only once and almost immediately after they are fired.
            BackgroundJob.Enqueue(() => SendMessage("Hangfire@outlook.com"));
            
            //Delayed jobs: Delayed jobs are executed only once too, but not immediately, after a certain time interval.
            Console.WriteLine(DateTime.Now);
            BackgroundJob.Schedule(() => SendMessage("Hangfire@outlook.com"), TimeSpan.FromMinutes(1));

            //Recurring jobs: Recurring jobs fire many times on the specified CRON schedule.
            RecurringJob.AddOrUpdate(() => SendMessage("Hangfire@outlook.com"), Cron.Minutely());

        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendMessage(string email)
        {
            Console.WriteLine($"Mail Sent at {DateTime.Now}");
        }
    }
}
