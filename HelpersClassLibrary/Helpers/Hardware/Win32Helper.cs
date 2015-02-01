﻿using System;
using System.Collections.Generic;
using System.Management;

namespace Lugia.Helpers.Hardware
{
    class Win32Helper
    {
        /// <summary>
        /// 非00开头的MAC地址
        /// </summary>
        internal List<string> Macs
        {
            get
            {
                List<string> macs = new List<string>();
                try
                {
                    string mac = "";
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        if ((bool)mo["IPEnabled"])
                        {
                            mac = mo["MacAddress"].ToString();
                            macs.Add(mac);
                        }
                    }
                    moc = null;
                    mc = null;
                }
                catch
                {
                    throw;
                }
                return macs;
            }
        }

        /// <summary>
        /// CPU ID
        /// *此方法很慢
        /// </summary>
        internal string CpuID
        {
            get
            {
                try
                {
                    string cpuInfo = "";//cpu序列号   
                    ManagementClass mc = new ManagementClass("Win32_Processor");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    }
                    moc = null;
                    mc = null;
                    return cpuInfo;
                }
                catch
                {
                    return "unknow";
                }
                finally
                {
                }
            }
        }

        /// <summary>
        /// 硬盘序列号
        /// </summary>
        internal List<string> DiskSerialNumber
        {
            get
            {
                List<string> ids = new List<string>();
                try
                {
                    string hdId = "";
                    ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        hdId = (string)mo.Properties["SerialNumber"].Value;
                        ids.Add(hdId);
                    }
                    moc = null;
                    mc = null;
                    return ids;
                }
                catch (Exception)
                {
                    return ids;
                }
            }
        }
    }
}
