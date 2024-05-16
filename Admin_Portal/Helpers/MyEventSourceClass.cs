using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

namespace Bank_Portal.Helpers
{
    [EventSource(Name = "MyEventSource")]
    sealed class MyEventSourceClass : EventSource
    {
      
        public static MyEventSourceClass Log = new MyEventSourceClass();

        private MyEventSourceClass()
        {
        }

        [Event(1, Opcode = EventOpcode.Info, Level = EventLevel.Informational)]
        public void TraceMessage(string message)
        {
            WriteEvent(1, message);
        }

        [Event(2, Message = "{0}({1}) - {2}: {3}", Opcode = EventOpcode.Info, Level = EventLevel.Informational)]
        public void TraceCodeLine([CallerFilePath] string filePath = "",
                                  [CallerLineNumber] int line = 0,
                                  [CallerMemberName] string memberName = "", string message = "")
        {
            WriteEvent(2, filePath, line, memberName, message);
        }


        [Event(3, Message = "Entering {0}.{1}", Opcode = EventOpcode.Start, Level = EventLevel.Informational)]
        public void TraceEnter(string className, string methodName)
        {
            WriteEvent(3, className, methodName);
        }

        [Event(4, Message = "Exiting {0}.{1}", Opcode = EventOpcode.Stop, Level = EventLevel.Informational)]
        public void TraceExit(string className, string methodName)
        {
            WriteEvent(4, className, methodName);
        }
    }
}
