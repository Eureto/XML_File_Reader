using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Polkas.PRP
{
    public class WinApi
    {
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void
            SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
                         int X, int Y, int width, int height, uint flags);

        [DllImport("kernel32.dll", EntryPoint = "Wow64DisableWow64FsRedirection")]
        public static extern bool Wow64DisableWow64FsRedirection(bool which);

        [DllImport("kernel32.dll", EntryPoint = "Wow64RevertWow64FsRedirection")]
        public static extern bool Wow64RevertWow64FsRedirection(bool which);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64; // 0x0040

        public static int ScreenX
        {
            get { return GetSystemMetrics(SM_CXSCREEN); }
        }

        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN); }
        }

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
        }

        public static void ShowOnScreenKeyboard()
        {
            if (Properties.Settings.Default.DisableWow64FsRedirection)
            {
                WinApi.Wow64DisableWow64FsRedirection(true);
                System.Diagnostics.Process.Start("osk.exe");
                WinApi.Wow64RevertWow64FsRedirection(true);
            }
            else
            {
                System.Diagnostics.Process.Start("osk.exe");
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        public static void BringMainWindowToFront(string processName)
        {
            // get the process

            Process bProcess = Process.GetProcessesByName(processName).FirstOrDefault();

            // check if the process is nothing or not.
            if (bProcess != null)
            {
                // get the hWnd of the process
                IntPtr hwnd = bProcess.MainWindowHandle;
                if (hwnd == IntPtr.Zero)
                {
                    // the window is hidden so try to restore it before setting focus.
                    ShowWindow(bProcess.Handle, ShowWindowEnum.Restore);
                }

                // set user the focus to the window
                SetForegroundWindow(bProcess.MainWindowHandle);
            }
            else
            {
                // the process is nothing, so start it
                Process.Start(processName);
            }
        }

        public static void BringMainWindowToFront(Process bProcess)
        {
            // check if the process is nothing or not.
            if (bProcess != null)
            {
                // get the hWnd of the process
                IntPtr hwnd = bProcess.MainWindowHandle;
                if (hwnd == IntPtr.Zero)
                {
                    // the window is hidden so try to restore it before setting focus.
                    ShowWindow(bProcess.Handle, ShowWindowEnum.Restore);
                }

                // set user the focus to the window
                SetForegroundWindow(bProcess.MainWindowHandle);
            }
        }

        // obtains user token
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
            int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        // closes open handes returned by LogonUser
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);


        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>


        public class UserImpersonation : IDisposable
        {
            private System.Security.Principal.WindowsImpersonationContext ImpersonationContext = null;
            private IntPtr UserHandle = IntPtr.Zero;

            public UserImpersonation(string username, string domain, string password, int LogonType = 2, int LogonProvider = 0)
            {
                if (string.IsNullOrEmpty(domain))
                    domain = System.Environment.MachineName;

                var result = WinApi.LogonUser(username, domain, password, LogonType, LogonProvider, ref UserHandle);
                //System.Windows.Forms.MessageBox.Show("Logged In: " + result);
                //if(!result)
                //System.Windows.Forms.MessageBox.Show("Exception impersonating user, error code: " + Marshal.GetLastWin32Error());
                if (!result)
                    throw new ApplicationException("Exception impersonating user, error code: " + Marshal.GetLastWin32Error());
                ImpersonationContext = System.Security.Principal.WindowsIdentity.Impersonate(UserHandle);
                //System.Windows.Forms.MessageBox.Show(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            }

            public void Dispose()
            {
                if (ImpersonationContext != null)
                {
                    ImpersonationContext.Undo();
                }

                if (UserHandle != IntPtr.Zero)
                {
                    CloseHandle(UserHandle);
                }
            }
        }
    }
}

//Przyklady:

//var result = WinApi.LogonUser(username, domain, password, LogonType, LogonProvider, ref UserHandle);

//new WinApi.UserImpersonation("TerminalAccess", Context.Config.UstawieniaStanowiska.WindowsDomain, "vmC4Elhelw")