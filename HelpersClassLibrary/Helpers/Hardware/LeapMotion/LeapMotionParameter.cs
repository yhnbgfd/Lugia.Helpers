using Leap;

namespace Lugia.Helpers.Hardware.LeapMotion
{
    /// <summary>
    /// 获取LeapMotion各种参数
    /// </summary>
    public class LeapMotionParameter
    {
        private Frame LMFrame;
        private int HandsNumber;
        private int FingersNumber;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LeapMotionParameter()
        {
            HandsNumber = 0;
            FingersNumber = 0;
        }

        /// <summary>
        /// 检查LM连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return LeapMotionInitialize.CheckConnection();
        }

        /// <summary>
        /// 获取当前识别到的手的数量
        /// </summary>
        /// <returns></returns>
        public int GetHandsNumber()
        {
            LMFrame = LeapMotionInitialize.GetFrame();
            HandList Hands = LMFrame.Hands;
            HandsNumber = Hands.Count;
            return HandsNumber;
        }

        /// <summary>
        /// 获取第一只手被识别出的手指数量
        /// </summary>
        /// <returns></returns>
        public int GetFingersNumber()
        {
            if (this.GetHandsNumber() == 0)
            {
                return 0;
            }
            LMFrame = LeapMotionInitialize.GetFrame();
            Hand hand = LMFrame.Hands[0];
            FingerList Fingers = hand.Fingers;
            FingersNumber = Fingers.Count;
            return Fingers.Count;
        }

        /// <summary>
        /// 获取第一只手手掌的坐标
        /// </summary>
        /// <returns></returns>
        public Vector GetPalmPosition()
        {
            LMFrame = LeapMotionInitialize.GetFrame();
            return LMFrame.Hands[0].PalmPosition;
        }

        /// <summary>
        /// 获取第一只手的第一根手指指尖的坐标
        /// </summary>
        /// <returns></returns>
        public Vector GetFingertipPosition()
        {
            LMFrame = LeapMotionInitialize.GetFrame();
            return LMFrame.Hands[0].Fingers[0].TipPosition;
        }

        /// <summary>
        /// 获取第一只手的所有手指的ID
        /// </summary>
        /// <returns></returns>
        public int[] GetFingersID()
        {
            int[] FingerID = new int[5];
            LMFrame = LeapMotionInitialize.GetFrame();
            for (int i = 0; i < LMFrame.Hands[0].Fingers.Count; i++)
            {
                FingerID[i] = LMFrame.Hands[0].Fingers[i].Id;
            }
            return FingerID;
        }

        /// <summary>
        /// 获取第一只手的所有手指的ID
        /// </summary>
        /// <param name="LMFrame"></param>
        /// <returns></returns>
        private int[] GetFingersID(Frame LMFrame)
        {
            int[] FingerID = new int[5];
            for (int i = 0; i < LMFrame.Hands[0].Fingers.Count; i++)
            {
                FingerID[i] = LMFrame.Hands[0].Fingers[i].Id;
            }
            return FingerID;
        }
    }
}
