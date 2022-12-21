using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.UI.MVVM
{
    public class AsyncWatcher<TResult> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Task<TResult> Task { get; }

        public TResult Result => Task.Status == TaskStatus.RanToCompletion ? Task.Result : default;

        public TaskStatus Status => Task.Status;

        public AggregateException AggregateException => Task.Exception;

        public Exception InnerException => Task.Exception?.InnerException;

        public bool IsCanceled => Task.IsCanceled;

        public bool IsCompleted => Task.IsCompleted;

        public bool IsCompletedSuccessfully => Task.IsCompletedSuccessfully;

        public bool IsFaulted => Task.IsFaulted;

        public AsyncWatcher(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync();
            }
        }

        private async Task WatchTaskAsync()
        {
            try
            {
                await Task;
            }
            catch
            {

            }

            if (Task.IsCanceled)
            {
                RaisePropertyChanged(nameof(IsCanceled));
                RaisePropertyChanged(nameof(Status));
                RaisePropertyChanged(nameof(IsCanceled));
            }
            else if (Task.IsFaulted)
            {
                RaisePropertyChanged(nameof(IsFaulted));
                RaisePropertyChanged(nameof(InnerException));
                RaisePropertyChanged(nameof(AggregateException));
                RaisePropertyChanged(nameof(Status));
                RaisePropertyChanged(nameof(IsFaulted));
            }
            else if (Task.IsCompletedSuccessfully)
            {
                RaisePropertyChanged(nameof(Result));
                RaisePropertyChanged(nameof(Status));
                RaisePropertyChanged(nameof(IsCompletedSuccessfully));
            }
            else
            {
                RaisePropertyChanged(nameof(Result));
                RaisePropertyChanged(nameof(Status));
                RaisePropertyChanged(nameof(IsCompleted));
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
