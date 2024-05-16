using System.Diagnostics;
using System.Reflection;

namespace Bank_Portal.Helpers
{
    public class FnTraceWrap : IDisposable
    {
        private string methodName;
        private string className;

        private bool disposed = false;

        public FnTraceWrap()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase method = frame.GetMethod();
            this.methodName = method.Name;
            this.className = method.DeclaringType.Name;
            MyEventSourceClass.Log.TraceEnter(this.className, this.methodName);
        }

        public void TraceMessage(string format, params object[] args)
        {
            string message = string.Format(format, args);
            MyEventSourceClass.Log.TraceMessage(message);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    MyEventSourceClass.Log.TraceExit(this.className, this.methodName);
                }
                disposed = true;
            }
        }
    }
}
