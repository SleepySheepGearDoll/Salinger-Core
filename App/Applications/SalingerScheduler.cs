using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Salinger.Core.Applications
{
    internal class SalingerScheduler : TaskScheduler
    {
        /// <summary>
        /// Indicates whether the current thread is processing work items.
        /// </summary>
        [ThreadStatic]
        private static bool currentThreadIsProcesingItems;

        /// <summary>
        /// The list of tasks to be executed.
        /// 
        /// protected by lock(tasks).
        /// </summary>
        /// <typeparam name="Task"></typeparam>
        /// <returns></returns>
        private readonly LinkedList<Task> tasks = new LinkedList<Task>();

        /// <summary>
        /// The maximum concurrency level allowed by this scheduler.
        /// </summary>
        private readonly int maxDegreeOfParallelism;

        /// <summary>
        /// Indicates whether the scheduler is currently processing work items.
        /// </summary>
        private int delegatesQueuedOrRunning = 0;

        /// <summary>
        /// Creates a new instance with the specified degree of parallelism.
        /// </summary>
        /// <param name="maxDegreeOfParallelism"></param>
        internal SalingerScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1)
            {
                throw new ArgumentException("maxDegreeOfParallelism");
            }
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        /// <summary>
        /// Queues a task to the scheduler.
        /// 
        /// Add the task to the list of tasks to be processed.
        /// If there aren't enough delegates currently queued or running to process tasks,
        /// schedule another.
        /// </summary>
        /// <param name="task"></param>
        protected sealed override void QueueTask(Task task)
        {
            lock (this.tasks)
            {
                this.tasks.AddLast(task);
                if (this.delegatesQueuedOrRunning < this.maxDegreeOfParallelism)
                {
                    ++this.delegatesQueuedOrRunning;
                    this.NotifyThreadPoolOfPendingWork();
                }
            }
        }

        /// <summary>
        /// Inform the ThreadPool that there's work to be executed for this scheduler.
        /// </summary>
        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>{
                SalingerScheduler.currentThreadIsProcesingItems = true;
                try
                {
                    // Process all available items in the queue.
                    while (true)
                    {
                        Task item;
                        lock (this.tasks)
                        {
                            // When there are no more items to be processed,
                            // note then we're done processing, and get out.
                            if (this.tasks.Count == 0)
                            {
                                --this.delegatesQueuedOrRunning;
                                break;
                            }

                            // Get the next item from the queue.
                            item = this.tasks.First.Value;
                            this.tasks.RemoveFirst();
                        }

                        // Execute the task we pulled out of the queue.
                        base.TryExecuteTask(item);
                    }
                }
                finally
                {
                    // We're done processing items on the current thread.
                    SalingerScheduler.currentThreadIsProcesingItems = false;
                }
            }, null);
        }

        /// <summary>
        /// Attempts to execute the specified task on the current thread.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskWasPreviouslyQueued"></param>
        /// <returns>
        /// If this thread isn't already processing a task, we don't support inlining.
        /// </returns>
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (SalingerScheduler.currentThreadIsProcesingItems == false)
            {
                return false;
            }

            // If the task was previously queued, remove it from the queue.
            if (taskWasPreviouslyQueued == true)
            {
                // Try to run the task.
                if (this.TryDequeue(task) == true)
                {
                    return base.TryExecuteTask(task);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return base.TryExecuteTask(task);
            }
        }

        /// <summary>
        /// Attempt to remove a previously scheduled task from the scheduler.
        /// </summary>
        /// <param name="task">Task</param>
        /// <returns></returns>
        protected sealed override bool TryDequeue(Task task)
        {
            lock (this.tasks)
            {
                return this.tasks.Remove(task);
            }
        }

        /// <summary>
        /// Gets the maximum concurrency level supported by this scheduler.
        /// </summary>
        /// <value></value>
        public sealed override int MaximumConcurrencyLevel {get {return this.maxDegreeOfParallelism;}}

        /// <summary>
        /// Gets an enumerable of the tasks currently scheduled on this scheduler.
        /// </summary>
        /// <returns>enumerable of the tasks.</returns>
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(this.tasks, ref lockTaken);
                if (lockTaken == true) 
                {
                    return this.tasks;
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            finally
            {
                if (lockTaken == true)
                {
                    Monitor.Exit(this.tasks);
                }
            }
        }
    }
}