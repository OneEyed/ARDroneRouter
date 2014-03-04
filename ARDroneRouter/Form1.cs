using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Management;
using System.Threading;
using System.Net;
using System.Net.Sockets;


using NativeWifi;

namespace ARDroneRouter
{
    public partial class Form1 : Form
    {
        public const int REGISTER_PORT = 5560;
        public UdpClient udpClient;

        private object sync = new object();

        public delegate void OutputScanDelegate();
        public OutputScanDelegate outputScanDelegate;

        public delegate void StoppedBindDelegate();
        public StoppedBindDelegate stoppedBindDelegate;

        public delegate void TelnetDelegate();
        public TelnetDelegate telnetDelegate;

        public delegate void RegisterDelegate();
        public RegisterDelegate registerDelegate;

        public delegate void StatusDelegate(String msg);
        public StatusDelegate statusDelegate;

        // Lists all available networks
        Wlan.WlanAvailableNetwork[] networkList;
        WlanClient networkClient;
        WlanClient.WlanInterface networkInterface;

        Wlan.WlanAvailableNetwork networkRouter;
        Wlan.WlanAvailableNetwork networkDrone;

        bool isBinding = false;
        bool isRegistering = false;

        public Form1()
        {
            InitializeComponent();

            udpClient = new UdpClient(REGISTER_PORT);

            outputScanDelegate = OutputScannedNetworks;
            stoppedBindDelegate = OutputBindCompleted;

            telnetDelegate = TelnetDrone;
            registerDelegate = RegisterDrone;

            statusDelegate = StatusEx;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            btnScan.Text = "Scanning WiFi Networks...";
            btnScan.Enabled = false;

            lstRouter.Items.Clear();
            lstDrone.Items.Clear();

            ScanNetworks();
        }

        public void ScanNetworks()
        {
            networkClient = new WlanClient(new Wlan.WlanNotificationCallbackDelegate(this.OnWlanNotification));

            foreach (WlanClient.WlanInterface wlanIface in networkClient.Interfaces)
            {
                // Lists all available networks
                networkInterface = wlanIface;
                networkInterface.Scan();
            }
        }


        public void OutputScannedNetworks()
        {
            

            foreach (Wlan.WlanAvailableNetwork network in networkList)
            {
                bool bDuplicate = false;
                foreach( Wlan.WlanAvailableNetwork n in lstRouter.Items )
                {
                    if( n.SSID.Equals(network.SSID) ) {
                        bDuplicate = true;
                        break;
                    }
                }
                if (bDuplicate || network.SSID.Trim().Length == 0) continue;

                lstRouter.Items.Add(network);
                lstDrone.Items.Add(network);
            }

            lstRouter.DisplayMember = "";
            lstDrone.DisplayMember = "";

            lstRouter.DisplayMember = "SSID";
            lstDrone.DisplayMember = "SSID";

            btnScan.Text = "Scan WiFi Networks";
            btnScan.Enabled = true;
        }

        private void btnBind_Click(object sender, EventArgs e)
        {
            btnBind.Text = "Binding Drone...";
            btnBind.Enabled = false;

            lock (sync)
            {
                networkRouter = (Wlan.WlanAvailableNetwork)lstRouter.SelectedItem;
                networkDrone = (Wlan.WlanAvailableNetwork)lstDrone.SelectedItem;
            }

            //ConnectToDrone();

            Thread t1 = new Thread(ConnectToDrone);
            t1.Start();

            
        }

        public void OutputBindCompleted()
        {
            btnBind.Text = "Bind Drone to Router";
            btnBind.Enabled = true;
            isBinding = false;
        }
        public void ConnectToDrone()
        {
            lock (sync)
            {
                isBinding = true;

                try
                {
                    //String xmlProfile = networkInterface.GetProfileXml(networkDrone.profileName);
                    //networkInterface.SetProfile(Wlan.WlanProfileFlags.AllUser, xmlProfile, true);

                    String xmlProfile = CreateProfile(networkDrone.SSID, "", false);// networkInterface.GetProfileXml(networkRouter.profileName);
                    networkInterface.SetProfile(Wlan.WlanProfileFlags.AllUser, xmlProfile, true);



                    Status("Connecting to " + networkDrone.SSID);
                    bool bSuccess = networkInterface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, networkDrone.SSID, 10000);
                    //bool bSuccess = networkInterface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, networkDrone.profileName, 10000);
                    //networkInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, drone.profileName);
                }
                catch (Exception e)
                {
                    Status("ERROR: " + e.Message);
                    this.Invoke(stoppedBindDelegate);
                }
            }
        }

        public void ConnectToRouter()
        {
            lock (sync)
            {
                isRegistering = true;

                try
                {
                    if (networkInterface.CurrentConnection.profileName.Equals(networkRouter.SSID))
                    {
                    
                            Status("Connected successfully to Router");
                            Thread t1 = new Thread(RegisterDrone);
                            t1.Start();
                            return;
                    
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    String xmlProfile = CreateProfile(networkRouter.SSID, "", false);// networkInterface.GetProfileXml(networkRouter.profileName);
                    networkInterface.SetProfile(Wlan.WlanProfileFlags.AllUser, xmlProfile, true);

                    Status("Connecting to " + networkRouter.SSID);
                    bool bSuccess = networkInterface.ConnectSynchronously(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, networkRouter.SSID, 10000);
                    //networkInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, drone.profileName);
                }
                catch (Exception e)
                {
                    Status("ERROR: " + e.Message);
                    this.Invoke(stoppedBindDelegate);
                }
            }
        }

        public String CreateProfile(String profileName, String password, bool isAdHoc)
        {
            String xml = "";
            xml += "<?xml version=\"1.0\" encoding=\"US-ASCII\"?>";
            xml += "<WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\">";
                xml += "<name>" + profileName + "</name>";
                xml += "<SSIDConfig>";
                    xml += "<SSID>";
                    xml += "<name>" + profileName + "</name>";
                    xml += "</SSID>";
                xml += "</SSIDConfig>";
                xml += "<connectionType>" + (isAdHoc ? "IBSS" : "ESS") + "</connectionType>";
                xml += "<connectionMode>manual</connectionMode>";
                xml += "<autoSwitch>false</autoSwitch>";
                xml += "<MSM>";
                    xml += "<security>";
                        xml += "<authEncryption>";
                            xml += "<authentication>" + (password.Length > 0 ? "WPA" : "open") + "</authentication>";
                            xml += "<encryption>" + (password.Length > 0 ? "TKIP" : "none") + "</encryption>";
                            xml += "<useOneX>false</useOneX>";
                        xml += "</authEncryption>";
                    xml += "</security>"; 
                xml += "</MSM>";
            xml += "</WLANProfile>";

            return xml;
        }
        public void RegisterDrone()
        {
            String szAddress = txtDroneAddress.Text;
            String szName = txtDroneName.Text;
            if( szName.Length == 0 ) {
                szName = networkDrone.SSID;
            }

            String proxyAddress = txtProxyIP.Text;// "192.168.1.3";

            IPAddress proxyIPAddress = IPAddress.Parse(proxyAddress);
            IPEndPoint ep = new IPEndPoint(proxyIPAddress, REGISTER_PORT);

            IPAddress droneIPAddress = IPAddress.Parse(szAddress);

            byte[] ipBytes = droneIPAddress.GetAddressBytes();

            byte[] data = new byte[5 + szName.Length];
            Array.Copy(ipBytes, 0, data, 0, 4);
            data[4] = (byte)szName.Length;
            System.Text.ASCIIEncoding.ASCII.GetBytes(szName, 0, szName.Length, data, 5);

            Status("Sending Drone Registration");
            udpClient.Send(data, data.Length, ep);
            Status("Drone Registration Sent");

            udpClient.Client.ReceiveTimeout = 1000;
            try
            {
                byte[] reply = udpClient.Receive(ref ep);
                if (reply.Length > 0 && reply[0] == 1)
                {
                    Status("Registration successful");
                }
                else
                {
                    String replyStr = "";
                    for (int a = 0; a < reply.Length; a++)
                        replyStr += "[" + reply[a] + "] ";
                    Status("Error with registration. Reply (" + reply.Length + ") - {" + replyStr + "}");
                }
            }
            catch (Exception e)
            {
                Status("Registration timed out.");
            }

            isRegistering = false;

            this.Invoke(stoppedBindDelegate);
        }

        private void TelnetDrone()
        {
            //lock (sync)
            {
                try
                {
                    Status("Establishing Telnet Connection to " + networkDrone.SSID);
                    Socket telnet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    telnet.Connect("192.168.1.1", 23);

                    if (telnet.Connected == false)
                        throw new Exception("Failed to connect to 192.168.1.1 on " + networkDrone.profileName);

                    Status("Telnet connected successfully");

                    String szPassword = "";
                    if (txtPassword.Text.Length > 0)
                        szPassword = " key s:" + txtPassword.Text;
                    //Disable DHCP Server
                    String cmd = "killall udhcpd;";
                    //Connect to Router SSID
                    cmd += "iwconfig ath0 mode managed" + szPassword + " essid " + networkRouter.SSID + 
                            " channel 9;";
                    //Configure IP Address and Subnet
                    cmd += " ifconfig ath0 " + txtDroneAddress.Text + " netmask 255.255.255.0 up;";
                    //Configure Router Gateway
                    cmd += " route add default gw " + txtRouterAddress.Text + ";";
                    //End Command
                    cmd += "\n";

                    //Send command to AR Drone
                    byte[] cmdData = new byte[cmd.Length];
                    System.Text.ASCIIEncoding.ASCII.GetBytes(cmd, 0, cmd.Length, cmdData, 0);
                    int sentBytes = telnet.Send(cmdData, cmdData.Length, SocketFlags.None);
                    if (sentBytes != cmdData.Length)
                        throw new Exception("Failed to send command via telnet: " + sentBytes.ToString() + " of " + cmd.Length + " bytes sent");
                    Status("Successfully sent configuration\n" + cmd);
                    
                    //Close Telnet connection
                    telnet.Close();
                    Status("Disconnected telnet connection");

                    //Disconnect from AR Drone's ad-hoc network
                    networkInterface.Disconnect();
                    Status("Disconnected from " + networkDrone.SSID);

                    

                    this.Invoke(stoppedBindDelegate);
                }
                catch (Exception e)
                {
                    Status("ERROR: " + e.Message);
                    this.Invoke(stoppedBindDelegate);
                }
            }
        }


        private void OnWlanNotification(ref Wlan.WlanNotificationData notifyData, IntPtr context)
        {
            switch (notifyData.notificationSource)
            {
                case Wlan.WlanNotificationSource.ACM:
                    switch ((Wlan.WlanNotificationCodeAcm)notifyData.notificationCode)
                    {
                        case Wlan.WlanNotificationCodeAcm.ConnectionComplete:
                            lock (sync)
                            {

                                if (isBinding)
                                {
                                    Status("Connected successfully to Drone");
                                    this.Invoke(telnetDelegate);
                                }
                                if (isRegistering)
                                {
                                    Status("Connected successfully to Router");
                                    Thread t1 = new Thread(RegisterDrone);
                                    t1.Start();
                                    //this.Invoke(registerDelegate);
                                }
                            }
                            
                            break;
                        case Wlan.WlanNotificationCodeAcm.ConnectionStart:
                            break;
                        case Wlan.WlanNotificationCodeAcm.ConnectionAttemptFail:
                            Status("Connection failed");
                            break;
                        case Wlan.WlanNotificationCodeAcm.Disconnected:
                            //Status("Disconnected");
                            break;
                        case Wlan.WlanNotificationCodeAcm.ScanComplete:
                            networkList = networkInterface.GetAvailableNetworkList(0);
                            this.Invoke(outputScanDelegate);
                            break;
                    }
                    break;
            }

        }

        public void StatusEx(String msg)
        {
            DateTime today = DateTime.UtcNow;
            DateTimeOffset todayTime = new DateTimeOffset(today, TimeSpan.Zero);
            String dateString = todayTime.LocalDateTime.ToString("[hh:mm] ");
            rtxtStatus.AppendText(dateString + msg + "\n");
            rtxtStatus.Select(rtxtStatus.Text.Length, 1);
            rtxtStatus.ScrollToCaret();
        }
        public void Status(String msg)
        {
            object[] args = new object[1];
            args[0] = msg;
            this.Invoke(statusDelegate, args);
        }

        private void btnDroneProxy_Click(object sender, EventArgs e)
        {
            lock (sync)
            {
                try
                {
                    networkRouter = (Wlan.WlanAvailableNetwork)lstRouter.SelectedItem;
                    networkDrone = (Wlan.WlanAvailableNetwork)lstDrone.SelectedItem;
                }
                catch (Exception ex)
                {
                    Status("No network selected.  Please scan for networks.");
                }
            }

            Thread t1 = new Thread(ConnectToRouter);
            t1.Start();
        }
    }
}
