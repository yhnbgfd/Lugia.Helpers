using Leap;
using System;

namespace Lugia.Helpers.Hardware
{
    /// <summary>
    /// 此类的LeapMotion代码是基于v1版本SDK，.NET3.5下编写并使用于Unity3.5项目，此类库代码只是做了转移，使用了v2.2.2版本、.NET4.0的LepaMotion dll，未作具体测试。
    /// 以往基于LeapMotion v1的Unity项目，在安装了v2 SDK的电脑上运行的时候，发现个别手势识别不出，可能v2修改了一些接口。
    /// </summary>
    public class LeapMotionHelper
    {
        private static Controller _controller = new Controller();

        /// <summary>
        /// 构造函数
        /// </summary>
        public LeapMotionHelper()
        {
            EnableGesture();
        }

        #region 设置
        /// <summary>
        /// LM自带手势开启
        /// </summary>
        private void EnableGesture()
        {
            _controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
            _controller.EnableGesture(Gesture.GestureType.TYPEINVALID);
            _controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
            _controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
            _controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        }

        #endregion

        #region 获取内容
        /// <summary>
        /// 获取帧
        /// </summary>
        /// <param name="history"></param>
        /// <returns></returns>
        public Frame GetFrame(int history = 0)
        {
            return _controller.Frame(history);
        }
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return _controller.IsConnected;
            }
        }
        /// <summary>
        /// 当前识别到的手的数量
        /// </summary>
        public int CurrentHandsNumber
        {
            get
            {
                return this.GetFrame().Hands.Count;
            }
        }
        /// <summary>
        /// 获取手指数量
        /// </summary>
        /// <param name="handNumber"></param>
        /// <returns></returns>
        public int GetFingersNumber(int handNumber = 0)
        {
            return (handNumber < this.CurrentHandsNumber) ? this.GetFrame().Hands[handNumber].Fingers.Count : 0;
        }
        /// <summary>
        /// 获取手掌的坐标
        /// </summary>
        /// <returns></returns>
        public Vector GetPalmPosition(int handNumber = 0)
        {
            return (handNumber < this.CurrentHandsNumber) ? this.GetFrame().Hands[handNumber].PalmPosition : new Vector(0, 0, 0);
        }
        /// <summary>
        /// 获取指尖的坐标
        /// </summary>
        /// <returns></returns>
        public Vector GetFingerTipPosition(int handNumber = 0, int fingerNumber = 0)
        {
            return (handNumber < this.CurrentHandsNumber) ? this.GetFrame().Hands[handNumber].Fingers[fingerNumber].TipPosition : new Vector(0, 0, 0);
        }
        /// <summary>
        /// 获取所有手指的ID
        /// </summary>
        /// <param name="handNumber"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        private int[] GetFingersID(int handNumber = 0, Frame frame = null)
        {
            if (frame == null)
            {
                frame = this.GetFrame();
            }
            int[] FingersID = new int[frame.Hands[handNumber].Fingers.Count];
            for (int i = 0; i < FingersID.Length; i++)
            {
                FingersID[i] = frame.Hands[handNumber].Fingers[i].Id;
            }
            return FingersID;
        }

        #endregion

        #region 判断动作
        /// <summary>
        /// 画圈手势
        /// 顺时针返回：1
        /// 逆时针返回：2
        /// </summary>
        /// <returns></returns>
        public int Circle()
        {
            Frame LMFrame = GetFrame();
            GestureList gestures = LMFrame.Gestures();
            for (int g = 0; g < gestures.Count; g++)
            {
                if (gestures[g].Type == Gesture.GestureType.TYPECIRCLE)
                {
                    CircleGesture circle = new CircleGesture(gestures[g]);
                    if (circle.Radius < 38.0f) return 0;
                    if (circle.Pointable.Direction.AngleTo(circle.Normal) <= Math.PI / 2)
                    {
                        return 1;//Clockwise
                    }
                    else
                    {
                        return 2;//Counterclockwise
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 五指收缩手势
        /// </summary>
        /// <returns></returns>
        public bool FingersShrink()
        {
            int CountFrames = 20;

            Frame LMFrame;
            Hand Hand;
            FingerList Fingers;
            int[] FingerId = new int[5];    //记录手指的id

            bool StartCount = false;
            int Count = 0;

            for (int f = CountFrames; f > 0; f--)
            {
                LMFrame = GetFrame(f);
                Hand = LMFrame.Hands[0];
                Fingers = Hand.Fingers;
                if (Fingers.Count == 5)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        FingerId[i] = Fingers[i].Id;
                    }
                    StartCount = true;
                }
                if (StartCount)
                {
                    if (Fingers.Count == 1)
                    {
                        if (Fingers[0].Id == FingerId[4])   //经测验，五指的时候，拇指id在最后一位，不论左右手
                        {
                            Count++;
                        }
                    }
                    else if (Fingers.Count == 0 && LMFrame.Hands.Count > 0)
                    {
                        Count++;
                    }
                }
                else if (f < CountFrames / 2)//超过一半还没开始统计就不用统计了
                {
                    return false;
                }
            }
            if (Count >= CountFrames / 2)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 五指张开手势
        /// </summary>
        /// <returns></returns>
        public bool FingersExpand()
        {
            int CountFrames = 20;

            Frame LMFrame;
            Hand Hand;
            FingerList Fingers;

            bool StartCount = false;
            bool StartCountWithThumb = false;
            int Count = 0;
            int ThumbID = 0;

            for (int f = CountFrames; f > 0; f--)
            {
                LMFrame = GetFrame(f);
                Hand = LMFrame.Hands[0];
                Fingers = Hand.Fingers;
                if (Fingers.Count == 0 && LMFrame.Hands.Count > 0)
                {
                    StartCount = true;
                }
                else if (Fingers.Count == 1)
                {
                    ThumbID = Fingers[0].Id;
                    StartCountWithThumb = true;
                }
                if (Fingers.Count == 5)
                {
                    if (StartCount) //0手指开始统计
                    {
                        Count++;
                    }
                    else if (StartCountWithThumb)    //大拇指开始统计
                    {
                        if (ThumbID == Fingers[4].Id)
                        {
                            Count++;
                        }
                        else
                        {
                            StartCountWithThumb = false;
                        }
                    }
                    else if (f < CountFrames / 2)//超过一半还没开始统计就不用统计了
                    {
                        return false;
                    }
                }
            }
            if (Count >= CountFrames / 2)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 手掌滑动手势
        /// 参数：坐标轴X Y Z
        /// 返回：1 正方向、2 负方向
        /// </summary>
        /// <param name="axis">坐标轴X Y Z</param>
        /// <returns>1 正方向、2 负方向</returns>
        public int HandSwipe(string axis)
        {
            Frame LMFrame = GetFrame();
            GestureList Gestures = LMFrame.Gestures();
            for (int g = 0; g < Gestures.Count; g++)
            {
                if (Gestures[g].Type == Gesture.GestureType.TYPESWIPE)
                {
                    SwipeGesture Swipe = new SwipeGesture(Gestures[g]);
                    switch (axis.ToLower())
                    {
                        case "x":
                            if (Swipe.Speed < 500
                                || Math.Abs(Swipe.Position.x - Swipe.StartPosition.x) < 200     //滑动距离大于200
                                || Swipe.Position.y - Swipe.StartPosition.y < 10)    //斜向上滑动才给通过
                            {
                                continue;
                            }
                            if (Swipe.Direction.x > 0.9f)
                            {
                                return 1;
                            }
                            else if (Swipe.Direction.x < -0.9f)
                            {
                                return 2;
                            }
                            break;
                        case "y":
                            if (Swipe.Speed < 800)
                            {
                                continue;
                            }
                            if (Swipe.Direction.y > 0.95f)
                            {
                                return 1;
                            }
                            else if (Swipe.Direction.y < -0.95f)
                            {
                                return 2;
                            }
                            break;
                        case "z":
                            if (Swipe.Speed < 500)
                            {
                                continue;
                            }
                            if (Swipe.Direction.z > 0.9f)
                            {
                                return 1;
                            }
                            else if (Swipe.Direction.z < -0.9f)
                            {
                                return 2;
                            }
                            break;
                        default:
                            return 0;
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 手指滑动手势
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public int FingerSwipe(string axis)
        {
            Frame LMFrame = GetFrame();
            Finger finger = LMFrame.Hands[0].Fingers[0];
            Vector tipVeloctiy = finger.TipVelocity;
            switch (axis.ToLower())
            {
                case "x":
                    if (Math.Abs(tipVeloctiy.x) > 1000 && tipVeloctiy.y > 0)
                    {
                        if (tipVeloctiy.x > 0)
                        {
                            return 1;
                        }
                        else if (tipVeloctiy.x < 0)
                        {
                            return 2;
                        }
                    }
                    break;
                case "y":
                    if (tipVeloctiy.y > 1000)
                    {
                        return 1;
                    }
                    else if (tipVeloctiy.y < -1000)
                    {
                        return 2;
                    }
                    break;
                case "z":
                    if (tipVeloctiy.z > 500)
                    {
                        return 1;
                    }
                    else if (tipVeloctiy.z < -500)
                    {
                        return 2;
                    }
                    break;
                default:
                    return 0;
            }
            return 0;
        }
        /// <summary>
        /// 两只手滑动
        /// 触发效果非常差
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public int SwipeWithTwoHands(string axis)
        {
            Frame LMFrame = GetFrame();
            GestureList gestures = LMFrame.Gestures();
            SwipeGesture swipeNegative = null;     //负方向
            SwipeGesture swipePositive = null;     //正方向
            for (int g = 0; g < gestures.Count; g++)
            {
                if (gestures[g].Type == Gesture.GestureType.TYPESWIPE)
                {
                    SwipeGesture swipe = new SwipeGesture(gestures[g]);
                    float direction = 0.0f;
                    switch (axis.ToLower())
                    {
                        case "x":
                            direction = swipe.Direction.x;
                            break;
                        case "y":
                            direction = swipe.Direction.y;
                            break;
                        case "z":
                            direction = swipe.Direction.z;
                            break;
                        default:
                            return 0;
                    }
                    if (swipePositive == null)  //未发现正方向手势
                    {
                        if (direction > 0.9f)
                        {
                            swipePositive = swipe;
                        }
                    }
                    else if (swipeNegative == null)  //未发现负方向手势
                    {
                        if (direction < -0.9f)
                        {
                            swipeNegative = swipe;
                        }
                    }
                    else /*if (swipeNegative && swipePositive)*/    //两个方向的手势齐全
                    {
                        float StartPositionNegative = 0.0f;
                        float StartPositionPositive = 0.0f;
                        switch (axis.ToLower())
                        {
                            case "x":
                                StartPositionNegative = swipeNegative.StartPosition.x;
                                StartPositionPositive = swipePositive.StartPosition.x;
                                break;
                            case "y":
                                StartPositionNegative = swipeNegative.StartPosition.y;
                                StartPositionPositive = swipePositive.StartPosition.y;
                                break;
                            case "z":
                                StartPositionNegative = swipeNegative.StartPosition.z;
                                StartPositionPositive = swipePositive.StartPosition.z;
                                break;
                        }
                        if (StartPositionNegative < StartPositionPositive)
                        {
                            return 1;   //<>
                        }
                        else if (StartPositionNegative > StartPositionPositive)
                        {
                            return 2;   //><
                        }
                    }
                }
            }
            return 0;
        }
        /// <summary>
        /// 两只手指滑动
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public int SwipeWithTwoFingers(string axis)
        {
            Frame LMFrame = GetFrame();
            if (LMFrame.Hands.Count >= 2)
            {

            }
            return 0;
        }
        /// <summary>
        /// 当从0有手进入识别范围且z小于0时，返回进入的手的数量
        /// </summary>
        /// <returns></returns>
        public int HandsEnter()
        {
            int EnterHandsNum = 0;//进入的手的统计
            Frame LastLMFrame = GetFrame(1);//上一帧Frame
            Frame LMFrame = GetFrame();
            if (LastLMFrame.Hands.Count == 0 && LMFrame.Hands.Count != 0)
            {
                for (int i = 0; i < LMFrame.Hands.Count; i++)
                {
                    if (LMFrame.Hands[i].PalmPosition.z < 0)
                    {
                        EnterHandsNum++;
                    }
                }
            }
            return EnterHandsNum;
        }

        #endregion
    }
}
