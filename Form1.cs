﻿using System;
using System.Windows.Forms;
using System.Security.Principal;
using System.IO;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;

namespace Hosts_changer
{
    public partial class mainWindow : Form
    {
        public mainWindow()
        {
            InitializeComponent();
        }

        static string hostsMessage = "# Copyright (c) 1993-2009 Microsoft Corp.\n#\n# This is a sample HOSTS file used by Microsoft TCP/IP for Windows.\n#\n# This file contains the mappings of IP addresses to host names. Each\n# entry should be kept on an individual line. The IP address should\n# be placed in the first column followed by the corresponding host name.\n# The IP address and the host name should be separated by at least one\n# space.\n#\n# Additionally, comments (such as these) may be inserted on individual\n# lines or following the machine name denoted by a '#' symbol.\n#\n# For example:\n#\n#    102.54.94.97   rhino.acme.com        # source server\n#     38.25.63.10   x.acme.com            # x client host\n#\n#\n# localhost name resolution is handled within DNS itself.\n#      127.0.0.1         localhost\n#      ::1               localhost\n# (This hosts file was generated by Hosts Modifier)";

        public static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        string currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        static bool sendStastics;
        static string errorMsg = "An error occured. If you allowed us to receive error reports from your client, the developer has now been informed anonymously about this error. You can continue using this app normally.";

        private static string getSystemDir()
        {
            string filePath = Path.Combine(Environment.SystemDirectory, "defrag.exe").ToLower();
            filePath = filePath.Replace("defrag.exe", "");
            return filePath;
        }

        private static string getHostIp()
        {
            string msg = new System.Net.WebClient().DownloadString("https://cbot.me/ip");
            msg = msg.Replace("Your current IP address is: ", "");
            return msg;
        }

        static bool sendHook(string content, int type)
        {
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.SendMessage(content, type);
                return true;
            }
        }

        public static void createHosts(string filePath, string fileName)
        {
            try
            {
                using (FileStream fs = File.Create(@"" + filePath + "\\" + fileName))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(hostsMessage);
                    fs.Write(info, 0, info.Length);
                }
            } catch (Exception ex)
            {
                if (sendStastics)
                {
                    sendHook("Client got error in createHosts: " + ex, 2);
                }
                MessageBox.Show(errorMsg, "Error");
            }
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            if (!IsAdministrator())
            {
                MessageBox.Show("You didn't open the app as an admin. I am not able to modify hosts file without them.\nRight click on this app, and choose \"run as an administrator\" option and then try again.");
                this.Close();
                return;
            };

            string confirm = MessageBox.Show("This application WILL MODIFY and DO ACTIONS on YOUR BEHALF.\nBy accepting that with this menu by pressing the \"YES\" button, if anything occurs during this session, the creator of this application IS NOT RESPONSIBLE FOR ANYTHING. Do you understand this and wish to proceed?\n\n(By accepting, you are also agreeing that we will receive your current Internet Protocol Address (public ip) letting us know that you've accepted the terms. You will also agree that you do not attempt to de-compile this exe file without a permission given by the creator (JammuMies327#5283) on Discord.)", "SAFETY NOTICE", MessageBoxButtons.YesNo).ToString().ToLower();
            if (confirm == "no")
            {
                this.Close();
                return;
            } else
            {
                sendHook("Client " + getHostIp() + " accepted the terms of the app.", 1);
            }

            string statistics = MessageBox.Show("Would you like to share occured errors with us? By accepting, if an error occurs and it's able to be caught on the scene, it'll automatically be sent to us. Your identity will remain anonymous on this matter. We're only collecting errors.", "Send statistics", MessageBoxButtons.YesNo).ToString().ToLower();
            if (statistics == "yes")
            {
                sendStastics = true;
            } else
            {
                sendStastics = false;
            }

        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string targetIp = Interaction.InputBox("Enter the address to be added", "Address input");
                if (targetIp == null || targetIp == "")
                {
                    MessageBox.Show("Cannot add an empty address.");
                    return;
                }

                string systemDir = getSystemDir();
                string hostsLocation = systemDir + "drivers\\etc\\hosts";
                string[] filePaths = Directory.GetFiles(@"" + systemDir + "drivers\\etc\\");
                foreach (string filePathCheck in filePaths)
                {
                    if (!filePathCheck.Contains(hostsLocation))
                    {
                        string answer = MessageBox.Show("Unable to locate hosts file from " + hostsLocation + ".\nWould you like to create one?", "Hosts file not found", MessageBoxButtons.YesNo).ToString().ToLower();
                        if (answer == "yes")
                        {
                            createHosts(systemDir + "\\drivers\\etc\\", "hosts");
                            MessageBox.Show("Hosts created with Microsoft basic comments.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Nothing was modified.");
                            return;
                        }
                    }
                    break;
                }
                string text = File.ReadAllText(hostsLocation);
                if (text.Contains(targetIp))
                {
                    string answer = MessageBox.Show("Your hosts file already has the given address there.\nIt might lead to problems if you re-add it, do you wish to continue?", "Same IP found", MessageBoxButtons.YesNo).ToString().ToLower();
                    if (answer == "no")
                    {
                        System.Windows.Forms.MessageBox.Show("Nothing was modified.");
                        return;
                    }
                }
                if (text.Length < 2)
                {
                    File.WriteAllText(hostsLocation, targetIp);
                }
                else
                {
                    File.WriteAllText(hostsLocation, text + "\n" + targetIp);
                }
                System.Windows.Forms.MessageBox.Show("Address added to your hosts file. You can check it out from " + hostsLocation);
                return;
            } catch (Exception ex)
            {
                if (sendStastics)
                {
                    sendHook("Client got error in addBtn_Click: " + ex, 2);
                }
                MessageBox.Show(errorMsg, "Error");
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string targetIp = Interaction.InputBox("Enter the address to remove from hosts file", "Remove");
                if (targetIp == null || targetIp == "")
                {
                    MessageBox.Show("The given address is empty.");
                    return;
                }
                else
                {
                    targetIp = targetIp.ToLower();
                }

                string systemDir = getSystemDir();
                string hostsFileLoc = systemDir + "drivers\\etc\\hosts";

                string[] filePaths = Directory.GetFiles(@"" + systemDir + "drivers\\etc\\");
                foreach (string filePathCheck in filePaths)
                {

                    if (!filePathCheck.Contains(hostsFileLoc))
                    {
                        string answer = MessageBox.Show("Unable to locate hosts file from " + hostsFileLoc + ".\nWould you like to create one?", "Hosts file not found", MessageBoxButtons.YesNo).ToString().ToLower();
                        if (answer == "yes")
                        {
                            createHosts(systemDir + "\\drivers\\etc\\", "hosts");
                            MessageBox.Show("Hosts file created with Microsoft basic comments.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Nothing was modified.");
                            return;
                        }
                    }
                    break;
                }

                string text = File.ReadAllText(hostsFileLoc).ToLower();
                if (text.Contains(targetIp))
                {
                    text = text.Replace(targetIp, "");
                }
                else
                {
                    MessageBox.Show("No matches for " + targetIp + " was found.");
                    return;
                }

                File.WriteAllText(hostsFileLoc, text);
                MessageBox.Show("Removed the match of " + targetIp + " from " + hostsFileLoc);
                return;
            } catch (Exception ex)
            {
                if (sendStastics)
                {
                    sendHook("Client got error in removeBtn_Click: " + ex, 2);
                }
                MessageBox.Show(errorMsg, "Error");
            }
        }

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string click = MessageBox.Show("The action is final! You will lose everything. Proceed?", "Clean hosts", MessageBoxButtons.YesNo).ToString().ToLower();
                if (click == "no")
                {
                    MessageBox.Show("Nothing was modified.");
                    return;
                }

                string systemDir = getSystemDir();
                string hostsFileLoc = systemDir + "drivers\\etc\\hosts";

                string[] filePaths = Directory.GetFiles(@"" + systemDir + "drivers\\etc\\");
                foreach (string filePathCheck in filePaths)
                {

                    if (!filePathCheck.Contains(hostsFileLoc))
                    {
                        string answer = MessageBox.Show("Unable to locate hosts file from " + hostsFileLoc + ".\nWould you like to create one?", "Hosts file not found", MessageBoxButtons.YesNo).ToString().ToLower();
                        if (answer == "yes")
                        {
                            createHosts(systemDir + "\\drivers\\etc\\", "hosts");
                            MessageBox.Show("Hosts file created with Microsoft basic comments.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Nothing was modified.");
                            return;
                        }
                    }
                    break;
                }

                File.WriteAllText(hostsFileLoc, "# Original content of this hosts file was cleaned by Hosts Modifier. (Requested by " + currentUser + ")");
                MessageBox.Show("Hosts file should be cleaned now.");
            } catch (Exception ex)
            {
                if (sendStastics)
                {
                    sendHook("Client got error in cleanBtn_Click: " + ex, 2);
                }
                MessageBox.Show(errorMsg, "Error");
            }
        }

        private void resetDefault_Click(object sender, EventArgs e)
        {
            try
            {
                string click = MessageBox.Show("The action is final! You will lose everything and the content of the hosts file will be returned to the official Microsoft one.", "Reset hosts", MessageBoxButtons.YesNo).ToString().ToLower();
                if (click == "no")
                {
                    MessageBox.Show("Nothing was modified.");
                    return;
                }

                string systemDir = getSystemDir();
                string hostsFileLoc = systemDir + "drivers\\etc\\hosts";

                string[] filePaths = Directory.GetFiles(@"" + systemDir + "drivers\\etc\\");
                foreach (string filePathCheck in filePaths)
                {

                    if (!filePathCheck.Contains(hostsFileLoc))
                    {
                        string answer = MessageBox.Show("Unable to locate hosts file from " + hostsFileLoc + ".\nWould you like to create one?", "Hosts file not found", MessageBoxButtons.YesNo).ToString().ToLower();
                        if (answer == "yes")
                        {
                            createHosts(systemDir + "\\drivers\\etc\\", "hosts");
                            MessageBox.Show("Hosts file created with Microsoft basic comments.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Nothing was modified.");
                            return;
                        }
                    }
                    break;
                }

                createHosts(systemDir + "drivers\\etc\\", "hosts");
                MessageBox.Show("Hosts file should be reseted now.");
            } catch (Exception ex)
            {
                if (sendStastics)
                {
                    sendHook("Client got error in resetDefault_Click: " + ex, 2);
                }
                MessageBox.Show(errorMsg, "Error");
            }
        }

        private void hostDisplay_Click(object sender, EventArgs e)
        {
            try {
                string systemDir = getSystemDir();
                string hostsFileLoc = systemDir + "drivers\\etc\\hosts";

                string[] filePaths = Directory.GetFiles(@"" + systemDir + "drivers\\etc\\");
                foreach (string filePathCheck in filePaths)
                {

                    if (!filePathCheck.Contains(hostsFileLoc))
                    {
                        string answer = MessageBox.Show("Unable to locate hosts file from " + hostsFileLoc + ".\nWould you like to create one?", "Hosts file not found", MessageBoxButtons.YesNo).ToString().ToLower();
                        if (answer == "yes")
                        {
                            createHosts(systemDir + "\\drivers\\etc\\", "hosts");
                            MessageBox.Show("Hosts file created with Microsoft basic comments.");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Nothing was modified.");
                            return;
                        }
                    }
                    break;
                }

                string text = File.ReadAllText(hostsFileLoc);
                MessageBox.Show(text + "\n\nThis is the content of your hosts file from " + hostsFileLoc);

            } catch (Exception ex)
            {
                if (sendStastics)
                {
                    sendHook("Client got error in hostDisplay_Click: " + ex, 2);
                }
                MessageBox.Show(errorMsg, "Error");
            }
}

        public class dWebHook : IDisposable
        {
            private readonly WebClient dWebClient;
            private static NameValueCollection discordValues = new NameValueCollection();
            private string errorWeb = "This will be the webhook where errors come in.";
            private string acceptHook = "For accepting the first pop up window.";

            public string UserName = "Analytics";
            public string ProfilePicture = "https://www.sasupra.lt/wp-content/uploads/2016/08/log-1.png";

            public dWebHook()
            {
                dWebClient = new WebClient();
            }


            public void SendMessage(string msgSend, int type)
            {
                string webhook;
                if (type == 2)
                {
                    webhook = errorWeb;
                } else
                {
                    webhook = acceptHook;
                }

                discordValues.Add("username", UserName);
                discordValues.Add("avatar_url", ProfilePicture);
                discordValues.Add("content", msgSend);

                dWebClient.UploadValues(webhook, discordValues);

                discordValues.Remove("username");
                discordValues.Remove("avatar_url");
                discordValues.Remove("content");
            }

            public void Dispose()
            {
                dWebClient.Dispose();
            }
        }

        private void cbotDc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://cbot.me/support");
        }
    }
}
