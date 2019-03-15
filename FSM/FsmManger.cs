using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 有限状态机
/// 2019-03-13
/// </summary>
namespace FSM
{
    [System.Serializable]
    public class StateFunc
    {
        public delegate void EnterFunc(object param);
        public delegate void ExitFunc();
        public delegate void UpdateFunc(float timer);

        public EnterFunc EnterAction;
        public ExitFunc ExitAction;
        public UpdateFunc UpdateAction;
    }

    [System.Serializable()]
    public class FsmManger<T> where T : struct
    {
        protected T mPreState;
        protected T mCurrentState;
        public float stateTimer = 0;

        public T CurrentState { get { return mCurrentState; } }
        protected Dictionary<T, StateFunc> mStateFuncs = new Dictionary<T, StateFunc>();

        /// <summary>
        /// 注册状态机
        /// </summary>
        /// <param name="state">当前的</param>
        /// <param name="enter">进入的状态</param>
        /// <param name="update">更新的状态</param>
        /// <param name="exit">退出的状态</param>
        public void RegisterState(T state, StateFunc.EnterFunc enter, StateFunc.UpdateFunc update, StateFunc.ExitFunc exit)
        {
            StateFunc func = new StateFunc
            {
                EnterAction = enter,
                UpdateAction = update,
                ExitAction = exit
            };

            mStateFuncs.Add(state, func);
            Debug.Log("state:" + state);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="elpise">时间间隔</param>
        public void UpdateState(float elpise)
        {
            if (mStateFuncs.ContainsKey(mCurrentState) && (mStateFuncs[mCurrentState].UpdateAction != null))
                mStateFuncs[mCurrentState].UpdateAction(elpise);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="state">要设置的状态</param>
        /// <param name="param">参数</param>
        public void SetState(T state, object param = null)
        {
            if (mStateFuncs.Equals(state)) return;
            T current = mCurrentState;
            mPreState = current;
            mCurrentState = state;
            if (mStateFuncs.ContainsKey(state) && (mStateFuncs[current].ExitAction)!= null)
                mStateFuncs[current].ExitAction();

            if (mStateFuncs.ContainsKey(mCurrentState) && (mStateFuncs[mCurrentState].EnterAction) != null)
                mStateFuncs[mCurrentState].EnterAction(param);

            stateTimer = 0;
        }

        public bool HasState(T state)
        {
            return mStateFuncs.Contains(state);
        }
    }
}
