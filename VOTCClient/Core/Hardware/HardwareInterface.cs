﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;
/*
    This file is part of VOTC.

    VOTC is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    VOTC is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with VOTC.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace VOTCClient.Core.Hardware
{
    public static class HardwareInterface
    {
        private static Computer _computer;
        public static List<ISensor> CPUSensors = new List<ISensor>();
        public static List<ISensor> GPUSensors = new List<ISensor>();
        public static List<ISensor> RAMSensors = new List<ISensor>();
        public static List<ISensor> OtherSensors = new List<ISensor>();

        public async static Task Initialize()
        {
            await Task.Run(() =>
            {
                _computer = new Computer {RAMEnabled = true, GPUEnabled = true, CPUEnabled = true, FanControllerEnabled = true, HDDEnabled = true, MainboardEnabled = true};
                _computer.Open();
                GetSensors();
            });
        }

        public static void GetSensors()
        {
            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update();
                var sensors = hardware.Sensors;
                new List<ISensor>().AddRange(sensors);
                switch (hardware.HardwareType)
                {
                        case HardwareType.CPU:
                        CPUSensors.AddRange(sensors);
                        break;
                        case HardwareType.GpuAti:
                        case HardwareType.GpuNvidia:
                        GPUSensors.AddRange(sensors);
                        break;
                        case HardwareType.RAM:
                        RAMSensors.AddRange(sensors);
                        break;
                    default:
                        OtherSensors.AddRange(sensors);
                        break;
                }
            }
        }
    }
}