using System.Collections.Generic;
using System.Threading;

namespace BlockchainAssignment
{
    /// <summary>
    /// This is a simple extension of the thread class which allows a list of threads to be joined.
    /// </summary>
    public static class ThreadExtension
    {
        public static void WaitAll(this IEnumerable<Thread> threads)
        {
            if (threads != null)
            {
                foreach (Thread thread in threads)
                { thread.Join(); }
            }
        }
    }
}
