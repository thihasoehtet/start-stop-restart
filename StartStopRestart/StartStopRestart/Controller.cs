using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StartStopRestart
{
    public class Controller
    {
        private int _count = 0;
        private Task _job;
        private Task _faultedJob;
        private CancellationTokenSource _tokenSource;

        public void Start()
        {
            _count++;
            _tokenSource = new CancellationTokenSource();
            _job = DoJob(_count, _tokenSource.Token);
            _faultedJob = _job.ContinueWith(CatchTaskUnhandledException, TaskContinuationOptions.OnlyOnFaulted);
        }

        private async Task DoJob(int count, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Doing Job {0}...", count);
                if (count == 5)
                {
                    throw new Exception("Testing");
                }
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
        }

        public void Stop()
        {
            if (_tokenSource != null)
            {
                Console.WriteLine("Canceling Tokent Source ...");
                _tokenSource.Cancel();    
            }

            if (_job != null)
            {
                while (true)
                {
                    if (_job.IsCompleted)
                    {
                        Console.WriteLine("Disposing Job ...");
                        _job.Dispose();
                        _job = null;
                        break;
                    }
                }    
            }
            
            Console.WriteLine("Job {0} stopped.", _count);
        }

        public void Restart()
        {
            this.Stop();
            this.Start();
        }

        private void CatchTaskUnhandledException(Task task)
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                Console.WriteLine(ex);
            }
            this.Restart();
        }
    }
}
