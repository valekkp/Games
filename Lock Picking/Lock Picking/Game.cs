﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lock_Picking
{
    class Game
    {
        private Lockpick lockpick;
        private Lock currentLock;
        private Screwdriver screwdriver;

        public void Start()
        {
            Console.WriteLine("Start");
            lockpick = new Lockpick();
            currentLock = new Lock();
            screwdriver = new Screwdriver();
        }

        public void MoveLockpick(Point location)
        {
            lockpick.Move(location);
        }

        public void DrawElements(Graphics graphics)
        {
            lockpick.Draw(graphics);
            currentLock.Draw(graphics);
            screwdriver.Draw(graphics);
        }

        public void NewScrewdriverAndLock()
        {
            screwdriver = new Screwdriver();
            currentLock = new Lock();
        }

        public void NewLockpick()
        {
            lockpick = new Lockpick();
        }

        public bool CheckLockState()
        {
            return screwdriver.Angle == 180;
        }

        public bool CheckLockpickDurability()
        {
            return lockpick.Durability == 0;
        }

        public void MoveScrewdriver()
        {
            screwdriver.Move(lockpick, currentLock);
        }

        public void ReturnScrewdriver()
        {
            screwdriver.Return();
            lockpick.CurrentColor = Color.Green;
        }
    }
}
