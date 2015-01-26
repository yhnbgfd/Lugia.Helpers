using Leap;

namespace Lugia.Helpers.Hardware.LeapMotion
{
    static class LeapMotionInitialize
    {
        private static Controller LMController;
        private static Frame LMFrame;
        private static Frame LMPreviousFrame;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static LeapMotionInitialize()
        {
            LMController = new Controller();
            EnableGesture();
        }

        /// <summary>
        /// LM自带手势开启
        /// </summary>
        private static void EnableGesture()
        {
            LMController.EnableGesture(Gesture.GestureType.TYPECIRCLE);
            LMController.EnableGesture(Gesture.GestureType.TYPEINVALID);
            LMController.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
            LMController.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
            LMController.EnableGesture(Gesture.GestureType.TYPESWIPE);
        }

        /// <summary>
        /// 获取当前帧
        /// </summary>
        /// <returns></returns>
        public static Frame GetFrame()
        {
            LMFrame = LMController.Frame();
            return LMFrame;
        }

        /// <summary>
        /// 获取非当前帧
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Frame GetFrame(int i)
        {
            LMPreviousFrame = LMController.Frame(i);
            return LMPreviousFrame;
        }

        /// <summary>
        /// 检查LM是否连接
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection()
        {
            return LMController.IsConnected;
        }
    }
}
