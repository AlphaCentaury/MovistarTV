using IpTviewr.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    class WindowsIconTest : Experiment
    {
        protected override int Run(string[] args)
        {
            var icon = new WindowsIcon(3);
            icon.AddImage(new Bitmap("iconTest@48.png"));
            icon.AddImage(new Bitmap("iconTest@16.png"));
            icon.AddImage(new Bitmap("iconTest@32.png"));

            icon.Save("iconTest.ico");

            byte[] data;

            using (var output = new MemoryStream())
            {
                icon.Save(output);
                output.Close();
                data = output.ToArray();
            } // using output

            for (int index=0,pos=0; index < 512;index++)
            {
                var b = data[index];
                Console.Write("{0,4:N0}", b);

                pos++;
                if ((pos % 8) == 0) Console.Write("   ");
                if ((pos % 16) == 0) Console.WriteLine();
            } // for index

            return 0;
        } // Run
    } // class WindowsIconTest
} // namespace
