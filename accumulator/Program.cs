using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;


namespace accumulator
{

    public class accu
    {


        private SshClient SC = null;
        public ShellStream SS = null;

        public string STRbuild = null;

        public string user = null;
        public string pass = null;
        public string host = null;
        public string cmd = null;
        public string type = null;



        public void recvSSHData()
        {
            while (true)
            {
                if (SS != null && SS.DataAvailable)
                {
                    string strData = SS.Read();
                    STRbuild = STRbuild + strData;
                    SS.Flush();

                    if ((STRbuild.Count(x => x == '#')) >= 3)
                    {
                        System.IO.File.WriteAllText(@"output\" + cmd + "\\" + host + "_" + cmd + ".txt", STRbuild);
                    }
                }

                System.Threading.Thread.Sleep(200);
            }
        }

        public void Run()
        {

            System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(recvSSHData);
            System.Threading.Thread thread = new System.Threading.Thread(threadStart);
            thread.IsBackground = true;
            thread.Start();


            SC = new SshClient(host, Convert.ToInt32(22), user, pass);
            SC.ConnectionInfo.Timeout = TimeSpan.FromSeconds(120);
            SC.ConnectionInfo.Timeout = new TimeSpan(0, 0, 55);
            try
            {
                SC.Connect();
            }
            catch(Exception e)
            {
                System.IO.File.AppendAllText(@"acculog.txt", "\n" + host + " " + cmd + " " + e);
            }
            SS = SC.CreateShellStream("SS", 80, 60, 800, 600, 65536);

            SS.Write("term len 0" + "\n");


            if (string.Compare(cmd, "run") == 0)
            {
                    SS.Write("sh run" + "\n");
            }

            if (string.Compare(cmd, "ver") == 0)
            {
                SS.Write("sh ver" + "\n");
            }

            if (string.Compare(cmd, "mac") == 0)
            {
                SS.Write("sh mac add" + "\n");
            }

            if (string.Compare(cmd, "arp") == 0)
            {
                SS.Write("sh ip arp" + "\n");
            }

            if (string.Compare(cmd, "intstatus") == 0)
            {
                SS.Write("sh int status" + "\n");
            }

            if (string.Compare(cmd, "vlan") == 0)
            {
                SS.Write("sh vlan" + "\n");
            }

            if (string.Compare(cmd, "stpdet") == 0)
            {
                SS.Write("sh spanning-tree detail" + "\n");
            }

            if (string.Compare(cmd, "cdpdet") == 0)
            {
                SS.Write("sh cdp neighbors det" + "\n");
            }

            if (string.Compare(cmd, "ipintbrief") == 0)
            {
                SS.Write("sh ip int brief" + "\n");
            }

            if (string.Compare(cmd, "logg") == 0)
            {
                SS.Write("sh logg" + "\n");
            }

            if (string.Compare(cmd, "powerinline") == 0)
            {
                SS.Write("sh power inline" + "\n");
            }

            if (string.Compare(cmd, "etherchannel") == 0)
            {
                SS.Write("sh etherchannel summary" + "\n");
            }

            if (string.Compare(cmd, "swenvall") == 0)
            {
                SS.Write("sh env all" + "\n");
            }

            if (string.Compare(cmd, "rtenvall") == 0)
            {
                SS.Write("sh env all" + "\n");
            }

            System.Threading.Thread.Sleep(20000);
        }

    }




    public partial class Program
    {


        static void Main(string[] args)
        {
            string user = args[0];
            string pass = args[1];
            string host = args[2];
            string cmd = args[3];
            string type = args[4];

            accu accumulator = new accu();

            accumulator.user = user;
            accumulator.pass = pass;
            accumulator.host = host;
            accumulator.cmd = cmd;
            accumulator.type = type;

            accumulator.Run();



        }



        
    }
}
