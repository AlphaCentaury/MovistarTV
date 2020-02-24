// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace IpTviewr.Internal.Tools.GuiTools
{
    internal sealed class TasksQueue: IDisposable
    {
        private CancellationToken _cancellationToken;
        private ConcurrentQueue<Task> _queue;
        private AutoResetEvent _queuedTask;
        private AutoResetEvent _ended;

        public event EventHandler Completed;

        public abstract class Task
        {
            public abstract void Execute();
        } // Task

        public TasksQueue(): this(CancellationToken.None)
        {
            // no-op
        } // constructor

        public TasksQueue(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            _queue = new ConcurrentQueue<Task>();
            _queuedTask = new AutoResetEvent(false);
            _ended = new AutoResetEvent(false);

            var worker = new Thread(ProcessTasks);
            worker.Start();
        } // constructor

        public bool AbortOnError { get; set; }

        public bool IsAborted { get; private set; }

        public int PendingTasks
        {
            get
            {
                if (_queue == null) throw new ObjectDisposedException(nameof(TasksQueue));

                return _queue.Count;
            } // get
        } // PendingTasks

        public void AddTask(Task task)
        {
            if (_queue == null) throw new ObjectDisposedException(nameof(TasksQueue));
            if (task == null) throw new ArgumentNullException(nameof(task));
            _queue.Enqueue(task);
            _queuedTask.Set();
        } // AddTask

        public void WaitCompletion()
        {
            if (_queue == null) throw new ObjectDisposedException(nameof(TasksQueue));
            if (IsAborted) return;

            _queue.Enqueue(null);
            _queuedTask.Set();
            _ended.WaitOne();
        } // WaitCompletion

        public void Dispose()
        {
            if (_queue == null) return;

            _queue = null;
            _queuedTask.Dispose();
            _queuedTask = null;
            _ended.Dispose();
            _ended = null;
        } // Dispose

        private void ProcessTasks()
        {
            Task task;

            while ((task = GetNextTask()) != null)
            {
                try
                {
                    task.Execute();
                }
                catch
                {
                    if (AbortOnError)
                    {
                        IsAborted = true;
                        break;
                    };
                } // try-catch
            } // while

            _ended.Set();
            Completed?.Invoke(this, EventArgs.Empty);
        } // ProcessTasks

        private Task GetNextTask()
        {
            while (true)
            {
                if (_cancellationToken.IsCancellationRequested) return null;
                if (_queue.TryDequeue(out var task))
                {
                    return task;
                } // if

                _queuedTask.WaitOne();
            } // while
        } // GetNextTask
    } // class TasksQueue
} // namespace
