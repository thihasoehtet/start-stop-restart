using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartStopRestart
{
    public class Controller
    {
        private int _count = 0;
        private Task _job;
        private bool _isStopped = false;

        public void Start()
        {
            _count++;
            _isStopped = false;
            _job = DoJob(_count);
        }

        private async Task DoJob(int count)
        {
            while (!_isStopped)
            {
                Console.WriteLine("Doing Job {0}...", count);
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        public void Stop()
        {
            _isStopped = true;
            _job.Wait();
            Console.WriteLine("Job {0} stopped.", _count);            
        }

        public void Restart()
        {
            this.Stop();
            this.Start();
        }
    }
}
