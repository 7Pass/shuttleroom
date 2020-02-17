using System.Runtime.InteropServices;

namespace shuttleroom {
    public enum ShuttleApiResults: int {
        Ok,
        ParamError,
        DriverNotFound,
        AlreadyRegistered,
        RegNotFound,
        UnknownError,
    }

    public enum ShuttleKeys: uint {
        // Inner ring
        JogLeft,
        JogRight,

        // Outter ring
        RingButtonL8,
        RingButtonL7,
        RingButtonL6,
        RingButtonL5,
        RingButtonL4,
        RingButtonL3,
        RingButtonL2,
        RingButtonL1,
        RingButtonCenter,
        RingButtonR1,
        RingButtonR2,
        RingButtonR3,
        RingButtonR4,
        RingButtonR5,
        RingButtonR6,
        RingButtonR7,
        RingButtonR8,

        // Outer ring clockwise transitions
        TransitionL8L7 = 24,
        TransitionL7L6,
        TransitionL6L5,
        TransitionL5L4,
        TransitionL4L3,
        TransitionL3L2,
        TransitionL2L1,
        TransitionL1Center,
        TransitionCenterR1,
        TransitionR1R2,
        TransitionR2R3,
        TransitionR3R4,
        TransitionR4R5,
        TransitionR5R6,
        TransitionR6R7,
        TransitionR7R8,

        // Outer ring counter-clockwise transitions
        TransitionR8R7 = 42,
        TransitionR7R6,
        TransitionR6R5,
        TransitionR5R4,
        TransitionR4R3,
        TransitionR3R2,
        TransitionR2R1,
        TransitionR1Center,
        TransitionCenterL1,
        TransitionL1L2,
        TransitionL2L3,
        TransitionL3L4,
        TransitionL4L5,
        TransitionL5L6,
        TransitionL6L7,
        TransitionL7L8,

        // Buttons
        Button1 = 61,
        Button2,
        Button3,
        Button4,
        Button5,
        Button6,
        Button7,
        Button8,
        Button9,
        Button10,
        Button11,
        Button12,
        Button13,
        Button14,
        Button15,
    }

    public enum ShuttleKeyStatuses: byte {
        Down,
        Up,
    }

    public enum ShuttleDeviceTypes: int {
        Express = 1,
        Pro,
        Pro2,
    }

    public enum ShuttleDeviceNumbers: int {
        Primary = 1,
        Secondary,
    }

    public static class ShuttleApi {
        public delegate void ShuttleCallback(ShuttleKeys key, ShuttleKeyStatuses statuses, ShuttleDeviceTypes types, ShuttleDeviceNumbers devNo);
        
        [DllImport("ShuttleSDK64.dll")]
        public static extern ShuttleApiResults Shuttle_Register_Callback_Global(ShuttleCallback callback, ShuttleDeviceTypes types, ShuttleDeviceNumbers devNo);

        [DllImport("ShuttleSDK64.dll")]
        public static extern ShuttleApiResults Shuttle_Unregister_Callback(ShuttleDeviceTypes types, ShuttleDeviceNumbers devNo);
    }
}