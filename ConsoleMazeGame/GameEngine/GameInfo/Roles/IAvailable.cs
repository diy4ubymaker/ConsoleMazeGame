﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIY4UMazeGame.GameEngine.GameInfo.Roles
{
    public interface IAvailable
    {
        bool isAvailable { get; set; }
        void SetNotAvailable(int ms);
    }
}
