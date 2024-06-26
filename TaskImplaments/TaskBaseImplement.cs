﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskImplaments.Interfaces;
using Win32Handlers;

namespace TaskImplaments
{
    public class TaskBaseImplement
    {
#pragma warning disable IDE0290
        public TaskBaseImplement(IAbstractCounter abstractCounter)
        {
            AbstractCounter = abstractCounter;
        }
#pragma warning restore IDE0290

        public HANDLE StateTask { get; private set; }
        public IAbstractCounter AbstractCounter { get; private set; }

        public int TimeOut { get; private set; } = 1;

        public void SetTimeOut(int timeOut)
        {
            TimeOut = timeOut;
        }


        public virtual async Task<HANDLE> StartAsync()
        {
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        public virtual async Task<HANDLE> ExecuteAsync()
        {
            if (!AbstractCounter.IsAliveUpdate())
            {
                StateTask = new HANDLE(Result.E_END);
                return await Task.FromResult(StateTask);
            }
            return await Task.FromResult(new HANDLE(Result.S_OK));
        }

        public virtual async Task<HANDLE> EndAsync()
        {
            StateTask = new HANDLE(Result.E_END);
            return await Task.FromResult(StateTask);
        }
    }
}
