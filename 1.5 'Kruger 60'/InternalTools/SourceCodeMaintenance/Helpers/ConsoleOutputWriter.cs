using System;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Helpers
{
    public class ConsoleOutputWriter : BaseOutputWriter, Interfaces.IToolOutputWriter
    {
        public ConsoleOutputWriter(int indentSize) : base(indentSize)
        {
        } // constructor

        public void WriteLine()
        {
            Console.WriteLine();
        } // WriteLine

        public override void WriteLine(string text)
        {
            Console.WriteLine(@"{0}{1}{2}", GetTimestamp(), GetIndent(), text);
        } // WriteLine

        public void WriteLine(string format, params object[] objects)
        {
            Console.Write(@"{0}{1}", GetTimestamp(), GetIndent());
            Console.WriteLine(format, objects);
        } // WriteLine
    } // class ConsoleOutputWriter
} // namespace