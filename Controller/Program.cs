using System;
using System.Collections.Generic;

namespace shuttleroom
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start: Shuttle Controller");

            var registerResult = ShuttleApi.Shuttle_Register_Callback_Global(
                OnKeyPressed, ShuttleDeviceTypes.Express, ShuttleDeviceNumbers.Primary);
            if (registerResult != ShuttleApiResults.Ok) {
                Console.WriteLine("Error: {0}", registerResult);
                return;
            }
            Console.WriteLine("Ok: Hook registered!");

            // Setup a timer with interval of 200ms to ensure GetMessage will not get stuck
            var timerId = WinApi.SetTimer(IntPtr.Zero, IntPtr.Zero, 200, IntPtr.Zero);
            
            try {
                while (true) {
                    if (IsExitRequested()) {
                        break;
                    }

                    if (!ProcessMessage()) {
                        break;
                    }
                }
            }
            finally {
                WinApi.KillTimer(IntPtr.Zero, timerId);
                ShuttleApi.Shuttle_Unregister_Callback(ShuttleDeviceTypes.Express,ShuttleDeviceNumbers.Primary);

                Console.WriteLine("Exited: Shuttle Controller");
            }
        }

        private static readonly Dictionary<ShuttleKeys, string> keyNames = new Dictionary<ShuttleKeys, string> {
            // Jog keys
            {ShuttleKeys.JogLeft, "JogLeft"},
            {ShuttleKeys.JogRight, "JogRight"},

            // Buttons
            {ShuttleKeys.Button1, "Button01"},
            {ShuttleKeys.Button2, "Button02"},
            {ShuttleKeys.Button3, "Button03"},
            {ShuttleKeys.Button4, "Button04"},
            {ShuttleKeys.Button5, "Button05"},
            {ShuttleKeys.Button6, "Button06"},
            {ShuttleKeys.Button7, "Button07"},
            {ShuttleKeys.Button8, "Button08"},
            {ShuttleKeys.Button9, "Button09"},
            {ShuttleKeys.Button10, "Button10"},
            {ShuttleKeys.Button11, "Button11"},
            {ShuttleKeys.Button12, "Button12"},
            {ShuttleKeys.Button13, "Button13"},
            {ShuttleKeys.Button14, "Button14"},
            {ShuttleKeys.Button15, "Button15"},

            // Outer ring
            {ShuttleKeys.TransitionL7L8, "RingL8"},
            {ShuttleKeys.TransitionL6L7, "RingL7"},
            {ShuttleKeys.TransitionL5L6, "RingL6"},
            {ShuttleKeys.TransitionL4L5, "RingL5"},
            {ShuttleKeys.TransitionL3L4, "RingL4"},
            {ShuttleKeys.TransitionL2L3, "RingL3"},
            {ShuttleKeys.TransitionL1L2, "RingL2"},
            {ShuttleKeys.TransitionCenterL1, "RingL1"},
            {ShuttleKeys.TransitionCenterR1, "RingR1"},
            {ShuttleKeys.TransitionR1R2, "RingR2"},
            {ShuttleKeys.TransitionR2R3, "RingR3"},
            {ShuttleKeys.TransitionR3R4, "RingR4"},
            {ShuttleKeys.TransitionR4R5, "RingR5"},
            {ShuttleKeys.TransitionR5R6, "RingR6"},
            {ShuttleKeys.TransitionR6R7, "RingR7"},
            {ShuttleKeys.TransitionR7R8, "RingR8"},
        };

        private static void OnKeyPressed(ShuttleKeys key, ShuttleKeyStatuses status,
            ShuttleDeviceTypes type, ShuttleDeviceNumbers devNo)
        {
            if (status != ShuttleKeyStatuses.Down)
            {
                // We only care about pressed, not releases
                return;
            }

            if (!keyNames.TryGetValue(key, out var name))
            {
                // Unknown key
                return;
            }

            Console.WriteLine(name);
        }

        private static bool IsExitRequested()
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }

            var key = Console.ReadKey(true);
            return key.Key == ConsoleKey.Escape;
        }

        private static bool ProcessMessage()
        {
            var result = WinApi.GetMessage(out var msg, IntPtr.Zero, 0, 0);
            if (result == -1)
            {
                // Error
                return false;
            }

            WinApi.TranslateMessage(ref msg);
            WinApi.DispatchMessage(ref msg);

            return true;
        }
    }
}
