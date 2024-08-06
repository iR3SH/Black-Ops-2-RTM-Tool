using PS3Lib;
using System;
using System.Collections.Generic;
using System.Text;
namespace Black_Ops_2_RTM_Tool.Classes
{
    public class Functions
    {
        private PS3API API;
        byte[] Buffer = [];
        public Functions(PS3API API)
        {
            this.API = API;
        }
        private void Send(uint Buffer, byte[] Data)
        {
            API.SetMemory(Buffer, Data);
        }
        public void DoUAV(bool apply)
        {
            if (apply)
            {
                Buffer = [0x2c, 3, 0, 1];
            }
            else
            {
                Buffer =[0x2c, 3, 0, 0];
            }
            API.SetMemory(0x33cb4, Buffer);
        }
        public void DoVSAT(bool apply)
        {
            if (apply)
            {
                Buffer = [0x60, 0, 0, 0];
            }
            else
            {
                Buffer = [0x40, 0x81, 0, 0x44];
            }
            API.SetMemory(0x23c60, Buffer);
        }

        public void DoRedBoxes(bool apply)
        {
            if (apply)
            {
                Buffer = [0x38, 0x60, 0, 1];
                API.SetMemory(0x783e0, Buffer);

                Buffer = [0x60, 0, 0, 0];
                API.SetMemory(0x78604, Buffer);
            }
            else
            {
                Buffer = [0x38, 0x60, 0, 0];
                API.SetMemory(0x783e0, Buffer);
                API.SetMemory(0x78604, Buffer);
            }
        }
        public void DoLazer(bool apply)
        {
            if (apply)
            {
                Buffer = [0x2c, 3, 0, 1];
            }
            else
            {
                Buffer = [0x2c, 3, 0, 0];
            }
            API.SetMemory(0xef68c, Buffer);
        }
        public void DoBigNames(bool apply)
        {
            if (apply)
            {
                Buffer = [0x3f, 0xff, 0xff, 0];
            }
            else
            {
                Buffer = [0x3f, 0x26, 0x66, 0x66];
            }
            API.SetMemory(0x1cc6e98, Buffer);
        }

        public void DoWallHack(bool apply)
        {
            if (apply)
            {
                Buffer = [0x38, 0xc0, 0xff, 0xff];
            }
            else
            {
                Buffer = [0x63, 0x26, 0, 0];
            }
            API.SetMemory(0x834d0, Buffer);
        }
        public void DoNoRecoil(bool apply)
        {
            if (apply)
            {
                Buffer = [0x60, 0, 0, 0];
            }
            else
            {
                Buffer = [0x48, 80, 110, 0xe5];
            }
            API.SetMemory(0xf9e54, Buffer);
        }
        public void DoSteadyAim(bool apply)
        {
            if (apply)
            {
                Buffer = [0x2c, 4, 0, 0];
            }
            else
            {
                Buffer = [0x2c, 4, 0, 2];
            }
            API.SetMemory(0x5f0a20, Buffer);
        }
        public void DoTargetFinder(bool apply)
        {
            if(apply)
            {
                Buffer = [1];
            }
            else
            {
                Buffer = [0];
            }
            API.SetMemory(0x1cc4bf8, Buffer);
        }
        public void DoFloatingBodies(bool apply)
        {
            if (apply)
            {
                Buffer = [0x43, 0x48];
            }
            else
            {
                Buffer = [0xc4, 0x48];
            }
            API.SetMemory(0x1cd0358, Buffer);
        }
        public void DoForceHost(bool apply)
        {
            if (apply)
            {
                Buffer = [0];
            }
            else
            {
                Buffer = [1];
            }
            API.SetMemory(0x1cd6018, Buffer);
        }

        public void ChangeUsername(string username)
        {
            if (username.Length > 0)
            {
                Buffer = Encoding.ASCII.GetBytes(username);
                Array.Resize(ref Buffer, Buffer.Length + 1);

                API.SetMemory(0x26c0658, Buffer);
                API.SetMemory(0x26c067f, Buffer);
            }
        }
        public void ChangeClanTag(string tag)
        {
            if (!string.IsNullOrEmpty(tag))
            {
                Buffer = Encoding.ASCII.GetBytes(tag);
                Array.Resize(ref Buffer, Buffer.Length + 1);
                API.SetMemory(0x2708238, Buffer);
            }
        }

        public List<Images> GetPresitgeIcons()
        {
            List<Images> images = [];
            for(int i = 1; i < 12; i++)
            {
                images.Add(new Images("Prestige " + i, "/Images/" + i + ".png"));
            }
            return images;
        }
        public void ChangePrestige(int index)
        {
            Buffer = BitConverter.GetBytes(index + 1);
            API.SetMemory(0x26fd014, Buffer);
        }
        public void DoLegitStats()
        {
            decimal num = 2764800M;
            decimal num2 = 28800M;
            decimal num3 = 1380M;
            decimal num4 = (num + num2) + num3;
            int num5 = 0x22c0a;

            Buffer = BitConverter.GetBytes(Convert.ToUInt32(num4.ToString()));
            API.SetMemory(0x26fd10a, Buffer);

            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fc942, Buffer);

            num5 = 0x53e10;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcb70, Buffer);

            num5 = 0x1199ed0;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd050, Buffer);

            num5 = 0x70c7;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd152, Buffer);

            num5 = 0x2d97;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcbe2, Buffer);

            Buffer = [
                0, 0, 15, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0,
                0x36, 0, 0, 0, 0, 0, 0x4c, 15, 0x13
             ];
            API.SetMemory(0x26fd016, Buffer);
        }
        public void DoLowStats()
        {
            decimal num = 2764800M;
            decimal num2 = 28800M;
            decimal num3 = 1380M;
            decimal num4 = (num + num2) + num3;
            int num5 = 0x3b2aa;

            Buffer = BitConverter.GetBytes(Convert.ToUInt32(num4.ToString()));
            API.SetMemory(0x26fd10a, Buffer);

            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fc942, Buffer);

            num5 = 0x53e10;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcb70, Buffer);

            num5 = 0x1199ed0;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd050, Buffer);

            num5 = 0x70c7;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd152, Buffer);

            num5 = 0x54a7;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcbe2, Buffer);

            Buffer = [
                0, 0, 15, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0,
                0x36, 0, 0, 0, 0, 0, 0x4c, 15, 0x13
             ];
            API.SetMemory(0x26fd016, Buffer);
        }
        public void DoMediumStats()
        {
            decimal num = 2764800M;
            decimal num2 = 28800M;
            decimal num3 = 1380M;
            decimal num4 = (num + num2) + num3;
            int num5 = 0x379a;

            Buffer = BitConverter.GetBytes(Convert.ToUInt32(num4.ToString()));
            API.SetMemory(0x26fd10a, Buffer);

            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fc942, Buffer);

            num5 = 0x8634;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcb70, Buffer);

            num5 = 0x1c297b;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd050, Buffer);

            num5 = 0xb47;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd152, Buffer);

            num5 = 0x48f;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcbe2, Buffer);

            Buffer = [
                0, 0, 15, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0,
                0x36, 0, 0, 0, 0, 0, 0x4c, 15, 0x13
             ];
            API.SetMemory(0x26fd016, Buffer);
        }
        public void DoHighStats()
        {
            decimal num = 2764800M;
            decimal num2 = 28800M;
            decimal num3 = 1380M;
            decimal num4 = (num + num2) + num3;
            int num5 = 0x22c0a;

            Buffer = BitConverter.GetBytes(Convert.ToUInt32(num4.ToString()));
            API.SetMemory(0x26fd10a, Buffer);

            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fc942, Buffer);

            num5 = 0x9d1f0;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcb70, Buffer);

            num5 = 0x1199ed0;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd050, Buffer);

            num5 = 0x6d967;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fd152, Buffer);

            num5 = 0x1c7dd;
            Buffer = BitConverter.GetBytes(Convert.ToInt32(num5.ToString()));
            API.SetMemory(0x26fcbe2, Buffer);

            Buffer = [
                0, 0, 15, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0,
                0x36, 0, 0, 0, 0, 0, 0x4c, 15, 0x13
             ];
            API.SetMemory(0x26fd016, Buffer);
        }
        public void DoLevel55()
        {
            Buffer = [
                0, 0, 15, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0,
                0x36, 0, 0, 0, 0, 0, 0x4c, 15, 0x13
             ];
            API.SetMemory(0x26fd016, Buffer);
        }
        public void DoPreOrderBonuses()
        {
            Buffer = [0xff];
            API.SetMemory(0x2708219, Buffer);

            Buffer = [0xff];
            API.SetMemory(0x270821c, Buffer);
        }
        public void DoUnlockAll()
        {
            Buffer = new UnlockAll().GetUnlockAllBuffer();
            API.SetMemory(0x26fedf0, Buffer);
            API.SetMemory(0x2708219, [0xff]);
            API.SetMemory(0x270821c, [0xff]);
        }
        public void Do255Token()
        {
            Buffer = [0xff, 0xff];
            API.SetMemory(0x2706938, Buffer);
        }
        public void EditScore(int score)
        {
            Buffer = BitConverter.GetBytes(score);
            API.SetMemory(0x26fd050, Buffer);
        }
        public void EditWins(int wins)
        {
            Buffer = BitConverter.GetBytes(wins);
            API.SetMemory(0x26fd152, Buffer);
        }
        public void EditLosses(int losses)
        {
            Buffer = BitConverter.GetBytes(losses);
            API.SetMemory(0x26fcbe2, Buffer);
        }
        public void EditKills(int kills)
        {
            Buffer = BitConverter.GetBytes(kills);
            API.SetMemory(0x26fcb70, Buffer);
        }
        public void EditDeaths(int deaths)
        {
            Buffer = BitConverter.GetBytes(deaths);
            API.SetMemory(0x26fc942, Buffer);
        }
    }
}
